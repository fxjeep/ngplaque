using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PlaqueData
{
    public interface IDataManager
    {
        ContactManager ContactRepo { get; }
        LiveManager LiveRepo { get; }
        DeadManager DeadRepo { get; }
        AncestorManager AncestorRepo { get; }
        void DeleteContact(Contact c);
        void ConnectDatabase(string filename);
    }

    public class DataManager : IDataManager
    {
        public static IDataManager Instance = new DataManager();

        public ContactManager ContactRepo { get; set; }
        public LiveManager LiveRepo { get; set; }
        public DeadManager DeadRepo { get; set; }
        public AncestorManager AncestorRepo { get; set; }

        private string database;

        public DataManager()
        {
            ConnectDatabase(PlaqueConfig.CurrentDataFile);
        }



        public void DeleteContact(Contact c)
        {
            LiveRepo.DeleteByContact(c);
            DeadRepo.DeleteByContact(c);
            AncestorRepo.DeleteByContact(c);
            ContactRepo.DeleteContact(c);
        }

        public void ConnectDatabase(string filename)
        {
            database = filename;
            if (!File.Exists(database))
            {
                var db = new SQLiteConnection(filename);
                db.CreateTable<Contact>();
                db.CreateTable<Live>();
                db.CreateTable<Dead>();
                db.CreateTable<Ancestor>();
            }

            ContactRepo = new ContactManager(database);
            LiveRepo = new LiveManager(database);
            DeadRepo = new DeadManager(database);
            AncestorRepo = new AncestorManager(database);
        }
    }
}
