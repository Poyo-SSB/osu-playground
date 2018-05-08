using UnityEngine;
using UnityEngine.UI;

namespace OsuPlayground.UI.OptionObjects
{
    /// <summary>
    /// Defines a base class for option objects which are manipulable in the sidebar.
    /// </summary>
    public abstract class Option : MonoBehaviour
    {
        public Text Text;
        public Image ColorBar;
    }
}
