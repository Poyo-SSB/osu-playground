using UnityEngine;

namespace OsuPlayground.HitObjects.Drawable.Components.Helpers
{
    /// <summary>
    /// Creates cool vertices.
    /// </summary>
    public static class MeshHelper
    {
        public static UIVertex Vertex(Vector2 position, Color color)
        {
            return new UIVertex
            {
                position = position,
                color = color,
                normal = Vector3.forward
            };
        }

        public static UIVertex Vertex(float x, float y, Color color) => Vertex(new Vector2(x, y), color);
    }
}
