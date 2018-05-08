using System;

namespace OsuPlayground.Bindables
{
    public abstract class Bindable
    { }

    public class Bindable<T> : Bindable
    {
        private T value;

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

        public static implicit operator Bindable<T>(T other) => new Bindable<T>(other);

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