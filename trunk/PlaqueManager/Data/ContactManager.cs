using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PlaqueData
{
    public class ContactManager
    {
        private string database;

        public ContactManager(string dbfile)
        {
            database = dbfile;
        }

        public List<Contact> GetByName(string name)
        {
            var db = new SQLiteConnection(database);
            var query = from c in db.Table<Contact>()
                        where c.Name.Contains(name)
                        orderby c.Code
                        select c;
            var list = query.ToList();
            db.Close();
            return list;
        }

        public List<Contact> GetByCode(string code)
        {
            var db = new SQLiteConnection(database);
            var query = from c in db.Table<Contact>()
                        where c.Code.Contains(code)
                        orderby c.Code
                        select c;
            var list = query.ToList();
            db.Close();
            return list;
        }

        public List<Contact> GetByExactNameAndCode(string name, string code)
        {
            var db = new SQLiteConnection(database);
            var query = from c in db.Table<Contact>()
                        where c.Code==code && c.Name == name
                        select c;
            var list = query.ToList();
            db.Close();
            return list;
        }

        public Contact CreateContact(string name, string code, bool isprint)
        {
            var contact = new Contact { Name = name, Code = code, IsPrint = isprint };
            var db = new SQLiteConnection(database);
            db.Insert(contact);
            db.Close();
            return contact;
        }

        public Contact GetById(int id)
        {
            var db = new SQLiteConnection(database);
            var c = db.Get<Contact>(id);
            db.Close();
            return c;
        }

        public List<Contact> GetAllContacts()
        {
            var db = new SQLiteConnection(database);
            var query = from c in db.Table<Contact>()
                        orderby c.Code
                        select c;
            var list = query.ToList();
            db.Close();
            return list;
        }

        #region Contact life cycle
        public void UpdateContact(Contact c)
        {
            var db = new SQLiteConnection(database);
            db.Update(c);
            db.Close();
        }

        public void DeleteContact(Contact c)
        {
            var db = new SQLiteConnection(database);
            var contact = this.GetById(c.Id);
            if (contact != null) db.Delete(contact);
            db.Close();
        }

        public Contact GetNewContact()
        {
            var contact = new Contact { Name = "Test", Code = "Z999", IsPrint = true };
            var db = new SQLiteConnection(database);
            db.Insert(contact);
            db.Close();
            return contact;
        }

        public List<Contact> SearchContact(string txt, int option)
        {
            return (option == 0) ? GetByName(txt) : GetByCode(txt);
        }

        public void SetContactToPrint(Contact contact)
        {
            var c = GetById(contact.Id);
            if (c != null)
            {
                c.IsPrint = true;
                UpdateContact(c);
            }
        }

        public void RemoveContactFromPrint(Contact contact)
        {
            var c = GetById(contact.Id);
            if (c != null)
            {
                c.IsPrint = false;
                UpdateContact(c);
            }
        }
        #endregion

        #region Outpu and Report

        public List<Contact> GetContactPrintList()
        {
            var db = new SQLiteConnection(database);
            var query = from c in db.Table<Contact>()
                        where c.IsPrint
                        select c;
            var list = query.ToList();
            db.Close();
            return list;
        }

        public void UpdateLastPrint(string date, int contactId)
        {
            var db = new SQLiteConnection(database);
            var query = String.Format("update Contact set LastPrint='{0}' where Id = " + contactId, date);
            db.Execute(query);
            db.Close();
        }
        #endregion
    }
}
