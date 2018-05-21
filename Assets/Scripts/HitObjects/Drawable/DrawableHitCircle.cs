using OsuPlayground.OsuMath;
using System.Collections.Generic;
using UnityEngine;

namespace OsuPlayground.HitObjects.Drawable
{
    public class DrawableHitCircle : DrawableHitObject
    {
        private RectTransform rectTransform;

        private void Awake() => this.rectTransform = this.GetComponent<RectTransform>();

        public void UpdateWith(float radius, float ratio, HitCircle hitCircle, int index)
        {
            this.Index = index;

            this.rectTransform.anchoredPosition3D = new Vector3(0, 0, index * radius);

            this.Position = hitCircle.Position;

            this.Text.text = index.ToString();
            var textSize = Mathf.RoundToInt(ratio * Constants.BASE_TEXT_SIZE * (radius / Constants.BASE_CIRCLE_RADIUS));
            this.Text.fontSize = textSize;
            this.Text.enabled = textSize > 0;
            this.Text.rectTransform.anchoredPosition = ratio * this.Position.PlayfieldOffset();

            this.Circle.rectTransform.anchoredPosition = ratio * this.Position.PlayfieldOffset();
            this.Circle.Radius = ratio * radius;
            this.Circle.SetVerticesDirty();
        }
    }
}
