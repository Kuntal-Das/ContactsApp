using ContactsApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        private SQLiteAsyncConnection _connAsync;

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
        private async Task GetContactsFromDbAsync()
        {
            try
            {
                _connAsync = new(DbPath);
                Contacts = await _connAsync.Table<Contact>().ToListAsync();
                _lastFetched = DateTime.UtcNow;
            }
            finally
            {
                await _connAsync.CloseAsync();
            }
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
        public async Task<int> AddContactAsync(Contact newContact)
        {
            int rowsAffected = 0;
            try
            {
                _connAsync = new SQLiteAsyncConnection(DbPath);
                rowsAffected = await _connAsync.InsertAsync(newContact);
                await GetContactsFromDbAsync();
            }
            finally
            {
                await _connAsync.CloseAsync();
            }
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
        public async Task<int> UpdateContactAsync(Contact newContact)
        {
            int rowsAffected = 0;
            try
            {
                _connAsync = new(DbPath);
                rowsAffected = await _connAsync.UpdateAsync(newContact);
                await GetContactsFromDbAsync();
            }
            finally
            {
                await _connAsync.CloseAsync();
            }
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
        public async Task<int> DeleteContactAsync(Contact contact)
        {
            int rowsAffected = 0;
            try
            {
                _connAsync = new(DbPath);
                rowsAffected = await _connAsync.DeleteAsync(contact);
                await GetContactsFromDbAsync();
            }
            finally
            {
                await _connAsync.CloseAsync();
            }
            return rowsAffected;
        }

    }
}
