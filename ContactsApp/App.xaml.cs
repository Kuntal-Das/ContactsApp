using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    //StartupUri="ViewModel\MainView.xaml">

    public partial class App : Application
    {
        private static string DbName = "Contacts.db";
        private static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string DbPath = System.IO.Path.Combine(folderPath, DbName);
    }
}
