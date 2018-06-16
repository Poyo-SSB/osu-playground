using UnityEngine;

namespace OsuPlayground
{
    public static class Constants
    {
        /// <summary>
        /// Defines how many vertices a circle should have.
        /// </summary>
        public const int CIRCLE_RESOLUTION = 24;

        public const float TOOLBAR_HEIGHT = 40;
        public const float OPTIONS_WIDTH = 350;

        public static readonly Color TOOLBAR_COLOR = new Color(0.05f, 0.05f, 0.05f);
        public static readonly Color OPTIONS_COLOR = new Color(0.05f, 0.05f, 0.05f);
        public static readonly Color PLAYFIELD_COLOR = Color.black;

        public static readonly Color BORDER_COLOR = Color.white;
        public static readonly Color BODY_COLOR = new Color(0.15f, 0.15f, 0.15f);

        /// <summary>
        /// Defines the size of a circle's body relative to its border.
        /// </summary>
        public const float BODY_SIZE_MULTIPLIER = 0.85f;
        /// <summary>
        /// Defines the size of a tick relative to its parent's size.
        /// </summary>
        public const float TICK_SIZE_MULTIPLIER = 0.125f;

        /// <summary>
        /// Defines the size of a circle at CS4. Used for text size calculation.
        /// </summary>
        public const float BASE_CIRCLE_RADIUS = 36.48f;
        /// <summary>
        /// The size of text at CS4.
        /// </summary>
        public const float BASE_TEXT_SIZE = 35f;

        public static readonly Color HANDLE_COLOR = Color.yellow;
        public static readonly Color MULTIPLE_HANDLE_COLOR = Color.blue;
    }
}
