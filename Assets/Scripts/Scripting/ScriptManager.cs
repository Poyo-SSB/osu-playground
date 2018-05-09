using Jint;
using OsuPlayground.Bindables;
using OsuPlayground.UI;
using OsuPlayground.UI.Handles;
using OsuPlayground.UI.Panels;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OsuPlayground.Scripting
{
    /// <summary>
    /// Controls the loading of scripts.
    /// </summary>
    public class ScriptManager : MonoBehaviour
    {
        private string baseCode;

        /// <summary>
        /// The path to the last script which was attempted to be loaded.
        /// </summary>
        public string CurrentPath;

        /// <summary>
        /// If not empty, an error message which describes errors in the file located at <see cref="CurrentPath"/>.
        /// </summary>
        public string Error;

        public Action StartFunction = () => { return; };
        public Action UpdateFunction = () => { return; };

        private void Awake()
        {
            // There can only be one.
            if (FindObjectsOfType<ScriptManager>().Length > 1)
            {
                Destroy(this.gameObject);
            }
            else
            {
                DontDestroyOnLoad(this.gameObject);
            }

            // Load utility code for injection into user scripts.
            this.baseCode = ((TextAsset)Resources.Load("framework")).text;
        }

        public void Reload(string path)
        {
            // Reload the scene to reset everything quickly because I am lazy.
            this.CurrentPath = path;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        public void ReloadError(string error)
        {
            this.StartFunction = () => { return; };
            this.UpdateFunction = () => { return; };
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            this.Error = error;
        }

        public void Load()
        {
            var code = File.ReadAllText(this.CurrentPath);

            // Create JavaScript engine and allow access to relevant libraries.
            var engine = new Engine(cfg => cfg.AllowClr(typeof(Vector2).Assembly, typeof(Playground).Assembly));

            // Allow user to do things.
            engine.SetValue("Playground", new ScriptUtilites(
                new BindableList(),
                FindObjectOfType<Playfield>(),
                FindObjectOfType<HandleManager>(),
                FindObjectOfType<OptionsPanel>()));

            try
            {
                engine.Execute(this.baseCode + code);
            }
            catch (Exception e)
            {
                // In the event of a parse error or something, tell the user.
                this.ReloadError("Error while loading: " + e.Message);
                return;
            }

            this.StartFunction = () => engine.Invoke("start");
            this.UpdateFunction = () => engine.Invoke("update");
        }
    }
}
