using ContactsApp.Command;
using ContactsApp.Models;
using SQLite;
using System.Windows;
using System.Windows.Input;

namespace ContactsApp.ViewModel
{
    public class AddNewContactViewModel
    {
        private Contact _contact;
        public Contact Contact
        {
            get { return _contact; }
            set { _contact = value; }
        }

        private ICommand _saveContactCommand;

        public ICommand SaveContactCommand
        {
            get
            {
                if (_saveContactCommand == null)
                    _saveContactCommand = new RelayCommand(SaveContactCommandExecute, CanSaveContactCommandExecute, false);
                return _saveContactCommand;
            }
        }

        private bool CanSaveContactCommandExecute(object window)
        {
            var props = typeof(Contact).GetProperties();

            foreach (var prop in props)
            {
                if (Contact[prop.Name] != null) return false;
            }
            return true;
        }

        private void SaveContactCommandExecute(object window)
        {
            using (SQLiteConnection connection = new(App.DbPath))
            {
                // if the table exists nextline has no effect
                connection.CreateTable<Contact>();

                connection.Insert(Contact);
            }
            ((Window)window).Close();
        }

        public AddNewContactViewModel()
        {
            Contact = new() { Name = "", Email = "", PhoneNo = "" };
        }
    }
}
