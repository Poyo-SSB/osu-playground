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

        public void UpdateWith(float radius, float ratio, KeyValuePair<int, Slider> keyValuePair)
        {
            var position = keyValuePair.Value.Position.PlayfieldOffset();

            this.Text.text = keyValuePair.Key.ToString();
            var textSize = Mathf.RoundToInt(ratio * Constants.BASE_TEXT_SIZE * (radius / Constants.BASE_CIRCLE_RADIUS));
            this.Text.fontSize = textSize;
            this.Text.enabled = textSize > 0;
            this.Text.rectTransform.anchoredPosition = ratio * position;

            this.Circle.rectTransform.anchoredPosition = ratio * position;
            this.Circle.Radius = ratio * radius;
            this.Circle.SetVerticesDirty();
            
            this.Body.Radius = ratio * radius;
            this.Body.Path = keyValuePair.Value.GetPath().Select(x => ratio * x.PlayfieldOffset()).ToList();
            this.Body.SetVerticesDirty();

            // Calculate the positions of ticks and draw them accordingly.
            var tickDistance = 100 * Options.SliderMultiplier.Value * Options.SpeedMultiplier.Value / Options.TickRate.Value;

            List<Vector2> tickPositions = new List<Vector2>();
            for (float i = 0; i < keyValuePair.Value.Distance; i += tickDistance)
            {
                var tickPosition = keyValuePair.Value.PositionAt(i / keyValuePair.Value.Distance);
                tickPositions.Add(ratio * tickPosition.PlayfieldOffset());
            }

            this.Ticks.Radius = ratio * radius;
            this.Ticks.Positions = tickPositions;
            this.Ticks.SetVerticesDirty();
        }
    }
}
