using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using PlaqueManager.Generator;
using PlaqueManager.Helper;
using PlaqueData;
using Infralution.Localization.Wpf;

namespace PlaqueManager.ViewModels
{
    public class OutputViewModel : Screen
    {
        public string DateString = DateTime.Now.ToString("yyyy-MM-dd");
        public ObservableCollection<ContactCount> ContactList { get; set; }
        public string ReportTotal { get; set; }
        private IDataManager _dataManager;

        public OutputViewModel(IDataManager dataManager)
        {
            CultureManager.UICultureChanged += CultureManagerOnUiCultureChanged;
            _dataManager = dataManager;
            RefreshOutput();
        }

        private void CultureManagerOnUiCultureChanged(object sender, EventArgs eventArgs)
        {
            RefreshOutput();
        }

        public void RefreshOutput()
        {
            ContactList = new ObservableCollection<ContactCount>();
            AddAllPrintContacts();
            AddPartialPrintContacts();
            ContactList.Sort(x => x.Code);

            int totalLivePrint = ContactList.Sum(x => x.CountPrintLive);
            int totalDeadPrint = ContactList.Sum(x => x.CountPrintDead);
            int totalAncestorPrint = ContactList.Sum(x => x.CountPrintAncestor);

            ReportTotal = String.Format(Extension.GetResourceText(Constants.Output_ReportTotal), 
                ContactList.Count, totalLivePrint, totalDeadPrint, totalAncestorPrint);

            NotifyOfPropertyChange(nameof(ReportTotal));
            NotifyOfPropertyChange(nameof(ContactList));
        }

        #region Add List
        public void AddAllPrintContacts()
        {
            List<Contact> allprintlist = _dataManager.ContactRepo.GetContactPrintList();
            foreach (var contact in allprintlist)
            {
                ContactCount count = new ContactCount();
                count.Id = contact.Id;
                count.Name = contact.Name;
                count.Code = contact.Code;
                count.LastPrint = contact.LastPrint;
                count.IsFull = true;
                count.CountPrintLive = count.CountTotalLive = _dataManager.LiveRepo.GetCount(contact);
                count.CountPrintDead = count.CountTotalDead = _dataManager.DeadRepo.GetCount(contact);
                count.CountPrintAncestor = count.CountTotalAncestor = _dataManager.AncestorRepo.GetCount(contact);
                ContactList.Add(count);
            }
        }

        public void AddPartialPrintContacts()
        {
            List<int> contactlive = _dataManager.LiveRepo.GetPrintContactIds();
            List<int> contactdead = _dataManager.DeadRepo.GetPrintContactIds();
            List<int> contactancestor = _dataManager.AncestorRepo.GetPrintContactIds();

            List<int> final = contactlive.Union(contactdead).Union(contactancestor).ToList();

            List<int> full_print = ContactList.Select(x => x.Id).ToList();

            foreach (var id in final)
            {
                if (full_print.Contains(id)) continue;

                var contact = _dataManager.ContactRepo.GetById(id);
                if (contact != null)
                {
                    ContactCount count = new ContactCount();
                    count.Id = id;
                    count.Name = contact.Name;
                    count.Code = contact.Code;
                    count.LastPrint = contact.LastPrint;
                    count.IsFull = false;
                    count.CountPrintLive = _dataManager.LiveRepo.GetPrintCount(contact);
                    count.CountPrintDead = _dataManager.DeadRepo.GetPrintCount(contact);
                    count.CountPrintAncestor = _dataManager.AncestorRepo.GetPrintCount(contact);
                    count.CountTotalLive = _dataManager.LiveRepo.GetCount(contact);
                    count.CountTotalDead = _dataManager.DeadRepo.GetCount(contact);
                    count.CountTotalAncestor = _dataManager.AncestorRepo.GetCount(contact);
                    ContactList.Add(count);
                }
            }
        }

        public void ClearPrinted()
        {
            var result = DialogHelper.ShowMessageBox(Constants.Output_ClearPrintTitle, Constants.Output_ClearPrintConfirm);
            if (result == MessageBoxResult.Cancel) return;

            for (int i = ContactList.Count - 1; i>=0; i--)
            {
                DeletePrint(ContactList[i]);
            }
            RefreshOutput();
        }

        public void DeletePrint(ContactCount item)
        {
            //clear print flag for contact
            var contact = _dataManager.ContactRepo.GetById(item.Id);
            if (contact != null)
            {
                _dataManager.ContactRepo.RemoveContactFromPrint(contact);
                _dataManager.LiveRepo.RemovPrintAll(contact);
                _dataManager.DeadRepo.RemovPrintAll(contact);
                _dataManager.AncestorRepo.RemovPrintAll(contact);
            }
            ContactList.Remove(item);
        }
        #endregion

        public void Generate()
        {
            string path = PlaqueConfig.GetPrintFolder() + DateTime.Now.ToString("yyyyMMdd");
            string filename = Path.Combine(path, "PW_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            if (ContactList == null || ContactList.Count == 0) return;

            GenerateCSYQ(filename);
            GenerateWS(filename);
            GenerateZX(filename);
            GeneratePrintReport(filename);
        }

        #region Generate methods
        public void GenerateCSYQ(string filename)
        {
            List<Live> list = new List<Live>();
            foreach (ContactCount c in ContactList)
            {
                var lives = _dataManager.LiveRepo.GetByContactCount(c);
                _dataManager.LiveRepo.SetPrintDate(c, DateString);
                list.AddRange(lives);
            }
            LiveXPS xps = new LiveXPS(filename);
            xps.Print(list);
        }

        public void GenerateWS(string filename)
        {
            List<Dead> list = new List<Dead>();
            foreach (ContactCount c in ContactList)
            {
                var deads = _dataManager.DeadRepo.GetByContactCount(c);
                _dataManager.DeadRepo.SetPrintDate(c, DateString);
                list.AddRange(deads);
            }
            DeadXPS xps = new DeadXPS(filename);
            xps.Print(list);
        }

        public void GenerateZX(string filename)
        {
            List<Ancestor> list = new List<Ancestor>();
            foreach (ContactCount c in ContactList)
            {
                var ancestors = _dataManager.AncestorRepo.GetByContactCount(c);
                _dataManager.AncestorRepo.SetPrintDate(c, DateString);
                list.AddRange(ancestors);
            }

            AncestorXPS xps = new AncestorXPS(filename);
            xps.Print(list);
        }

        public void GeneratePrintReport(string filename)
        {
            var xps = new PrintListXPS(filename);
            xps.Print(ContactList);

            var dxps = new PrintDetailXPS(filename, _dataManager);
            dxps.Print(ContactList);

            //update dates
            foreach (var c in ContactList)
            {
                _dataManager.ContactRepo.UpdateLastPrint(DateTime.Now.ToString("yyyy-MM-dd"), c.Id);
            }
        }
        #endregion
    }
}
