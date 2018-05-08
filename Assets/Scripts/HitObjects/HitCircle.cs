using System;

namespace OsuPlayground.HitObjects
{
    public class HitCircle : HitObject
    {
        public override string ToString() => String.Format("circle: {0},{1}", Math.Round(this.Position.x), Math.Round(this.Position.y));
    }
}
