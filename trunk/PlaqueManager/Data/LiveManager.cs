using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PlaqueData
{
    public class LiveManager
    {
        private string database;
        public LiveManager(string dbfile)
        {
            database = dbfile;
        }

        public List<Live> GetAll(int cid)
        {
            var db = new SQLiteConnection(database);
            var query = from l in db.Table<Live>()
                        where l.ContactId == cid
                        orderby l.Name
                        select l;
            var list = query.ToList();
            db.Close();
            return list;
        }

        public List<Live> GetByName(Live live)
        {
            var db = new SQLiteConnection(database);
            var query = from l in db.Table<Live>()
                        where l.ContactId == live.ContactId && l.Name == live.Name
                        select l;
            var list = query.ToList();
            db.Close();
            return list;
        }

        public void Delete(Live live)
        {
            var db = new SQLiteConnection(database);
            db.Delete(live);
            db.Close();
        }

        public void SaveOrUpdate(Live live)
        {
            if (live.Id == 0)
            {
                var db = new SQLiteConnection(database);
                db.Insert(live);
                db.Close();
            }
            else
            {
                var db = new SQLiteConnection(database);
                var query = from l in db.Table<Live>()
                            where l.ContactId == live.ContactId && l.Id == live.Id
                            select l;
                var list = query.ToList();
                if (list.Count > 0)
                {
                    list[0].Name = live.Name;
                    list[0].IsPrint = live.IsPrint;
                    db.Update(list[0]);
                }
                db.Close();
            }
        }

        public void DeleteByContact(Contact c)
        {
            var db = new SQLiteConnection(database);
            var query = "delete from Live where Contactid = " + c.Id.ToString();
            db.Execute(query);
            db.Close();
        }

        public void PrintAll(Contact c)
        {
            var db = new SQLiteConnection(database);
            var query = "update Live set IsPrint=1 where Contactid = " + c.Id.ToString();
            db.Execute(query);
            db.Close();
        }

        public void RemovPrintAll(Contact c)
        {
            var db = new SQLiteConnection(database);
            var query = "update Live set IsPrint=0 where Contactid = " + c.Id.ToString();
            db.Execute(query);
            db.Close();
        }

        public int GetPrintCount(Contact c)
        {
            var db = new SQLiteConnection(database);
            var query = from l in db.Table<Live>()
                        where l.ContactId == c.Id && l.IsPrint
                        select l;
            var count = query.Count();
            db.Close();
            return count;
        }

        public int GetCount(Contact c)
        {
            var db = new SQLiteConnection(database);
            var query = from l in db.Table<Live>()
                        where l.ContactId == c.Id
                        select l;
            var count = query.Count();
            db.Close();
            return count;
        }

        public List<int> GetPrintContactIds()
        {
            var db = new SQLiteConnection(database);
            var query = from l in db.Table<Live>()
                        where l.IsPrint
                        select l;
            var list = query.ToList();
            db.Close();
            return list.Select(x=>x.ContactId).ToList();
        }

        public List<Live> GetPrintLiveByContactId(int contactid)
        {
            var db = new SQLiteConnection(database);
            var query = from l in db.Table<Live>()
                        where l.IsPrint && l.ContactId == contactid
                        select l;
            var list = query.ToList();
            db.Close();
            return list;
        }

        public void Export(Contact c, string filename)
        {
            var all = GetAll(c.Id);
            var sw = new StreamWriter(filename, true);
            sw.Write(String.Join("\r\n", all.Select(x => x.GetExportString()+"\t"+c.Name+"\t"+c.Code)));
            sw.Write("\r\n");
            sw.Close();
        }

        public List<Live> GetByContactCount(ContactCount c)
        {
            return c.IsFull ? GetAll(c.Id) : GetPrintLiveByContactId(c.Id);
        }

        public void SetPrintDate(ContactCount c, string date)
        {
            if (c.IsFull)
            {
                var db = new SQLiteConnection(database);
                var query = "update Live set LastPrint='" + date + "' where Contactid = " + c.Id;
                db.Execute(query);
                db.Close();
            }
            else
            {
                var db = new SQLiteConnection(database);
                var query = "update Live set LastPrint='" + date + "' where IsPrint=1 and Contactid = " + c.Id;
                db.Execute(query);
                db.Close();
            }
        }

        public void UpdateContactId(int fromId, int ToId)
        {
            var db = new SQLiteConnection(database);
            var query = "update Live set ContactId='" + ToId + "' where Contactid = " + fromId;
            db.Execute(query);
            db.Close();
        }
    }
}
