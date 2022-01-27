using ContactsApp.Models;
using System.Windows;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ReadDatabase();
        }

        private void newContactBtn_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new();
            newContactWindow.ShowDialog();

            ReadDatabase();
        }

        void ReadDatabase()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DbPath))
            {
                conn.CreateTable<Contact>();
                var contacts = conn.Table<Contact>().ToList();
            }
        }
    }
}
