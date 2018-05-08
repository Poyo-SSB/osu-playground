using OsuPlayground.UI.Handles;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OsuPlayground.UI.Panels
{
    /// <summary>
    /// Defines the main panel containing the cool things.
    /// </summary>
    public class PlayfieldPanel : Panel, IDragHandler, IScrollHandler
    {
        [SerializeField]
        private Border border;
        [SerializeField]
        private Playfield playfield;
        [SerializeField]
        private HandleManager handleManager;

        public Vector2 RawOffset;
        public float RawZoom;

        private float zoomFactor;

        private void Update()
        {
            // This function primarily handles zooming and panning.

            this.zoomFactor = Mathf.Pow(2, this.RawZoom / 4 - 0.25f);

            var availableSpace = this.RectTransform.rect.size;

            var spaceRatio = availableSpace.x / availableSpace.y;
            var playfieldRatio = (float)Playfield.WIDTH / Playfield.HEIGHT;

            var multiplier = 0f;

            if (spaceRatio < playfieldRatio)
            {
                multiplier = availableSpace.x / Playfield.WIDTH;
            }
            else
            {
                multiplier = availableSpace.y / Playfield.HEIGHT;
            }

            this.border.rectTransform.anchoredPosition = this.RawOffset * this.zoomFactor;
            this.border.rectTransform.sizeDelta = multiplier * this.zoomFactor * new Vector2(
                Playfield.WIDTH,
                Playfield.HEIGHT);

            this.border.SetVerticesDirty();

            this.playfield.RectTransform.anchoredPosition = this.RawOffset * this.zoomFactor;
            this.playfield.RectTransform.sizeDelta = multiplier * this.zoomFactor * new Vector2(
                Playfield.WIDTH,
                Playfield.HEIGHT);

            this.handleManager.RectTransform.anchoredPosition = this.RawOffset * this.zoomFactor;
            this.handleManager.RectTransform.sizeDelta = multiplier * this.zoomFactor * new Vector2(
                Playfield.WIDTH,
                Playfield.HEIGHT);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                this.RawOffset += eventData.delta / this.zoomFactor;
            }
        }

        public void OnScroll(PointerEventData eventData)
        {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                this.RawZoom += Mathf.Round(eventData.scrollDelta.y);
                this.RawZoom = Mathf.Clamp(this.RawZoom, -20, 12);
            }
        }
    }
}
