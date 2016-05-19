using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PlaqueData
{
    public interface IDetailItem
    {
        int ContactId { get; set; }
        bool IsPrint { get; set; }
        string LastPrint { get; set; }
        string GetExportString();
    }
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }

        protected bool SetProperty<T>(ref T storage, T value,
            [CallerMemberName] String propertyName = null)
        {
            if (Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    [Table("Contact")]
    public class Contact : ObservableObject
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }

        private string name;

        [MaxLength(50)]
        public string Name{ get { return name; } set { SetProperty(ref name, value);}}

        private string code;
        [MaxLength(10)]
        public string Code { get { return code; } set { SetProperty(ref code, value);} }
        
        [MaxLength(10)]
        public string LastPrint { get; set; }

        private bool isprint = false;
        public bool IsPrint
        {
            get { return isprint; }
            set { SetProperty(ref isprint, value); }
        }
    }

    [Table("Live")]
    public class Live : ObservableObject, IDetailItem
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public int ContactId { get; set; }

        private string name;
        [MaxLength(50)]
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        [MaxLength(10)]
        public string LastPrint { get; set; }

        private bool isprint = false;
        public bool IsPrint
        {
            get { return isprint; }
            set { SetProperty(ref isprint, value); }
        }

        public string GetExportString()
        {
            return Name;
        }

        public Live()
        {
            Name = "";
        }
    }

    [Table("Dead")]
    public class Dead : ObservableObject, IDetailItem
    {
        private string deadname;
        private string livename;
        private string relation;

        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public int ContactId { get; set; }

        [MaxLength(50)]
        public string DeadName
        {
            get { return deadname; }
            set { SetProperty(ref deadname, value); }
        }

        [MaxLength(50)]
        public string LiveName
        {
            get { return livename; }
            set { SetProperty(ref livename, value); }
        }

        [MaxLength(50)]
        public string Relation
        {
            get { return relation; }
            set { SetProperty(ref relation, value); }
        }
        
        [MaxLength(10)]
        public string LastPrint { get; set; }

        private bool isprint = false;
        public bool IsPrint
        {
            get { return isprint; }
            set { SetProperty(ref isprint, value); }
        }

        public string GetExportString()
        {
            return DeadName+"\t"+LiveName+"\t"+Relation;
        }

        public Dead()
        {
            DeadName = "";
            LiveName = "";
            Relation = "";
        }
    }

    [Table("Ancestor")]
    public class Ancestor : ObservableObject, IDetailItem
    {
        private string surname;
        private string name;

        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public int ContactId { get; set; }

        [MaxLength(50)]
        public string Surname
        {
            get { return surname; }
            set { SetProperty(ref surname, value); }
        }

        [MaxLength(50)]
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        [MaxLength(10)]
        public string LastPrint { get; set; }

        private bool isprint = false;
        public bool IsPrint
        {
            get { return isprint; }
            set { SetProperty(ref isprint, value); }
        }

        public string GetExportString()
        {
            return Surname + "\t" + Name;
        }

        public Ancestor()
        {
            Surname = "";
            Name = "";
        }
    }

    public class ContactCount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string LastPrint { get; set; }
        public bool IsFull { get; set; }
        public int CountPrintLive { get; set; }
        public int CountPrintAncestor { get; set; }
        public int CountPrintDead { get; set; }
        public int CountTotalLive { get; set; }
        public int CountTotalAncestor { get; set; }
        public int CountTotalDead { get; set; }
        public bool IsShowPrintCount { get; set; } = true;

        public Contact GetContact()
        {
            Contact contact = new Contact();
            contact.Code = Code;
            contact.Name = Name;
            contact.Id = Id;
            return contact;
        }

        public string IsFullPrint => IsFull ? "Y" : "";

        public string LiveCount => 
            IsShowPrintCount?CountPrintLive + "/" + CountTotalLive : CountTotalLive.ToString();

        public string DeadCount => 
            IsShowPrintCount? CountPrintDead + "/" + CountTotalDead : CountTotalDead.ToString();

        public string AncestorCount => 
            IsShowPrintCount? CountPrintAncestor + "/" + CountTotalAncestor : CountTotalAncestor.ToString();
    }
}
