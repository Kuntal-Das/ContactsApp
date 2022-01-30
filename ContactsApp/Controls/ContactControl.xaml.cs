using ContactsApp.Models;
using System.Windows;
using System.Windows.Controls;

namespace ContactsApp.Controls
{
    /// <summary>
    /// Interaction logic for ContactControl.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {
        public ContactControl()
        {
            InitializeComponent();
        }

        public Contact Contact
        {
            get { return (Contact)GetValue(ContactProperty); }
            set { SetValue(ContactProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contact.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContactProperty =
            DependencyProperty.Register("Contact", typeof(Contact), typeof(ContactControl), new PropertyMetadata(null, OnDpPropChange));

        private static void OnDpPropChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContactControl contactControl = d as ContactControl;

            if (contactControl == null) return;

            contactControl.NameTxtBx.Text = (e.NewValue as Contact).Name;
            contactControl.EmailTxtBx.Text = (e.NewValue as Contact).Email;
            contactControl.PhoneNTxtBx.Text = (e.NewValue as Contact).PhoneNo;
        }
    }
}
