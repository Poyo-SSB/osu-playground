using OsuPlayground.Scripting;
using OsuPlayground.UI;
using OsuPlayground.UI.Panels;
using SFB;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace OsuPlayground
{
    /// <summary>
    /// The main controller for the application.
    /// </summary>
    public class Playground : MonoBehaviour
    {
        public static GameObject ToolbarButtonPrefab;

        public static GameObject OptionNumberPrefab;
        public static GameObject OptionBoolPrefab;
        public static GameObject OptionVector2Prefab;

        public static GameObject DrawableHitCirclePrefab;
        public static GameObject DrawableSliderPrefab;

        public static GameObject HandlePrefab;

        [Header("Prefabs")]
        [SerializeField]
        private GameObject toolbarButtonPrefab;

        [SerializeField]
        private GameObject optionNumberPrefab;
        [SerializeField]
        private GameObject optionBoolPrefab;
        [SerializeField]
        private GameObject optionVector2Prefab;

        [SerializeField]
        private GameObject drawableHitCirclePrefab;
        [SerializeField]
        private GameObject drawableSliderPrefab;

        [SerializeField]
        private GameObject handlePrefab;

        [Header("References")]
        [SerializeField]
        private ScriptManager scriptManager;
        [SerializeField]
        private OptionsPanel optionsPanel;
        [SerializeField]
        private ToolbarPanel toolbarPanel;
        [SerializeField]
        private PlayfieldPanel playfieldPanel;
        [SerializeField]
        private ErrorPanel errorPanel;
        [SerializeField]
        private Playfield playfield;

        private void Awake()
        {
            ToolbarButtonPrefab = this.toolbarButtonPrefab;
            OptionNumberPrefab = this.optionNumberPrefab;
            OptionBoolPrefab = this.optionBoolPrefab;
            OptionVector2Prefab = this.optionVector2Prefab;
            DrawableHitCirclePrefab = this.drawableHitCirclePrefab;
            DrawableSliderPrefab = this.drawableSliderPrefab;
            HandlePrefab = this.handlePrefab;

            this.scriptManager = FindObjectOfType<ScriptManager>();
        }

        private void Start()
        {
            if (!String.IsNullOrWhiteSpace(this.scriptManager.Error))
            {
                this.errorPanel.Text.text = this.scriptManager.Error;
                this.errorPanel.gameObject.SetActive(true);
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(this.scriptManager.CurrentPath))
                {
                    this.scriptManager.Load();
                    this.toolbarPanel.Text.text = Path.GetFileName(this.scriptManager.CurrentPath);
                }
            }

            // Blue.
            var optionColor = new Color(0.184313725f, 0.631372549f, 0.839215686f);
            this.optionsPanel.AddOption(Options.CircleSize, "Circle size", 1, 0, 10, optionColor);
            this.optionsPanel.AddOption(Options.SliderMultiplier, "Slider multiplier", 2, 0.4f, 3.6f, optionColor);
            this.optionsPanel.AddOption(Options.SpeedMultiplier, "Speed multiplier", 2, 0.1f, 4, optionColor);
            this.optionsPanel.AddOption(Options.TickRate, "Tick rate", 1, 4, optionColor);
            this.optionsPanel.AddOption(Options.BeatSnapDivisor, "Beat snap", 1, 16, optionColor);

            this.toolbarPanel.AddButton("Load file", this.LoadFile, ToolbarSide.Left);
            this.toolbarPanel.AddButton("Reload", this.Reload, ToolbarSide.Left);
            this.toolbarPanel.AddButton("Copy code", this.CopyCode, ToolbarSide.Left);
            this.toolbarPanel.AddButton("Reset zoom", this.ResetZoom, ToolbarSide.Left);

            this.toolbarPanel.AddButton("Hide", this.ToggleOptions, ToolbarSide.Right);
            this.toolbarPanel.AddButton("Open wiki", this.OpenWiki, ToolbarSide.Right);

            this.scriptManager.StartFunction();
        }

        private void Update() => this.scriptManager.UpdateFunction();

        private void LoadFile(Button button)
        {
            // Unity does not provide a way to open a file prompt, so I had to use a library to do it.
            // I will admit that the library is wonderful.
            var files = StandaloneFileBrowser.OpenFilePanel("Load file", String.Empty, new ExtensionFilter[]
            {
                new ExtensionFilter("osu!Playground scripts", "oss", "js" ),
                new ExtensionFilter("All Files", "*" ),
            }, false);

            if (files == null || files.Length < 1)
            {
                return;
            }

            this.scriptManager.Reload(files[0]);
        }

        private void Reload(Button button)
        {
            if (String.IsNullOrWhiteSpace(this.scriptManager.CurrentPath))
            {
                return;
            }

            this.scriptManager.Reload(this.scriptManager.CurrentPath);
        }

        private void CopyCode(Button button) => GUIUtility.systemCopyBuffer = this.playfield.LatestHitObjects;

        private void ResetZoom(Button button)
        {
            this.playfieldPanel.RawOffset = Vector2.zero;
            this.playfieldPanel.RawZoom = 0;
        }

        private void ToggleOptions(Button button)
        {
            Options.ShowOptions.Value = !Options.ShowOptions.Value;

            button.GetComponentInChildren<Text>().text = Options.ShowOptions.Value ? "Hide" : "Show";
        }

        private void OpenWiki(Button button) => Application.OpenURL("https://github.com/Poyo-SSB/osu-playground/wiki");
    }
}
