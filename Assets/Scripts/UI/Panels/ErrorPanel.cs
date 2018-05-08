using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OsuPlayground.UI.Panels
{
    public class ErrorPanel : Panel, IPointerClickHandler
    {
        public Text Text;

        public void OnPointerClick(PointerEventData eventData)
        {
            this.gameObject.SetActive(false);
        }
    }
}
