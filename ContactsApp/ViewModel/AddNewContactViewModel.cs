using ContactsApp.Command;
using ContactsApp.Models;
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ContactsApp.ViewModel
{
    public class AddNewContactViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange([CallerMemberName] string propName = "")
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private Contact? _contact;
        public Contact Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
                OnPropertyChange();
            }
        }

        private ICommand? _saveContactCommand;

        public ICommand SaveContactCommand
        {
            get
            {
                if (_saveContactCommand == null)
                    _saveContactCommand = new RelayCommand(SaveContactCommandExecute, CanSaveContactCommandExecute, false);
                return _saveContactCommand;
            }
        }

        private bool CanSaveContactCommandExecute(object arg)
        {
            return Contact.Error == null;
        }

        private void SaveContactCommandExecute(object obj)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DbPath))
            {
                // if the table exists nextline has no effect
                connection.CreateTable<Contact>();

                connection.Insert(Contact);
            }
            ((Window)obj).Close();
        }

        public AddNewContactViewModel()
        {
            Contact = new() { Name = "", Email = "", PhoneNo = "" };
        }
    }
}
