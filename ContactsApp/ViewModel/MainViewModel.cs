using ContactsApp.BaseClasses;
using ContactsApp.Models;
using System.Collections.ObjectModel;

namespace ContactsApp.ViewModel
{
    class MainViewModel : NotifyPropChangedBase
    {
        private ObservableCollection<Contact> _contacts;
        public ObservableCollection<Contact> Contacts
        {
            get { return _contacts; }
            private set { _contacts = value; }
        }

        public void AddContact(Contact newContact)
        {
            Contacts.Add(newContact);
        }



    }
}
