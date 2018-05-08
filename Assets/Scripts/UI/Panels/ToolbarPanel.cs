using System;
using UnityEngine;
using UnityEngine.UI;

namespace OsuPlayground.UI.Panels
{
    public enum ToolbarSide
    {
        Left,
        Right
    }

    public class ToolbarPanel : Panel
    {
        private float leftButtonPosition = 5;
        private float rightButtonPosition = -5;
        private GameObject PositionButton(ToolbarSide side)
        {
            var newObject = Instantiate(Playground.ToolbarButtonPrefab, this.transform);

            var rectTransform = newObject.GetComponent<RectTransform>();

            if (side == ToolbarSide.Left)
            {
                rectTransform.pivot = new Vector2(0, 0);
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(0, 1);
                rectTransform.anchoredPosition = new Vector2(this.leftButtonPosition, 5);

                this.leftButtonPosition += 105;
            }
            else
            {
                rectTransform.pivot = new Vector2(1, 0);
                rectTransform.anchorMin = new Vector2(1, 0);
                rectTransform.anchorMax = new Vector2(1, 1);
                rectTransform.anchoredPosition = new Vector2(this.rightButtonPosition, 5);

                this.rightButtonPosition -= 105;
            }

            return newObject;
        }

        public void AddButton(string label, Action<Button> action, ToolbarSide side)
        {
            var newObject = PositionButton(side);

            var button = newObject.GetComponent<Button>();
            newObject.GetComponentInChildren<Text>().text = label;

            button.onClick.AddListener(() => action(button));
        }
    }
}
