using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaqueData
{
    public class AncestorImportRecord : ImportRecordBase
    {
        public string LiveName { get; set; }
        public string Surname { get; set; }

        public override void Initial(string[] fields)
        {
            if (fields == null)
            {
                Error = "Null fields";
                return;
            }

            if (fields.Length == 0)
            {
                Error = "No fields";
                return;
            }

            if (fields.Length == 1)
            {
                Error = "missing Surname, contract name and code";
                LiveName = fields[0];
            }
            else if (fields.Length == 2)
            {
                Error = "missing contract name and code";
                LiveName = fields[0];
                Surname = fields[1];
            }
            else if (fields.Length == 3)
            {
                Error = "missing contact code";
                LiveName = fields[0];
                Surname = fields[1];
                ContactName = fields[2];
            }
            else
            {
                Surname = fields[0]; 
                LiveName = fields[1];
                ContactName = fields[2];
                ContactCode = fields[3];
            }
        }

        public override void Import(IDataManager dataManager)
        {
            var contactlist = dataManager.ContactRepo.GetByExactNameAndCode(ContactName, ContactCode);
            if (contactlist.Count == 0)
            {
                var c = dataManager.ContactRepo.CreateContact(ContactName, ContactCode, false);
                contactlist.Add(c);
            }

            var a = new Ancestor { Surname= Surname, Name= LiveName, ContactId = contactlist[0].Id };
            var ancestorlist = dataManager.AncestorRepo.GetByName(a);
            if (ancestorlist.Count == 0)
            {
                dataManager.AncestorRepo.SaveOrUpdate(a);
                IsDuplicated = false;
            }
            else
            {
                IsDuplicated = true;
            }
        }

        public override string GetOutputString()
        {
            return LiveName + "\t" + Surname + "\t" + ContactName + "\t" + ContactCode + "\t" + Error;
        }
    }
}
