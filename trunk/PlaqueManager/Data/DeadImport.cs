using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaqueData
{
    public class DeadImportRecord : ImportRecordBase
    {
        public string DeadName { get; set; }
        public string LiveName { get; set; }
        public string Relation { get; set; }


        public override void Initial(string[] fields)
        {
            if (fields == null) Error = "Null fields";

            else if (fields.Length == 0) Error = "No fields";

            else if (fields.Length == 1)
            {
                Error = "missing livename, relation, contract name and code";
                DeadName = fields[0];
            }
            else if (fields.Length == 2)
            {
                Error = "missing relation, contract name and code";
                DeadName = fields[0];
                LiveName = fields[1];
            }
            else if (fields.Length == 3)
            {
                Error = "missing contract name and code";
                DeadName = fields[0];
                LiveName = fields[1];
                Relation = fields[2];
            }
            else if (fields.Length == 4)
            {
                Error = "missing contract code";
                DeadName = fields[0];
                LiveName = fields[1];
                Relation = fields[2];
                ContactName = fields[3];
            }
            else
            {
                Error = "";
                DeadName = fields[0];
                LiveName = fields[1];
                Relation = fields[2];
                ContactName = fields[3];
                ContactCode = fields[4];
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

            var d = new Dead { DeadName=DeadName, LiveName=LiveName, Relation=Relation, ContactId = contactlist[0].Id };
            var deadlist = dataManager.DeadRepo.GetByName(d);
            if (deadlist.Count == 0)
            {
                dataManager.DeadRepo.SaveOrUpdate(d);
                IsDuplicated = false;
            }
            else
            {
                IsDuplicated = true;
            }
        }

        public override string GetOutputString()
        {
            return DeadName + "\t" + LiveName + "\t" + Relation +"\t" + ContactName + "\t" + ContactCode + "\t" + Error;
        }
    }
}
