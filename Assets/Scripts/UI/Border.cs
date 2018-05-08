using UnityEngine;
using UnityEngine.UI;

namespace OsuPlayground.UI
{
    /// <summary>
    /// Draws a border of a specified width which contains its <see cref="RectTransform"/>'s area.
    /// </summary>
    public class Border : MaskableGraphic
    {
        public float Width = 3;

        private UIVertex[] Quad(params Vector3[] positions)
        {
            var vertices = new UIVertex[4];
            for (int i = 0; i < 4; i++)
            {
                vertices[i] = new UIVertex
                {
                    position = positions[i],
                    color = this.color
                };
            }
            return vertices;
        }
        
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            // Manually define eight vertices and triangles.
            
            vh.Clear();

            Vector2 from = this.rectTransform.rect.min;
            Vector2 to = this.rectTransform.rect.max;

            Vector3 inner0 = new Vector3(to.x, to.y);
            Vector3 inner1 = new Vector3(to.x, from.y);
            Vector3 inner2 = new Vector3(from.x, from.y);
            Vector3 inner3 = new Vector3(from.x, to.y);
            Vector3 outer0 = new Vector3(to.x + this.Width, to.y + this.Width);
            Vector3 outer1 = new Vector3(to.x + this.Width, from.y - this.Width);
            Vector3 outer2 = new Vector3(from.x - this.Width, from.y - this.Width);
            Vector3 outer3 = new Vector3(from.x - this.Width, to.y + this.Width);

            vh.AddUIVertexQuad(Quad(inner0, inner1, outer1, outer0));
            vh.AddUIVertexQuad(Quad(inner1, inner2, outer2, outer1));
            vh.AddUIVertexQuad(Quad(inner2, inner3, outer3, outer2));
            vh.AddUIVertexQuad(Quad(inner3, inner0, outer0, outer3));
        }
    }
}
