using System.Collections.Generic;
using Caliburn.Micro;
using PlaqueData;

namespace PlaqueManager.ViewModels
{
    public class MergeContactViewModel : Screen
    {
        public string MergeFromContact { get; set; }
        public ContactCount MergeToContact { get; set; }
        public List<ContactCount> AllContacts { get; set; }

        private readonly IDataManager _dataManager;
        private readonly Contact _mergeFrom;

        public MergeContactViewModel(IDataManager dataManager, Contact mergeFrom)
        {
            _dataManager = dataManager;
            _mergeFrom = mergeFrom;
            AllContacts = new List<ContactCount>();

            SetMergeFromContact(_mergeFrom);
            GetAllContacts();
        }

        public void SetMergeFromContact(Contact c)
        {
            MergeFromContact = c.Name + "-" + c.Code;
            NotifyOfPropertyChange(nameof(MergeFromContact));
        }


        public void GetAllContacts()
        {
            var allprintlist = _dataManager.ContactRepo.GetAllContacts();
            foreach (var contact in allprintlist)
            {
                var count = new ContactCount
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    Code = contact.Code,
                    LastPrint = contact.LastPrint,
                    IsFull = true,
                    IsShowPrintCount = false

                };
                count.CountTotalLive = _dataManager.LiveRepo.GetCount(contact);
                count.CountTotalDead = _dataManager.DeadRepo.GetCount(contact);
                count.CountTotalAncestor = _dataManager.AncestorRepo.GetCount(contact);

                AllContacts.Add(count);
            }
        }

        public void Merge()
        {
            if (MergeToContact != null)
            {
                int toId = MergeToContact.Id;
                int fromId = _mergeFrom.Id;

                _dataManager.LiveRepo.UpdateContactId(fromId, toId);
                _dataManager.DeadRepo.UpdateContactId(fromId, toId);
                _dataManager.AncestorRepo.UpdateContactId(fromId, toId);
            }
        }
    }
}
