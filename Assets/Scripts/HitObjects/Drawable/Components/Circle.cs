using OsuPlayground.HitObjects.Drawable.Components.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace OsuPlayground.HitObjects.Drawable.Components
{
    /// <summary>
    /// Represents both hit circles and slider heads.
    /// </summary>
    public class Circle : MaskableGraphic
    {
        public Vector2 Position;
        public float Radius;

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            CircleDrawer.Circle(this.Position, vh, this.Radius, Constants.BORDER_COLOR);
            CircleDrawer.Circle(this.Position, vh, this.Radius * Constants.BODY_SIZE_MULTIPLIER, Constants.BODY_COLOR);
        }
    }
}
