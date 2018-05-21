using System.Collections.Generic;
using System.Linq;
using OsuPlayground.HitObjects.Drawable.Components;
using OsuPlayground.OsuMath;
using UnityEngine;

namespace OsuPlayground.HitObjects.Drawable
{
    public class DrawableSlider : DrawableHitObject
    {
        public SliderBody Body;
        public SliderTicks Ticks;

        public Slider Slider;

        private RectTransform rectTransform;

        private void Awake() => this.rectTransform = this.GetComponent<RectTransform>();

        public void UpdateWith(float radius, float ratio, Slider slider, int index)
        {
            this.Index = index;
            this.Slider = slider;

            this.rectTransform.anchoredPosition3D = new Vector3(0, 0, index * radius);

            this.Position = slider.Position;

            this.Text.text = index.ToString();
            var textSize = Mathf.RoundToInt(ratio * Constants.BASE_TEXT_SIZE * (radius / Constants.BASE_CIRCLE_RADIUS));
            this.Text.fontSize = textSize;
            this.Text.enabled = textSize > 0;
            this.Text.rectTransform.anchoredPosition = ratio * this.Position.PlayfieldOffset();

            this.Circle.rectTransform.anchoredPosition = ratio * this.Position.PlayfieldOffset();
            this.Circle.Radius = ratio * radius;
            this.Circle.SetVerticesDirty();
            
            this.Body.Radius = ratio * radius;
            this.Body.Path = slider.GetPath().Select(x => ratio * x.PlayfieldOffset()).ToList();
            this.Body.SetVerticesDirty();

            // Calculate the positions of ticks and draw them accordingly.
            var tickDistance = 100 * Options.SliderMultiplier.Value * Options.SpeedMultiplier.Value / Options.TickRate.Value;

            List<Vector2> tickPositions = new List<Vector2>();
            for (float i = 0; i < slider.Length; i += tickDistance)
            {
                var tickPosition = slider.PositionAt(i / slider.Length);
                tickPositions.Add(ratio * tickPosition.PlayfieldOffset());
            }

            this.Ticks.Radius = ratio * radius;
            this.Ticks.Positions = tickPositions;
            this.Ticks.SetVerticesDirty();
        }
    }
}
