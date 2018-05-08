using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OsuPlayground.HitObjects.Drawable.Components.Helpers
{
    /// <summary>
    /// Draws a line of arbitrary radius with rounded caps.
    /// </summary>
    public static class LineDrawer
    {
        /// <summary>
        /// Instead of calculating something logically, this function instead just draws an entire new segment with every pair of points.
        /// This means that there are several orders of magnitude more vertices than necessary, but it's mathematically easy (for me).
        /// </summary>
        public static void Line(List<Vector2> positions, VertexHelper vh, float radius, Color color)
        {
            var segmentCount = positions.Count - 1;

            if (segmentCount < 1)
            {
                return;
            }

            var vertexIndex = vh.currentVertCount;

            var circlePoints = Constants.CIRCLE_RESOLUTION;
            var capPoints = circlePoints / 2 + 1;

            for (int i = 0; i < segmentCount; i++)
            {
                var from = positions[i];
                var to = positions[i + 1];

                var angle = Mathf.Atan2(from.y - to.y, from.x - to.x);
                var fromAngle = angle - Mathf.PI / 2f;
                var toAngle = angle + Mathf.PI / 2f;

                var fromIndex = vh.currentVertCount;
                vh.AddVert(MeshHelper.Vertex(from, color));

                for (int j = 0; j < capPoints; j++)
                {
                    var offset = new Vector2(
                        Mathf.Cos(fromAngle + j * 2 * Mathf.PI / circlePoints),
                        Mathf.Sin(fromAngle + j * 2 * Mathf.PI / circlePoints));

                    vh.AddVert(MeshHelper.Vertex(from + radius * offset, color));
                }

                for (int j = 0; j < capPoints; j++)
                {
                    vh.AddTriangle(
                        vertexIndex,
                        vertexIndex + (j) % (capPoints) + 1,
                        vertexIndex + (j + 1) % (capPoints) + 1);
                }

                vertexIndex = vh.currentVertCount;

                var toIndex = vh.currentVertCount;
                vh.AddVert(MeshHelper.Vertex(to, color));

                for (int j = 0; j < capPoints; j++)
                {
                    var offset = new Vector2(
                        Mathf.Cos(toAngle + j * 2 * Mathf.PI / circlePoints),
                        Mathf.Sin(toAngle + j * 2 * Mathf.PI / circlePoints));

                    vh.AddVert(MeshHelper.Vertex(to + radius * offset, color));
                }

                for (int j = 0; j < capPoints; j++)
                {
                    vh.AddTriangle(
                        vertexIndex,
                        vertexIndex + (j) % (capPoints) + 1,
                        vertexIndex + (j + 1) % (capPoints) + 1);
                }

                vh.AddTriangle(toIndex, fromIndex + 1, fromIndex);
                vh.AddTriangle(toIndex, toIndex + capPoints, fromIndex + 1);
                vh.AddTriangle(fromIndex, toIndex + 1, toIndex);
                vh.AddTriangle(fromIndex, fromIndex + capPoints, toIndex + 1);
            }
        }
    }
}
