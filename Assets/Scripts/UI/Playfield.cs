using OsuPlayground.HitObjects;
using OsuPlayground.HitObjects.Drawable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OsuPlayground.UI
{
    /// <summary>
    /// Actually draws hit objects.
    /// </summary>
    public class Playfield : MonoBehaviour
    {
        public const int WIDTH = 512;
        public const int HEIGHT = 384;

        [HideInInspector]
        public RectTransform RectTransform;

        private List<DrawableHitCircle> hitCircles = new List<DrawableHitCircle>();
        private List<DrawableSlider> sliders = new List<DrawableSlider>();

        private int bufferIndex = 0;
        private Dictionary<int, HitCircle> hitCircleBuffer = new Dictionary<int, HitCircle>();
        private Dictionary<int, Slider> sliderBuffer = new Dictionary<int, Slider>();

        public string LatestHitObjects = String.Empty;
        public string HoveredObject = String.Empty;

        private void Awake() => this.RectTransform = this.GetComponent<RectTransform>();

        public HitCircle HitCircle(Vector2 position, bool draw = true)
        {
            var hitCircle = new HitCircle { Position = position };
            if (draw)
            {
                this.hitCircleBuffer.Add(++this.bufferIndex, hitCircle);
            }
            return hitCircle;
        }

        public Slider Slider(CurveType type, List<Vector2> list, bool draw = true)
        {
            var slider = new Slider
            {
                CurveType = type,
                Position = list[0],
                ControlPoints = list
            };
            if (draw)
            {
                this.sliderBuffer.Add(++this.bufferIndex, slider);
            }
            return slider;
        }

        public Slider Slider(CurveType type, List<Vector2> list, bool draw, float length)
        {
            var slider = new Slider
            {
                CurveType = type,
                Position = list[0],
                ControlPoints = list,
                Length = length
            };

            if (draw)
            {
                this.sliderBuffer.Add(++this.bufferIndex, slider);
            }
            return slider;
        }

        private void LateUpdate()
        {
            // Let's draw.

            // First get the ratio between the screen's width and the arbitrary playfield width.
            var ratio = this.RectTransform.rect.width / WIDTH;

            // This is the formula for circle size in arbitrary "osu!pixels."
            var radius = 64 * ((1 - 0.7f * (Options.CircleSize.Value - 5) / 5) / 2);

            // How many of each object type have been buffered this frame?
            var circleCount = this.hitCircleBuffer.Count;
            var sliderCount = this.sliderBuffer.Count;

            // Create displayable objects if there aren't enough.
            for (int i = 0; i < circleCount; i++)
            {
                if (i >= this.hitCircles.Count)
                {
                    this.hitCircles.Add(Instantiate(Playground.DrawableHitCirclePrefab, this.transform).GetComponent<DrawableHitCircle>());
                }
            }
            for (int i = 0; i < sliderCount; i++)
            {
                if (i >= this.sliders.Count)
                {
                    this.sliders.Add(Instantiate(Playground.DrawableSliderPrefab, this.transform).GetComponent<DrawableSlider>());
                }
            }

            // Set the relevant variables so the hit objects are drawn in the right place.
            for (int i = 0; i < circleCount; i++)
            {
                var pair = this.hitCircleBuffer.ElementAt(i);
                this.hitCircles[i].UpdateWith(radius, ratio, pair.Value, pair.Key);
                this.hitCircles[i].transform.SetSiblingIndex(this.bufferIndex - this.hitCircleBuffer.ElementAt(i).Key);
            }
            for (int i = 0; i < sliderCount; i++)
            {
                var pair = this.sliderBuffer.ElementAt(i);
                this.sliders[i].UpdateWith(radius, ratio, pair.Value, pair.Key);
                this.sliders[i].transform.SetSiblingIndex(this.bufferIndex - this.sliderBuffer.ElementAt(i).Key);
            }

            // Hide any excess hit objects. This is done to avoid garbage collection every frame, which would cause absurd performance drops.
            for (int i = 0; i < this.hitCircles.Count; i++)
            {
                this.hitCircles[i].gameObject.SetActive(i < circleCount);
            }
            for (int i = 0; i < this.sliders.Count; i++)
            {
                this.sliders[i].gameObject.SetActive(i < sliderCount);
            }

            // Create the text to be used for exporting the created patterns to text.
            StringBuilder exportBuilder = new StringBuilder();

            for (int i = 1; i < circleCount + sliderCount + 1; i++)
            {
                if (this.sliderBuffer.ContainsKey(i))
                {
                    exportBuilder.AppendLine(this.sliderBuffer[i].ToString());
                }
                else
                {
                    exportBuilder.AppendLine(this.hitCircleBuffer[i].ToString());
                }
            }

            this.LatestHitObjects = exportBuilder.ToString();

            // Set the hit object index to 0 and clear the buffers so that they're fresh for the next frame.
            this.bufferIndex = 0;
            this.hitCircleBuffer.Clear();
            this.sliderBuffer.Clear();

            var r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit info;
            if (Physics.Raycast(r, out info, Single.MaxValue, ~0))
            {
                var hitCircle = info.transform.parent.GetComponent<DrawableHitCircle>();
                var slider = info.transform.parent.GetComponent<DrawableSlider>();
                if (hitCircle != null)
                {
                    this.HoveredObject = $"[{hitCircle.Index}] Hit circle - " +
                        $"position: ({Mathf.Round(hitCircle.Position.x * 100f) / 100f}, {Mathf.Round(hitCircle.Position.y * 100f) / 100f})";
                }
                else if (slider != null)
                {
                    this.HoveredObject = $"[{slider.Index}] Slider - " +
                        $"start: ({Mathf.Round(slider.Position.x * 100f) / 100f}, {Mathf.Round(slider.Position.y * 100f) / 100f}), " +
                        $"end: ({Mathf.Round(slider.Slider.PositionAt(1).x * 100f) / 100f}, {Mathf.Round(slider.Slider.PositionAt(1).y * 100f) / 100f}), " +
                        $"length: {Mathf.Round(slider.Slider.Length * 100f) / 100f}";
                }
                else
                {
                    this.HoveredObject = String.Empty;
                }
            }
            else
            {
                this.HoveredObject = String.Empty;
            }
        }
    }
}