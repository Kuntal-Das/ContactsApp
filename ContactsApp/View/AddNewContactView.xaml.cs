using System.Windows;

namespace ContactsApp.View
{
    /// <summary>
    /// Interaction logic for AddNewContactView.xaml
    /// </summary>
    public partial class AddNewContactView : Window
    {
        public AddNewContactView()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
    }
}
