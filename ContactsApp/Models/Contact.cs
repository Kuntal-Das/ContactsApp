using ContactsApp.BaseClasses;
using SQLite;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ContactsApp.Models
{
    public class Contact : NotifyPropChangedBase, IDataErrorInfo
    {
        private static readonly string emailPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        [MaxLength(50)]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChange();
            }
        }

        private string _email;
        [Unique]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChange();
            }
        }

        private string _phoneNo;
        [Unique]
        public string PhoneNo
        {
            get { return _phoneNo; }
            set
            {
                _phoneNo = value;
                OnPropertyChange();
            }
        }

        public string Error
        {
            get
            {
                var props = typeof(Contact).GetProperties();

                foreach (var prop in props)
                {
                    if (this[prop.Name] != null)
                        return this[prop.Name];
                }
                return "";
            }
        }

        public string this[string propName]
        {
            get
            {
                string result = null;
                switch (propName)
                {
                    case "Name":
                        if (Name.Length <= 3)
                            result = "Name must more that 3 characters";
                        break;
                    case "Email":
                        if (Email.Length <= 3 || !Regex.IsMatch(Email, emailPattern, RegexOptions.IgnoreCase))
                            result = "Not a valid Email";
                        break;
                    case "PhoneNo":
                        if (PhoneNo.Length != 10)
                            result = "Phone number should be of 10 numerics";
                        else if (!long.TryParse(PhoneNo, out long temp) || temp <= 0)
                            result = "Phone number should be numeric and greater than zero";
                        break;
                }
                return result;
            }
        }

        public override string ToString()
        {
            return $"{Name}\t{PhoneNo}\t{Email}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is null or not Contact) return false;

            var otherContact = obj as Contact;
            return Id.Equals(otherContact.Id) &&
                Name.Equals(otherContact.Name, StringComparison.OrdinalIgnoreCase) &&
                PhoneNo.Equals(otherContact.PhoneNo) &&
                Email.Equals(otherContact.Email);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, PhoneNo, Email);
        }
    }
}
