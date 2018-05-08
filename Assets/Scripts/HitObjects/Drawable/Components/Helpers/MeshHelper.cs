using UnityEngine;

namespace OsuPlayground.HitObjects.Drawable.Components.Helpers
{
    public static class MeshHelper
    {
        public static UIVertex Vertex(Vector2 position, Color color)
        {
            return new UIVertex
            {
                position = position,
                color = color
            };
        }

        public static UIVertex Vertex(float x, float y, Color color) => Vertex(new Vector2(x, y), color);
    }
}
