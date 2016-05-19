using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PlaqueData
{
    public class AncestorManager
    {
         private string database;
         public AncestorManager(string dbfile)
         {
            database = dbfile;
         }

         public List<Ancestor> GetAll(int cid)
        {
            var db = new SQLiteConnection(database);
            var query = from d in db.Table<Ancestor>()
                        where d.ContactId == cid
                        select d;
            var list = query.ToList();
            db.Close();
            return list;
        }

         public List<Ancestor> GetByName(Ancestor ance)
         {
             var db = new SQLiteConnection(database);
             var query = from l in db.Table<Ancestor>()
                         where l.ContactId == ance.ContactId
                                && l.Surname == ance.Surname 
                                && l.Name == ance.Name
                         select l;
             var list = query.ToList();
             db.Close();
             return list;
         }

         public void Delete(Ancestor ance)
         {
             var db = new SQLiteConnection(database);
             db.Delete(ance);
             db.Close();
         }

         public void SaveOrUpdate(Ancestor ance)
         {
             if (ance.Id == 0)
             {
                 var db = new SQLiteConnection(database);
                 db.Insert(ance);
                 db.Close();
             }
             else
             {
                 var db = new SQLiteConnection(database);
                 var query = from l in db.Table<Ancestor>()
                             where l.ContactId == ance.ContactId && l.Id == ance.Id
                             select l;
                 var list = query.ToList();
                 if (list.Count > 0)
                 {
                     list[0].Name = ance.Name;
                     list[0].Surname = ance.Surname;
                     list[0].IsPrint = ance.IsPrint;
                     db.Update(list[0]);
                 }
                 db.Close();
             }
         }

        public void DeleteByContact(Contact c)
         {
             var db = new SQLiteConnection(database);
             var query = "delete from Ancestor where Contactid = " + c.Id.ToString();
             db.Execute(query);             
             db.Close();
         }

        public void PrintAll(Contact c)
        {
            var db = new SQLiteConnection(database);
            var query = "update Ancestor set IsPrint=1 where Contactid = " + c.Id.ToString();
            db.Execute(query);
            db.Close();
        }

        public void RemovPrintAll(Contact c)
        {
            var db = new SQLiteConnection(database);
            var query = "update Ancestor set IsPrint=0 where Contactid = " + c.Id.ToString();
            db.Execute(query);
            db.Close();
        }

        public int GetPrintCount(Contact c)
        {
            var db = new SQLiteConnection(database);
            var query = from l in db.Table<Ancestor>()
                        where l.ContactId == c.Id && l.IsPrint
                        select l;
            var count = query.Count();
            db.Close();
            return count;
        }

        public int GetCount(Contact c)
        {
            var db = new SQLiteConnection(database);
            var query = from l in db.Table<Ancestor>()
                        where l.ContactId == c.Id
                        select l;
            var count = query.Count();
            db.Close();
            return count;
        }

        public List<int> GetPrintContactIds()
        {
            var db = new SQLiteConnection(database);
            var query = from l in db.Table<Ancestor>()
                        where l.IsPrint
                        select l;
            var list = query.ToList();
            db.Close();
            return list.Select(x=>x.ContactId).ToList();
        }

        public void Export(Contact c, string filename)
        {
            var all = GetAll(c.Id);
            var sw = new StreamWriter(filename, true);
            sw.Write(String.Join("\r\n", all.Select(x => x.GetExportString() + "\t" + c.Name + "\t" + c.Code)));
            sw.Write("\r\n");
            sw.Close();
        }

        public List<Ancestor> GetPrintLiveByContactId(int contactid)
        {
            var db = new SQLiteConnection(database);
            var query = from l in db.Table<Ancestor>()
                        where l.IsPrint && l.ContactId == contactid
                        select l;
            var list = query.ToList();
            db.Close();
            return list;
        }
        public List<Ancestor> GetByContactCount(ContactCount c)
        {
            return c.IsFull ? GetAll(c.Id) : GetPrintLiveByContactId(c.Id);
        }

        public void SetPrintDate(ContactCount c, string date)
        {
            if (c.IsFull)
            {
                var db = new SQLiteConnection(database);
                var query = "update Ancestor set LastPrint='" + date + "' where Contactid = " + c.Id;
                db.Execute(query);
                db.Close();
            }
            else
            {
                var db = new SQLiteConnection(database);
                var query = "update Ancestor set LastPrint='" + date + "' where IsPrint=1 and Contactid = " + c.Id;
                db.Execute(query);
                db.Close();
            }
        }

        public void UpdateContactId(int fromId, int ToId)
        {
            var db = new SQLiteConnection(database);
            var query = "update Ancestor set ContactId='" + ToId + "' where Contactid = " + fromId;
            db.Execute(query);
            db.Close();
        }
    }
}
