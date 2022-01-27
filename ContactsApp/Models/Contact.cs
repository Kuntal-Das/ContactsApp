using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContactsApp.Models
{
    public class Contact : INotifyPropertyChanged, IDataErrorInfo
    {

        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChange(); }
        }

        private string _name;
        [MaxLength(50)]
        public string Name
        {
            get { return _name.Trim(); }
            set { _name = value; OnPropertyChange(); }
        }

        private string _email;
        [Unique]
        public string Email
        {
            get { return _email.Trim(); }
            set { _email = value; OnPropertyChange(); }
        }

        private string _phoneNo;
        [Unique]
        public string PhoneNo
        {
            get { return _phoneNo.Trim(); }
            set { _phoneNo = value; OnPropertyChange(); }
        }

        public string Error => null;
        public string this[string propName]
        {
            get
            {
                string result = null;
                switch (propName)
                {
                    case "Name":
                        if (string.IsNullOrWhiteSpace(Name))
                            result = "Name is required";
                        break;
                    case "Email":
                        if (string.IsNullOrWhiteSpace(Email))
                            result = "email is required";
                        break;
                    case "PhoneNo":
                        if (string.IsNullOrWhiteSpace(PhoneNo))
                            result = "phone no. is required";
                        break;
                }
                return result;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange([CallerMemberName] string propName = "")
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
