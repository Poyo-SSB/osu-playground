using System.Collections;
using System.Collections.Generic;

namespace OsuPlayground.Bindables
{
    /// <summary>
    /// A utility class to avoid having to use a <see cref="Dictionary{TKey, TValue}"/> directly.
    /// </summary>
    public class BindableList : IEnumerable
    {
        private Dictionary<string, Bindable> bindables = new Dictionary<string, Bindable>();

        public IEnumerator GetEnumerator() => ((IEnumerable)this.bindables).GetEnumerator();

        /// <summary>
        /// Creates a new <see cref="Bindable{T}"/> and adds it to the list.
        /// </summary>
        /// <param name="name">The name of the new <see cref="Bindable{T}"/>.</param>
        /// <param name="value">The value of the new <see cref="Bindable{T}"/>.</param>
        /// <returns>The <see cref="Bindable{T}"/> created by this operation.</returns>
        public Bindable<T> Add<T>(string name, T value)
        {
            var newBindable = new Bindable<T>(value);
            this.bindables.Add(name, newBindable);
            return newBindable;
        }

        /// <summary>
        /// Gets a <see cref="Bindable{T}"/> by its name.
        /// </summary>
        /// <param name="name">The name of the <see cref="Bindable{T}"/> to return.</param>
        /// <returns>The found <see cref="Bindable{T}"/>.</returns>
        public Bindable<T> Get<T>(string name) => (Bindable<T>)this.bindables[name];

        /// <summary>
        /// Gets the value of a <see cref="Bindable{T}"/> by its name.
        /// </summary>
        /// <param name="name">The name of the <see cref="Bindable{T}"/> to return the value of.</param>
        /// <returns>The value of the found <see cref="Bindable{T}"/>.</returns>
        public T Value<T>(string name) => Get<T>(name).Value;
    }
}
