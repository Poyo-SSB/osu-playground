using OsuPlayground.Bindables;

namespace OsuPlayground
{
    public static class Options
    {
        public static Bindable<bool> ShowOptions = true;

        public static Bindable<float> CircleSize = 4;

        public static Bindable<float> SliderMultiplier = 1.4f;
        public static Bindable<float> SpeedMultiplier = 1;

        public static Bindable<int> TickRate = 1;

        public static Bindable<int> BeatSnapDivisor = 4;
    }
}
