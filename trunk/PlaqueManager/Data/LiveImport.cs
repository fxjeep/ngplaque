using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaqueData
{
    public class LiveImportRecord : ImportRecordBase
    {
        public string Name { get; private set; }

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
                Error = "missing contract name and code";
                Name = fields[0];
            }

            if (fields.Length == 2)
            {
                Error = "missing contact code";
                Name = fields[0];
                ContactName = fields[1];
            }

            if (fields.Length == 3)
            {
                Error = "";
                Name = fields[0];
                ContactName = fields[1];
                ContactCode = fields[2];
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

            var l = new Live {Name = Name, ContactId = contactlist[0].Id};

            var livelist = dataManager.LiveRepo.GetByName(l);
            if (livelist.Count == 0)
            {
                dataManager.LiveRepo.SaveOrUpdate(l);
                IsDuplicated = false;
            }
            else
            {
                IsDuplicated = true;
            }
        }

        public override string GetOutputString()
        {
            return Name + "\t" + ContactName + "\t" + ContactCode + "\t" + Error;
        }
    }
}
