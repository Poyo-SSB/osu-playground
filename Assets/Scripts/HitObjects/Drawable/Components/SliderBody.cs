using OsuPlayground.HitObjects.Drawable.Components.Helpers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OsuPlayground.HitObjects.Drawable.Components
{
    public class SliderBody : MaskableGraphic
    {
        public float Radius;
        public List<Vector2> Path;

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            LineDrawer.Line(this.Path, vh, this.Radius, Constants.BORDER_COLOR);
            LineDrawer.Line(this.Path, vh, this.Radius * Constants.BODY_SIZE_MULTIPLIER, Constants.BODY_COLOR);

            var collider = this.GetComponent<SphereCollider>();
            collider.center = this.Path[this.Path.Count - 1];
            collider.radius = this.Radius;
        }
    }
}
