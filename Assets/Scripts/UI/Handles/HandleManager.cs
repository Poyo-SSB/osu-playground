using OsuPlayground.Bindables;
using System.Collections.Generic;
using UnityEngine;

namespace OsuPlayground.UI.Handles
{
    public class HandleManager : MonoBehaviour
    {
        [HideInInspector]
        public RectTransform RectTransform;

        private List<Handle> handles = new List<Handle>();

        private void Awake() => this.RectTransform = this.GetComponent<RectTransform>();

        public void CreateHandle(Bindable<Vector2> variable)
        {
            var handle = Instantiate(Playground.HandlePrefab, this.transform).GetComponent<Handle>();
            handle.Bound = variable;
            handle.Manager = this;

            this.handles.Add(handle);
        }

        private void Update()
        {
            foreach (var handle in this.handles)
            {
                handle.UpdatePosition(this.RectTransform.rect.width / Playfield.WIDTH);
            }
        }
    }
}
