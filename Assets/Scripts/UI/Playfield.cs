using OsuPlayground.HitObjects;
using OsuPlayground.HitObjects.Drawable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OsuPlayground.UI
{
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

        private void LateUpdate()
        {
            var ratio = this.RectTransform.rect.width / Playfield.WIDTH;

            var radius = 64 * ((1 - 0.7f * (Options.CircleSize.Value - 5) / 5) / 2);

            var circleCount = this.hitCircleBuffer.Count;
            var sliderCount = this.sliderBuffer.Count;

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

            for (int i = 0; i < circleCount; i++)
            {
                this.hitCircles[i].UpdateWith(radius, ratio, this.hitCircleBuffer.ElementAt(i));
                this.hitCircles[i].transform.SetSiblingIndex(this.bufferIndex - this.hitCircleBuffer.ElementAt(i).Key);
            }
            for (int i = 0; i < sliderCount; i++)
            {
                this.sliders[i].UpdateWith(radius, ratio, this.sliderBuffer.ElementAt(i));
                this.sliders[i].transform.SetSiblingIndex(this.bufferIndex - this.sliderBuffer.ElementAt(i).Key);
            }

            for (int i = 0; i < this.hitCircles.Count; i++)
            {
                this.hitCircles[i].gameObject.SetActive(i < circleCount);
            }

            for (int i = 0; i < this.sliders.Count; i++)
            {
                this.sliders[i].gameObject.SetActive(i < sliderCount);
            }

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

            this.bufferIndex = 0;
            this.hitCircleBuffer.Clear();
            this.sliderBuffer.Clear();
        }
    }
}