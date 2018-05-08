using OsuPlayground.Bindables;
using OsuPlayground.UI.OptionObjects;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace OsuPlayground.UI.Panels
{
    /// <summary>
    /// Contains option objects for manipulation.
    /// </summary>
    public class OptionsPanel : Panel
    {
        [SerializeField]
        private RectTransform container;

        public void AddOption(Bindable<float> variable, string name, int precision, Bindable<float> min, Bindable<float> max, Color color)
        {
            var newObject = Instantiate(Playground.OptionNumberPrefab, this.container);
            var component = newObject.GetComponent<OptionNumber>();

            component.Text.text = name;
            component.ColorBar.color = color;
            component.Text.color = color;
            component.InputField.textComponent.color = color;
            component.Slider.fillRect.GetComponent<Graphic>().color = color;

            var precisionValue = Mathf.Pow(10, precision);

            min.ValueChanged += (value) => component.Slider.minValue = value * precisionValue;
            max.ValueChanged += (value) => component.Slider.maxValue = value * precisionValue;

            variable.ValueChanged += (value) =>
            {
                component.InputField.text = value.ToString();
                component.Slider.value = value * precisionValue;
            };

            component.Slider.onValueChanged.AddListener((value) => variable.Value = value / precisionValue);

            component.InputField.onEndEdit.AddListener((value) =>
            {
                float number;
                if (Single.TryParse(value, out number))
                {
                    variable.Value = Mathf.Clamp(number, min.Value, max.Value);
                }
            });
        }

        public void AddOption(Bindable<int> variable, string name, Bindable<int> min, Bindable<int> max, Color color)
        {
            var newObject = Instantiate(Playground.OptionNumberPrefab, this.container);
            var component = newObject.GetComponent<OptionNumber>();

            component.Text.text = name;
            component.ColorBar.color = color;
            component.Text.color = color;
            component.InputField.textComponent.color = color;
            component.Slider.fillRect.GetComponent<Graphic>().color = color;

            min.ValueChanged += (value) => component.Slider.minValue = value;
            max.ValueChanged += (value) => component.Slider.maxValue = value;

            variable.ValueChanged += (value) =>
            {
                component.InputField.text = value.ToString();
                component.Slider.value = value;
            };

            component.Slider.onValueChanged.AddListener((value) => variable.Value = Mathf.RoundToInt(value));

            component.InputField.onEndEdit.AddListener((value) =>
            {
                int number;
                if (Int32.TryParse(value, out number))
                {
                    variable.Value = Mathf.Clamp(number, min.Value, max.Value);
                }
            });
        }

        public void AddOption(Bindable<bool> variable, string name, Color color)
        {
            var newObject = Instantiate(Playground.OptionBoolPrefab, this.container);
            var component = newObject.GetComponent<OptionBool>();

            component.Text.text = name;
            component.Text.color = color;
            component.ColorBar.color = color;
            component.Toggle.graphic.color = color;

            variable.ValueChanged += (value) => component.Toggle.isOn = value;

            component.Toggle.onValueChanged.AddListener((value) => variable.Value = value);
        }

        public void AddOption(Bindable<Vector2> variable, string name, Color color)
        {
            var newObject = Instantiate(Playground.OptionVector2Prefab, this.container);
            var component = newObject.GetComponent<OptionVector2>();

            component.Text.text = name;
            component.ColorBar.color = color;
            component.Text.color = color;
            component.InputFieldX.textComponent.color = color;
            component.InputFieldY.textComponent.color = color;
            
            variable.ValueChanged += (value) =>
            {
                component.InputFieldX.text = Mathf.Round(value.x).ToString();
                component.InputFieldY.text = Mathf.Round(value.y).ToString();
            };

            component.InputFieldX.onEndEdit.AddListener((value) =>
            {
                float number;
                if (Single.TryParse(value, out number))
                {
                    variable.Value = new Vector2(Mathf.Round(number), Mathf.Round(variable.Value.y));
                }
            });
            component.InputFieldY.onEndEdit.AddListener((value) =>
            {
                float number;
                if (Single.TryParse(value, out number))
                {
                    variable.Value = new Vector2(Mathf.Round(variable.Value.x), Mathf.Round(number));
                }
            });
        }
    }
}
