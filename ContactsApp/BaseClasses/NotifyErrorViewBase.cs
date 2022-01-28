using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ContactsApp.BaseClasses
{
    public abstract class NotifyErrorViewBase : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _propErrors;

        public bool HasErrors => _propErrors.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;


        public NotifyErrorViewBase()
        {
            _propErrors = new Dictionary<string, List<string>>();
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            return _propErrors.GetValueOrDefault(propertyName, null);
        }


        protected void AddError(string errMsg, [CallerMemberName] string propName = "")
        {
            if (!_propErrors.ContainsKey(propName))
            {
                _propErrors.Add(propName, new List<string>());
            }

            _propErrors[propName].Add(errMsg);
            OnErrorChanged(propName);
        }

        protected void RemoveError([CallerMemberName] String propName = "")
        {
            if (_propErrors.Remove(propName))
            {
                OnErrorChanged(propName);
            }
        }

        private void OnErrorChanged(string propName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propName));
        }
    }
}
