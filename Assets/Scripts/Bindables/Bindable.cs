using System;

namespace OsuPlayground.Bindables
{
    public abstract class Bindable
    { }

    /// <summary>
    /// A class for making globally accessible values from immutable underlying types.
    /// </summary>
    public class Bindable<T> : Bindable
    {
        private T value;

        /// <summary>
        /// The value of this <see cref="Bindable{T}"/>.
        /// </summary>
        public virtual T Value
        {
            get { return this.value; }
            set
            {
                this.value = value;

                this.TriggerValueChange();
            }
        }

        private event Action<T> valueChanged;
        /// <summary>
        /// An event fired whenever <see cref="Value"/> is changed.
        /// </summary>
        public event Action<T> ValueChanged
        {
            add
            {
                valueChanged += value;
                this.TriggerValueChange();
            }
            remove
            {
                valueChanged -= value;
            }
        }

        /// <summary>
        /// Converts a value into a <see cref="Bindable{T}"/> representing that value.
        /// </summary>
        public static implicit operator Bindable<T>(T other) => new Bindable<T>(other);

        /// <summary>
        /// Creates a <see cref="Bindable{T}"/> with a specified value.
        /// </summary>
        public Bindable(T value)
        {
            this.value = value;
        }

        private void TriggerValueChange()
        {
            if (valueChanged != null)
            {
                valueChanged.Invoke(this.value);
            }
        }
    }
}