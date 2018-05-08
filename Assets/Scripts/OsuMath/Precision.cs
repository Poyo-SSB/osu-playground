using System;
using UnityEngine;

namespace OsuPlayground.OsuMath
{
    // This code was adapted from the following file:
    // https://github.com/ppy/osu-framework/blob/master/osu.Framework/MathUtils/Precision.cs
    public static class Precision
    {
        public const float FLOAT_EPSILON = 1e-3f;
        public const double DOUBLE_EPSILON = 1e-7;

        public static bool DefinitelyBigger(float value1, float value2, float acceptableDifference = FLOAT_EPSILON) => value1 - acceptableDifference > value2;

        public static bool DefinitelyBigger(double value1, double value2, double acceptableDifference = DOUBLE_EPSILON) => value1 - acceptableDifference > value2;

        public static bool AlmostBigger(float value1, float value2, float acceptableDifference = FLOAT_EPSILON) => value1 > value2 - acceptableDifference;

        public static bool AlmostBigger(double value1, double value2, double acceptableDifference = DOUBLE_EPSILON) => value1 > value2 - acceptableDifference;

        public static bool AlmostEquals(float value1, float value2, float acceptableDifference = FLOAT_EPSILON) => Math.Abs(value1 - value2) <= acceptableDifference;

        public static bool AlmostEquals(Vector2 value1, Vector2 value2, float acceptableDifference = FLOAT_EPSILON) => AlmostEquals(value1.x, value2.x, acceptableDifference) && AlmostEquals(value1.y, value2.y, acceptableDifference);

        public static bool AlmostEquals(double value1, double value2, double acceptableDifference = DOUBLE_EPSILON) => Math.Abs(value1 - value2) <= acceptableDifference;
    }
}
