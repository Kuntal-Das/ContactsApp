using ContactsApp.Command;
using ContactsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ContactsApp.ViewModel
{
    class ContactDetailsViewModel
    {
        public Contact Contact { get; set; }

        private ICommand _updateContactCommand;

        public ICommand UpdateContactCommand
        {
            get
            {
                if (_updateContactCommand == null)
                    _updateContactCommand = new RelayCommand(UpdateContactCommandExecute, CanUpdateContactCommandExecute, false);
                return _updateContactCommand;
            }
        }

        private bool CanUpdateContactCommandExecute(object window)
        {
            return string.IsNullOrWhiteSpace(Contact.Error);
        }

        private void UpdateContactCommandExecute(object window)
        {
            App.ContactDbContext.UpdateContact(Contact);

            if (window != null) (window as Window).Close();
        }

        private ICommand _deleteContactCommand;

        public ICommand DeleteContactCommand
        {
            get
            {
                if (_deleteContactCommand == null)
                    _deleteContactCommand = new RelayCommand(DeleteContactCommandExecute, CanDeleteContactCommandExecute, false);
                return _deleteContactCommand;
            }
        }

        private bool CanDeleteContactCommandExecute(object arg)
        {
            return true;
        }

        private void DeleteContactCommandExecute(object window)
        {
            App.ContactDbContext.DeleteContact(Contact);

            if (window != null) (window as Window).Close();
        }

        public ContactDetailsViewModel(Contact contact)
        {
            Contact = contact;
        }
    }
}
