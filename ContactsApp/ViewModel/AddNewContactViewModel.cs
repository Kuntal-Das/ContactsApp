using ContactsApp.Command;
using ContactsApp.Models;
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
            return string.IsNullOrWhiteSpace(Contact.Error);
        }

        private void SaveContactCommandExecute(object window)
        {
            App.ContactDbContext.AddContact(Contact);
            ((Window)window).Close();
        }

        public AddNewContactViewModel()
        {
            Contact = new() { Name = "", Email = "", PhoneNo = "" };
        }
    }
}
