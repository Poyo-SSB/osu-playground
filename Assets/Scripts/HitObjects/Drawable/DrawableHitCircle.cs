using OsuPlayground.OsuMath;
using System.Collections.Generic;
using UnityEngine;

namespace OsuPlayground.HitObjects.Drawable
{
    public class DrawableHitCircle : DrawableHitObject
    {
        public void UpdateWith(float radius, float ratio, KeyValuePair<int, HitCircle> keyValuePair)
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
        }
    }
}
