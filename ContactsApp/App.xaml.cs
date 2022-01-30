using ContactsApp.Helpers;
using System.Windows;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    //StartupUri="ViewModel\MainView.xaml">

    public partial class App : Application
    {
        public static ContactDbContext ContactDbContext = new();
    }
}
