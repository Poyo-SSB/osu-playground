using System.Collections.Generic;
using UnityEngine;

namespace OsuPlayground.HitObjects
{
    // This code was adapted from the following file:
    // https://github.com/ppy/osu/blob/master/osu.Game/Rulesets/Objects/CatmullApproximator.cs
    public class CatmullApproximator
    {
        private const int detail = 50;

        private readonly List<Vector2> controlPoints;

        public CatmullApproximator(List<Vector2> controlPoints)
        {
            this.controlPoints = controlPoints;
        }
        
        public List<Vector2> CreateCatmull()
        {
            var result = new List<Vector2>();

            for (int i = 0; i < this.controlPoints.Count - 1; i++)
            {
                var v1 = i > 0 ? this.controlPoints[i - 1] : this.controlPoints[i];
                var v2 = this.controlPoints[i];
                var v3 = i < this.controlPoints.Count - 1 ? this.controlPoints[i + 1] : v2 + v2 - v1;
                var v4 = i < this.controlPoints.Count - 2 ? this.controlPoints[i + 2] : v3 + v3 - v2;

                for (int c = 0; c < detail; c++)
                {
                    result.Add(FindPoint(ref v1, ref v2, ref v3, ref v4, (float)c / detail));
                    result.Add(FindPoint(ref v1, ref v2, ref v3, ref v4, (float)(c + 1) / detail));
                }
            }

            return result;
        }
        
        private Vector2 FindPoint(ref Vector2 vec1, ref Vector2 vec2, ref Vector2 vec3, ref Vector2 vec4, float t)
        {
            float t2 = t * t;
            float t3 = t * t2;

            Vector2 result = new Vector2(
                0.5f * (2f * vec2.x + (-vec1.x + vec3.x) * t + (2f * vec1.x - 5f * vec2.x + 4f * vec3.x - vec4.x) * t2 + (-vec1.x + 3f * vec2.x - 3f * vec3.x + vec4.x) * t3),
                0.5f * (2f * vec2.y + (-vec1.y + vec3.y) * t + (2f * vec1.y - 5f * vec2.y + 4f * vec3.y - vec4.y) * t2 + (-vec1.y + 3f * vec2.y - 3f * vec3.y + vec4.y) * t3));

            return result;
        }
    }
}