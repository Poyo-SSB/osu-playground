using OsuPlayground.Bindables;

namespace OsuPlayground
{
    public static class Options
    {
        /// <summary>
        /// Resets every option.
        /// </summary>
        public static void Reset()
        {
            ShowOptions = true;
            CircleSize = 4;
            SliderMultiplier = 1.4f;
            SpeedMultiplier = 1;
            TickRate = 1;
            BeatSnapDivisor = 4;
        }

        /// <summary>
        /// Defines if the options panel should be shown.
        /// </summary>
        public static Bindable<bool> ShowOptions = true;

        /// <summary>
        /// Defines the osu! "Circle Size" value.
        /// </summary>
        public static Bindable<float> CircleSize = 4;

        /// <summary>
        /// Defines the osu! "slider multiplier" value. Also known as "slider velocity."
        /// </summary>
        public static Bindable<float> SliderMultiplier = 1.4f;
        /// <summary>
        /// Defines the osu! "speed multiplier" value. Also known as "slider velocity multiplier."
        /// </summary>
        public static Bindable<float> SpeedMultiplier = 1;

        /// <summary>
        /// Defines how often ticks are drawn on a slider's body.
        /// </summary>
        public static Bindable<int> TickRate = 1;

        /// <summary>
        /// Defines how many beats a measure is divided into, which controls how sliders are shortened when too long.
        /// </summary>
        public static Bindable<int> BeatSnapDivisor = 4;
    }
}
