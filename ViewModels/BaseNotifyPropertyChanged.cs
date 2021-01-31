using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PasswordsManager.ViewModels
{
    public abstract class BaseNotifyPropertyChanged : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<string, object> _propertyValues;

        protected BaseNotifyPropertyChanged()
        {
            _propertyValues = new Dictionary<string, object>();
        }

        protected virtual T GetValue<T>([CallerMemberName] string propertyName = null, T defaultValue = default)
        {
            if (_propertyValues == null) return defaultValue;
            if (_propertyValues.ContainsKey(propertyName)) return (T)_propertyValues[propertyName];
            return defaultValue;
        }

        protected virtual bool SetValue<T>(T newValue, [CallerMemberName] string propertyName = null)
        {
            if (_propertyValues == null) return false;

            var current = GetValue<T>(propertyName);

            if ((current == null && newValue == null) ||
                (current != null && EqualityComparer<T>.Default.Equals(current, newValue)))
                return false;

            _propertyValues[propertyName] = newValue;
            OnPropertyChanged(propertyName);

            return true;
        }

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
