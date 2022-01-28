using ContactsApp.Models;
using System.Collections.Generic;
using System.Windows;

namespace ContactsApp.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            ReadDatabase();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewContactView newContactWindow = new();
            newContactWindow.ShowDialog();

            ReadDatabase();
        }
        private void ReadDatabase()
        {
            List<Contact> contacts;
            using (SQLite.SQLiteConnection conn = new(App.DbPath))
            {
                conn.CreateTable<Contact>();
                contacts = conn.Table<Contact>().ToList();
            }
            ContactsListView.ItemsSource = contacts;
        }
    }
}
