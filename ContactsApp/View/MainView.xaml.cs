using ContactsApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ContactsApp.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private List<Contact> _contacts;
        public MainView()
        {
            InitializeComponent();

            _contacts = new();
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
            using (SQLite.SQLiteConnection conn = new(App.DbPath))
            {
                conn.CreateTable<Contact>();
                _contacts = conn.Table<Contact>().ToList();
            }
            ContactsListView.ItemsSource = _contacts;
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox searchTextBox = (TextBox)sender;

            var filteredList = _contacts
                .Where(contact => contact.Name.Contains(searchTextBox.Text,System.StringComparison.OrdinalIgnoreCase))
                .ToList();

            ContactsListView.ItemsSource = filteredList;
        }
    }
}
