using OsuPlayground.HitObjects.Drawable.Components.Helpers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OsuPlayground.HitObjects.Drawable.Components
{
    public class SliderTicks : MaskableGraphic
    {
        public float Radius;
        public List<Vector2> Positions;

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            foreach (var position in this.Positions)
            {
                CircleDrawer.Circle(position, vh, this.Radius * Constants.TICK_SIZE_MULTIPLIER, Constants.BORDER_COLOR);
            }
        }
    }
}
