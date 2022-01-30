using ContactsApp.Models;
using ContactsApp.ViewModel;
using System.Windows;

namespace ContactsApp.View
{
    /// <summary>
    /// Interaction logic for ContactDetailsView.xaml
    /// </summary>
    public partial class ContactDetailsView : Window
    {
        public ContactDetailsView(Contact contact)
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            DataContext = new ContactDetailsViewModel(contact);
        }
    }
}
