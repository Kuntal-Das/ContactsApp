using ContactsApp.Models;
using System;
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
            ContactsListView.ItemsSource = App.ContactDbContext.Contacts;
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox searchTextBox = (TextBox)sender;

            var filteredList = (from contact in _contacts
                                where contact.Name.Contains(searchTextBox.Text, StringComparison.OrdinalIgnoreCase)
                                orderby contact.Name ascending
                                select contact).ToList();


            //_contacts.Where(contact => contact.Name.Contains(searchTextBox.Text, System.StringComparison.OrdinalIgnoreCase)).ToList();

            ContactsListView.ItemsSource = filteredList;
        }

        private void ContactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact selectedContact = (Contact)ContactsListView.SelectedItem;
            if (selectedContact != null)
            {
                ContactDetailsView contactDetailsView = new(selectedContact);
                contactDetailsView.ShowDialog();

                ReadDatabase();
            }
        }
    }
}
