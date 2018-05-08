using OsuPlayground.OsuMath;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace OsuPlayground.HitObjects
{
    // This code was adapted from the following file:
    // https://github.com/ppy/osu/blob/master/osu.Game/Rulesets/Objects/CircularArcApproximator.cs
    public class CircularArcApproximator
    {
        private readonly Vector2 a;
        private readonly Vector2 b;
        private readonly Vector2 c;

        private int amountPoints;

        private const float tolerance = 0.1f;

        public CircularArcApproximator(Vector2 a, Vector2 b, Vector2 c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        
        public List<Vector2> CreateArc()
        {
            float aSq = (this.b - this.c).sqrMagnitude;
            float bSq = (this.a - this.c).sqrMagnitude;
            float cSq = (this.a - this.b).sqrMagnitude;
            
            if (Precision.AlmostEquals(aSq, 0) || Precision.AlmostEquals(bSq, 0) || Precision.AlmostEquals(cSq, 0))
            {
                return new List<Vector2>();
            }

            float s = aSq * (bSq + cSq - aSq);
            float t = bSq * (aSq + cSq - bSq);
            float u = cSq * (aSq + bSq - cSq);

            float sum = s + t + u;
            
            if (Precision.AlmostEquals(sum, 0))
            {
                return new List<Vector2>();
            }

            Vector2 centre = (s * this.a + t * this.b + u * this.c) / sum;
            Vector2 dA = this.a - centre;
            Vector2 dC = this.c - centre;

            float r = dA.magnitude;

            double thetaStart = Math.Atan2(dA.y, dA.x);
            double thetaEnd = Math.Atan2(dC.y, dC.x);

            while (thetaEnd < thetaStart)
            {
                thetaEnd += 2 * Math.PI;
            }

            double dir = 1;
            double thetaRange = thetaEnd - thetaStart;
            
            Vector2 orthoAtoC = this.c - this.a;
            orthoAtoC = new Vector2(orthoAtoC.y, -orthoAtoC.x);
            if (Vector2.Dot(orthoAtoC, this.b - this.a) < 0)
            {
                dir = -dir;
                thetaRange = 2 * Math.PI - thetaRange;
            }
            
            this.amountPoints = 2 * r <= tolerance ? 2 : Math.Max(2, (int)Math.Ceiling(thetaRange / (2 * Math.Acos(1 - tolerance / r))));

            List<Vector2> output = new List<Vector2>(this.amountPoints);

            for (int i = 0; i < this.amountPoints; ++i)
            {
                double fract = (double)i / (this.amountPoints - 1);
                double theta = thetaStart + dir * fract * thetaRange;
                Vector2 o = new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * r;
                output.Add(centre + o);
            }

            return output;
        }
    }
}