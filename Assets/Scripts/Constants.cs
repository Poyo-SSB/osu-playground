using UnityEngine;

namespace OsuPlayground
{
    public static class Constants
    {
        public const int CIRCLE_RESOLUTION = 24;

        public const float TOOLBAR_HEIGHT = 40;
        public const float OPTIONS_WIDTH = 350;

        public static readonly Color TOOLBAR_COLOR = new Color(0.05f, 0.05f, 0.05f);
        public static readonly Color OPTIONS_COLOR = new Color(0.05f, 0.05f, 0.05f);
        public static readonly Color PLAYFIELD_COLOR = Color.black;

        public static readonly Color BORDER_COLOR = Color.white;
        public static readonly Color BODY_COLOR = new Color(0.15f, 0.15f, 0.15f);

        public const float BODY_SIZE_MULTIPLIER = 0.85f;
        public const float TICK_SIZE_MULTIPLIER = 0.125f;

        public const float BASE_CIRCLE_RADIUS = 36.48f;
        public const float BASE_TEXT_SIZE = 35f;

        public static readonly Color HANDLE_COLOR = Color.yellow;
    }
}
