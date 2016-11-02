using System;
using System.ComponentModel;

namespace Mvvm
{
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        private static float _floatEpsilon = 0.00001f;
        private static double _doubleEpsilon = 0.00001d;

        /// <summary>
        /// Reset the epsilon of float type for the SetProperty method.
        /// </summary>
        /// <param name="epsilon">default value is 0.00001 for WP7 compatible</param>
        public static void SetFloatEpsilon(float epsilon)
        {
            _floatEpsilon = epsilon;
        }

        /// <summary>
        /// Reset the epsilon of double type for the SetProperty method.
        /// </summary>
        /// <param name="epsilon">default value is 0.00001 for WP7 compatible</param>
        public static void SetDoubleEpsilon(double epsilon)
        {
            _doubleEpsilon = epsilon;
        }

        /// <summary>
        /// Set new value to the property and fire events if the propertyNames is valid.
        /// </summary>
        /// <param name="property">The field</param>
        /// <param name="value">The value</param>
        /// <param name="propertyNames">Any property name(s) to fire PropertyChanged event(s).</param>
        public void SetProperty(ref float property, float value, params string[] propertyNames)
        {
            if (Math.Abs(property - value) < _floatEpsilon)
            {
                return;
            }

            property = value;
            FireNotifyPropertyChangedEvents(propertyNames);
        }

        /// <summary>
        /// Set new value to the property and fire events if the propertyNames is valid.
        /// </summary>
        /// <param name="property">The field</param>
        /// <param name="value">The value</param>
        /// <param name="propertyNames">Any property name(s) to fire PropertyChanged event(s).</param>
        public void SetProperty(ref double property, double value, params string[] propertyNames)
        {
            if (Math.Abs(property - value) < _doubleEpsilon)
            {
                return;
            }

            property = value;
            FireNotifyPropertyChangedEvents(propertyNames);
        }

        /// <summary>
        /// Set new string to the property and fire events if the propertyNames is valid.
        /// </summary>
        /// <param name="property">The field</param>
        /// <param name="value">The value</param>
        /// <param name="propertyNames">Any property name(s) to fire PropertyChanged event(s).</param>
        public void SetProperty(ref string property, string value, params string[] propertyNames)
        {
            if (property == null && value == null)
            {
                return;
            }

            if (property != null && value != null && property.Equals(value))
            {
                return;
            }

            property = value;
            FireNotifyPropertyChangedEvents(propertyNames);
        }

        /// <summary>
        /// Set new string to the property and fire events if the propertyNames is valid.
        /// </summary>
        /// <param name="property">The field</param>
        /// <param name="value">The value</param>
        /// <param name="comparisonType">StringComparison</param>
        /// <param name="propertyNames">Any property name(s) to fire PropertyChanged event(s).</param>
        public void SetProperty(ref string property, string value, StringComparison comparisonType, params string[] propertyNames)
        {
            if (property == null && value == null)
            {
                return;
            }

            if (property != null && value != null && property.Equals(value, comparisonType))
            {
                return;
            }

            property = value;
            FireNotifyPropertyChangedEvents(propertyNames);
        }

        /// <summary>
        /// Set new value to the property and fire events if the propertyNames is valid.
        /// </summary>
        /// <param name="property">The field</param>
        /// <param name="value">The value</param>
        /// <param name="propertyNames">Any property name(s) to fire PropertyChanged event(s).</param>
        public void SetProperty<T>(ref T property, T value, params string[] propertyNames)
        {
            if (typeof(T).IsValueType)
            {
                if (property.Equals(value))
                {
                    return;
                }
            }
            else
            {
                if (ReferenceEquals(property, value))
                {
                    return;
                }
            }

            property = value;
            FireNotifyPropertyChangedEvents(propertyNames);
        }

        private void FireNotifyPropertyChangedEvents(params string[] propertyNames)
        {
            if (propertyNames == null)
            {
                return;
            }

            foreach (var propertyName in propertyNames)
            {
                if (propertyName != null)
                {
                    OnPropertyChanged(propertyName);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
