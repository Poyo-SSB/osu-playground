using OsuPlayground.UI;
using UnityEngine;

namespace OsuPlayground.OsuMath
{
    public static class VectorMath
    {
        /// <summary>
        /// Offsets an input position to account for the osu! playfield coordinate system.
        /// </summary>
        public static Vector2 PlayfieldOffset(this Vector2 input)
        {
            return new Vector2(
                input.x - Playfield.WIDTH / 2,
                -input.y + Playfield.HEIGHT / 2);
        }

        public static Vector2 Rotate(this Vector2 point, float angle) => point.Rotate(Vector3.zero, angle);

        public static Vector2 Rotate(this Vector2 point, Vector2 around, float angle)
        {
            var temporaryPoint = point - around;

            float sin = Mathf.Sin(angle);
            float cos = Mathf.Cos(angle);

            float tx = temporaryPoint.x;
            float ty = temporaryPoint.y;
            temporaryPoint.x = (cos * tx) - (sin * ty);
            temporaryPoint.y = (sin * tx) + (cos * ty);

            return temporaryPoint + around;
        }
    }
}
