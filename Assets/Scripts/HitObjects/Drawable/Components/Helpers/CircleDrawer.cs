using UnityEngine;
using UnityEngine.UI;

namespace OsuPlayground.HitObjects.Drawable.Components.Helpers
{
    /// <summary>
    /// Draws a circle.
    /// </summary>
    public static class CircleDrawer
    {
        public static void Circle(Vector2 position, VertexHelper vh, float radius, Color color)
        {
            var circlePoints = Constants.CIRCLE_RESOLUTION;

            var vertexIndex = vh.currentVertCount;

            vh.AddVert(MeshHelper.Vertex(position, color));

            for (int i = 0; i < circlePoints; i++)
            {
                var offset = new Vector2(
                    Mathf.Cos(i * 2 * Mathf.PI / (circlePoints)),
                    Mathf.Sin(i * 2 * Mathf.PI / (circlePoints)));

                vh.AddVert(MeshHelper.Vertex(position + radius * offset, color));
            }

            for (int i = 0; i < circlePoints; i++)
            {
                vh.AddTriangle(
                    vertexIndex,
                    vertexIndex + i % (circlePoints) + 1,
                    vertexIndex + (i + 1) % (circlePoints) + 1);
            }
        }
    }
}
