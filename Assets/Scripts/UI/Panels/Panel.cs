using UnityEngine;
using UnityEngine.UI;

namespace OsuPlayground.UI.Panels
{
    /// <summary>
    /// Defines a base class for UI panels.
    /// </summary>
    [RequireComponent(typeof(RectTransform), typeof(CanvasGroup), typeof(Image))]
    public abstract class Panel : MonoBehaviour
    {
        [HideInInspector]
        public RectTransform RectTransform;
        [HideInInspector]
        public CanvasGroup CanvasGroup;
        [HideInInspector]
        public Image Background;

        private void Awake()
        {
            this.RectTransform = this.GetComponent<RectTransform>();
            this.CanvasGroup = this.GetComponent<CanvasGroup>();
            this.Background = this.GetComponent<Image>();
        }
    }
}