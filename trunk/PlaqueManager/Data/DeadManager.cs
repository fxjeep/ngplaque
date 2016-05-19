using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PlaqueData
{
    public class DeadManager
    {
         private string database;
         public DeadManager(string dbfile)
        {
            database = dbfile;
        }

        public List<Dead> GetAll(int cid)
        {
            var db = new SQLiteConnection(database);
            var query = from d in db.Table<Dead>()
                        where d.ContactId == cid
                        orderby d.DeadName
                        select d;
            var list = query.ToList();
            db.Close();
            return list;
        }

        public List<Dead> GetByName(Dead ance)
        {
            var db = new SQLiteConnection(database);
            var query = from l in db.Table<Dead>()
                        where l.ContactId == ance.ContactId
                               && l.DeadName == ance.DeadName
                               && l.LiveName == ance.LiveName
                        select l;
            var list = query.ToList();
            db.Close();
            return list;
        }

        public void Delete(Dead ance)
        {
            var db = new SQLiteConnection(database);
            db.Delete(ance);
            db.Close();
        }

        public void SaveOrUpdate(Dead dead)
        {
            if (dead.Id == 0)
            {
                var db = new SQLiteConnection(database);
                db.Insert(dead);
                db.Close();
            }
            else
            {
                var db = new SQLiteConnection(database);
                var query = from l in db.Table<Dead>()
                            where l.ContactId == dead.ContactId && l.Id == dead.Id
                            select l;
                var list = query.ToList();
                if (list.Count > 0)
                {
                    list[0].LiveName = dead.LiveName;
                    list[0].DeadName = dead.DeadName;
                    list[0].Relation = dead.Relation;
                    list[0].IsPrint = dead.IsPrint;
                    db.Update(list[0]);
                }
                db.Close();
            }
        }

        public void DeleteByContact(Contact c)
        {
            var db = new SQLiteConnection(database);
            var query = "delete from Dead where Contactid = " + c.Id.ToString();
            db.Execute(query);
            db.Close();
        }

        public void PrintAll(Contact c)
        {
            var db = new SQLiteConnection(database);
            var query = "update Dead set IsPrint=1 where Contactid = " + c.Id.ToString();
            db.Execute(query);
            db.Close();
        }

        public void RemovPrintAll(Contact c)
        {
            var db = new SQLiteConnection(database);
            var query = "update Dead set IsPrint=0 where Contactid = " + c.Id.ToString();
            db.Execute(query);
            db.Close();
        }

        public int GetPrintCount(Contact c)
        {
            var db = new SQLiteConnection(database);
            var query = from l in db.Table<Dead>()
                        where l.ContactId == c.Id && l.IsPrint
                        select l;
            var count = query.Count();
            db.Close();
            return count;
        }

        public int GetCount(Contact c)
        {
            var db = new SQLiteConnection(database);
            var query = from l in db.Table<Dead>()
                        where l.ContactId == c.Id
                        select l;
            var count = query.Count();
            db.Close();
            return count;
        }


        public List<int> GetPrintContactIds()
        {
            var db = new SQLiteConnection(database);
            var query = from l in db.Table<Dead>()
                        where l.IsPrint
                        select l;
            var list = query.ToList();
            db.Close();
            return list.Select(x=>x.ContactId).ToList();
        }

        public List<Dead> GetPrintLiveByContactId(int contactid)
        {
            var db = new SQLiteConnection(database);
            var query = from l in db.Table<Dead>()
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
            sw.Write(String.Join("\r\n", all.Select(x => x.GetExportString() + "\t" + c.Name + "\t" + c.Code)));
            sw.Write("\r\n");
            sw.Close();
        }

        public List<Dead> GetByContactCount(ContactCount c)
        {
            return c.IsFull ? GetAll(c.Id) : GetPrintLiveByContactId(c.Id);
        }

        public void SetPrintDate(ContactCount c, string date)
        {
            if (c.IsFull)
            {
                var db = new SQLiteConnection(database);
                var query = "update Dead set LastPrint='" + date + "' where Contactid = " + c.Id;
                db.Execute(query);
                db.Close();
            }
            else
            {
                var db = new SQLiteConnection(database);
                var query = "update Dead set LastPrint='" + date + "' where IsPrint=1 and Contactid = " + c.Id;
                db.Execute(query);
                db.Close();
            }
        }

        public void UpdateContactId(int fromId, int ToId)
        {
            var db = new SQLiteConnection(database);
            var query = "update Dead set ContactId='" + ToId + "' where Contactid = " + fromId;
            db.Execute(query);
            db.Close();
        }
    }
}
