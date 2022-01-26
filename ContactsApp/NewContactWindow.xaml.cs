using ContactsApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

            string DbName = "Contacts.db";
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string DbPath = System.IO.Path.Combine(folderPath, DbName);

            using (SQLiteConnection connection = new SQLiteConnection(DbPath))
            {
                // if the table exists nextline has no effect
                connection.CreateTable<Contact>();

                connection.Insert(contact);
            }

            this.Close();
        }
    }
}
