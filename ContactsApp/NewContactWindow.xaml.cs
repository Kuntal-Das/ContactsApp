using ContactsApp.Models;
using SQLite;
using System.Windows;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        public NewContactWindow()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Save Contact
            Contact contact = new Contact()
            {
                Name = nameTxtBx.Text.Trim(),
                Email = emailTxtBx.Text.Trim(),
                PhoneNo = phoneTxtBx.Text.Trim()
            };

            using (SQLiteConnection connection = new SQLiteConnection(App.DbPath))
            {
                // if the table exists nextline has no effect
                connection.CreateTable<Contact>();

                connection.Insert(contact);
            }

            this.Close();
        }
    }
}
