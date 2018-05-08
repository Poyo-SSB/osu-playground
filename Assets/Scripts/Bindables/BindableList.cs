using System;
using System.Collections;
using System.Collections.Generic;

namespace OsuPlayground.Bindables
{
    public class BindableList : IEnumerable
    {
        private Dictionary<string, Bindable> bindables = new Dictionary<string, Bindable>();

        public IEnumerator GetEnumerator() => ((IEnumerable)this.bindables).GetEnumerator();

        public Bindable<T> Add<T>(string name, T value)
        {
            var newBindable = new Bindable<T>(value);
            bindables.Add(name, newBindable);
            return newBindable;
        }

        public Bindable<T> Get<T>(string name) => (Bindable<T>)bindables[name];

        public T Value<T>(string name) => Get<T>(name).Value;
    }
}
