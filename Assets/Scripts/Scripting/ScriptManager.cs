using Jint;
using Jint.Runtime;
using Jint.Runtime.Interop;
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
    public class ScriptManager : MonoBehaviour
    {
        private BindableList variables;

        private string baseCode;
        private ScriptUtilites utilities;

        public string CurrentPath;
        public string Error;

        public Action StartFunction = () => { return; };
        public Action UpdateFunction = () => { return; };

        private void Awake()
        {
            if (FindObjectsOfType<ScriptManager>().Length > 1)
            {
                Destroy(this.gameObject);
            }
            else
            {
                DontDestroyOnLoad(this.gameObject);
            }

            this.baseCode = ((TextAsset)Resources.Load("framework")).text;
        }

        public void Reload(string path)
        {
            this.CurrentPath = path;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        public void Load()
        {
            this.variables = new BindableList();
            this.utilities = new ScriptUtilites(
                this.variables,
                FindObjectOfType<Playfield>(),
                FindObjectOfType<HandleManager>(),
                FindObjectOfType<OptionsPanel>());

            var code = File.ReadAllText(this.CurrentPath);

            var engine = new Engine(cfg => cfg.AllowClr(typeof(Vector2).Assembly, typeof(Playground).Assembly));

            engine.SetValue("Playground", this.utilities);

            try
            {
                engine.Execute(baseCode + code);
            }
            catch (Exception e)
            {
                var nowPath = this.CurrentPath;
                StartFunction = () => { return; };
                UpdateFunction = () => { return; };
                this.Reload(String.Empty);
                this.Error = e.GetType() + " " + e.Message;
                this.CurrentPath = nowPath;
                return;
            }

            this.StartFunction = () => engine.Invoke("start");
            this.UpdateFunction = () => engine.Invoke("update");
        }
    }
}
