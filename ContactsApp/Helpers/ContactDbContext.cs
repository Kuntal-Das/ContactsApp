using ContactsApp.Models;
using SQLite;
using System;
using System.Collections.Generic;

namespace ContactsApp.Helpers
{
    public class ContactDbContext
    {
        private static string DbName = "Contacts.db";
        private static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static string DbPath = System.IO.Path.Combine(folderPath, DbName);

        private DateTime _lastFetched;
        private List<Contact> _contacts;
        private SQLiteConnection _conn;

        public List<Contact> Contacts
        {
            get
            {
                if (_lastFetched < DateTime.UtcNow.AddMinutes(5))
                {
                    GetContactsFromDb();
                }
                return _contacts;
            }
            set { _contacts = value; }
        }


        public ContactDbContext()
        {
            using (_conn = new SQLiteConnection(DbPath))
            {
                _conn.CreateTable<Contact>();
                Contacts = _conn.Table<Contact>().ToList();
            }
            _lastFetched = DateTime.UtcNow;
        }

        private void GetContactsFromDb()
        {
            using (_conn = new SQLiteConnection(DbPath))
            {
                Contacts = _conn.Table<Contact>().ToList();
            }
            _lastFetched = DateTime.UtcNow;
        }

        public int AddContact(Contact newContact)
        {
            int rowsAffected = 0;
            using (_conn = new(DbPath))
            {
                rowsAffected = _conn.Insert(newContact);
            }
            GetContactsFromDb();
            return rowsAffected;
        }

        public int UpdateContact(Contact newContact)
        {
            int rowsAffected = 0;
            using (_conn = new(DbPath))
            {
                rowsAffected = _conn.Update(newContact);
            }
            GetContactsFromDb();
            return rowsAffected;
        }

        public int DeleteContact(Contact contact)
        {
            int rowsAffected;
            using (_conn = new(DbPath))
            {
                rowsAffected = _conn.Delete(contact);
            }
            GetContactsFromDb();
            return rowsAffected;
        }
    }
}
