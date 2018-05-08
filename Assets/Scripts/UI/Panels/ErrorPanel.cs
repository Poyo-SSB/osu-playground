using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OsuPlayground.UI.Panels
{
    /// <summary>
    /// Defines a pseudo-modal window showing an error when a script fails to load.
    /// </summary>
    public class ErrorPanel : Panel, IPointerClickHandler
    {
        public Text Text;

        public void OnPointerClick(PointerEventData eventData) => this.gameObject.SetActive(false);
    }
}
