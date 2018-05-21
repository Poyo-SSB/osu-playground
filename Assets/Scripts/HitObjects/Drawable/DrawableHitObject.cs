using OsuPlayground.HitObjects.Drawable.Components;
using UnityEngine;
using UnityEngine.UI;

namespace OsuPlayground.HitObjects.Drawable
{
    public abstract class DrawableHitObject : MonoBehaviour
    {
        public Vector2 Position;
        public int Index;

        public Text Text;
        public Circle Circle;
    }
}
