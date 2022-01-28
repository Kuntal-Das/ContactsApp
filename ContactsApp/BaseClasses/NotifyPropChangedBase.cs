using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContactsApp.BaseClasses
{
    public abstract class NotifyPropChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChange([CallerMemberName] string propName = "")
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
