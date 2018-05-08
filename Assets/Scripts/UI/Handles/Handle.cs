using OsuPlayground.Bindables;
using OsuPlayground.OsuMath;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OsuPlayground.UI.Handles
{
    public class Handle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler
    {
        [HideInInspector]
        public HandleManager Manager;

        private RectTransform rectTransform;

        [SerializeField]
        private Image center;

        public Bindable<Vector2> Bound;

        private float ratio;

        private void Awake() => this.rectTransform = this.GetComponent<RectTransform>();

        public void OnDrag(PointerEventData eventData)
        {
            // Set the color because the handles lag behind by one frame for some reason which I don't care enough to ascertain.
            this.center.color = Constants.HANDLE_COLOR;
            this.Bound.Value += new Vector2(eventData.delta.x, -eventData.delta.y) / this.ratio;
        }

        public void OnEndDrag(PointerEventData eventData) => this.center.color = Color.white;

        public void OnPointerEnter(PointerEventData eventData) => this.center.color = Constants.HANDLE_COLOR;

        public void OnPointerExit(PointerEventData eventData) => this.center.color = Color.white;

        public void UpdatePosition(float ratio)
        {
            this.ratio = ratio;
            this.rectTransform.anchoredPosition = this.Bound.Value.PlayfieldOffset() * ratio;
        }
    }
}