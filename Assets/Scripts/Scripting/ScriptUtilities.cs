using OsuPlayground.Bindables;
using OsuPlayground.HitObjects;
using OsuPlayground.UI;
using OsuPlayground.UI.Handles;
using OsuPlayground.UI.Panels;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OsuPlayground.Scripting
{
    public class ScriptUtilites
    {
        public const int PLAYFIELD_WIDTH = 512;
        public const int PLAYFIELD_HEIGHT = 384;

        private static readonly Color green = new Color(0.525490196f, 0.898039216f, 0.278431373f);

        private BindableList variables;
        private Playfield playfield;
        private HandleManager handleManager;
        private OptionsPanel optionsPanel;

        public ScriptUtilites(BindableList variables, Playfield playfield, HandleManager handleManager, OptionsPanel optionsPanel)
        {
            this.variables = variables;
            this.playfield = playfield;
            this.handleManager = handleManager;
            this.optionsPanel = optionsPanel;
        }

        public Bindable<float> AddFloat(string name, float input)
            => this.variables.Add(name, input);

        public Bindable<int> AddInt(string name, int input)
            => this.variables.Add(name, input);

        public Bindable<bool> AddBool(string name, bool input)
            => this.variables.Add(name, input);

        public Bindable<Vector2> AddVector2(string name, Vector2 input)
            => this.variables.Add(name, input);

        public void AddOptionFloat(Bindable<float> variable, string name, int precision, Bindable<float> min, Bindable<float> max)
            => this.optionsPanel.AddOption(variable, name, precision, min, max, green);
        public void AddOptionFloat(Bindable<float> variable, string name, int precision, float min, Bindable<float> max)
            => this.optionsPanel.AddOption(variable, name, precision, min, max, green);
        public void AddOptionFloat(Bindable<float> variable, string name, int precision, Bindable<float> min, float max)
            => this.optionsPanel.AddOption(variable, name, precision, min, max, green);
        public void AddOptionFloat(Bindable<float> variable, string name, int precision, float min, float max)
            => this.optionsPanel.AddOption(variable, name, precision, min, max, green);

        public void AddOptionInt(Bindable<int> variable, string name, Bindable<int> min, Bindable<int> max)
            => this.optionsPanel.AddOption(variable, name, min, max, green);
        public void AddOptionInt(Bindable<int> variable, string name, int min, Bindable<int> max)
            => this.optionsPanel.AddOption(variable, name, min, max, green);
        public void AddOptionInt(Bindable<int> variable, string name, Bindable<int> min, int max)
            => this.optionsPanel.AddOption(variable, name, min, max, green);
        public void AddOptionInt(Bindable<int> variable, string name, int min, int max)
            => this.optionsPanel.AddOption(variable, name, min, max, green);

        public void AddOptionBool(Bindable<bool> variable, string name)
            => this.optionsPanel.AddOption(variable, name, green);

        public void AddOptionVector2(Bindable<Vector2> variable, string name)
        {
            this.optionsPanel.AddOption(variable, name, green);
            this.handleManager.CreateHandle(variable);
        }

        public float GetValueFloat(string name)
            => this.variables.Value<float>(name);

        public int GetValueInt(string name)
            => this.variables.Value<int>(name);

        public bool GetValueBool(string name)
            => this.variables.Value<bool>(name);

        public Vector2 GetValueVector2(string name)
            => this.variables.Value<Vector2>(name);

        public HitCircle AddHitCircle(Vector2 position, bool draw)
            => this.playfield.HitCircle(position, draw);
        public HitCircle AddHitCircle(Vector2 position)
            => this.playfield.HitCircle(position, true);

        public Slider AddSlider(CurveType curveType, object[] list, bool draw)
            => this.playfield.Slider(curveType, list.Cast<Vector2>().ToList(), draw);
        public Slider AddSlider(CurveType curveType, object[] list)
            => this.playfield.Slider(curveType, list.Cast<Vector2>().ToList(), true);

    }
}
