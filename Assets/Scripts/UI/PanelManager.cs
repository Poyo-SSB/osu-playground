using OsuPlayground.UI.Panels;
using UnityEngine;

namespace OsuPlayground.UI
{
    public class PanelManager : MonoBehaviour
    {
        [SerializeField]
        private ToolbarPanel toolbar;
        [SerializeField]
        private OptionsPanel options;
        [SerializeField]
        private PlayfieldPanel playfield;

        private void Update()
        {
            var optionsWidth = Options.ShowOptions.Value ? Constants.OPTIONS_WIDTH : 0;

            this.toolbar.Background.color = Constants.TOOLBAR_COLOR;
            this.toolbar.RectTransform.anchoredPosition = Vector2.zero;
            this.toolbar.RectTransform.sizeDelta = new Vector2(Screen.width, Constants.TOOLBAR_HEIGHT);

            this.options.Background.color = Constants.OPTIONS_COLOR;
            this.options.RectTransform.anchoredPosition = new Vector2(Screen.width - optionsWidth, Constants.TOOLBAR_HEIGHT);
            this.options.RectTransform.sizeDelta = new Vector2(optionsWidth, Screen.height - Constants.TOOLBAR_HEIGHT);

            this.playfield.Background.color = Constants.PLAYFIELD_COLOR;
            this.playfield.RectTransform.anchoredPosition = new Vector2(0, Constants.TOOLBAR_HEIGHT);
            this.playfield.RectTransform.sizeDelta = new Vector2(Screen.width - optionsWidth, Screen.height - Constants.TOOLBAR_HEIGHT);

            this.options.CanvasGroup.alpha = Options.ShowOptions.Value ? 1 : 0;
            this.options.CanvasGroup.blocksRaycasts = Options.ShowOptions.Value;
        }
    }
}
