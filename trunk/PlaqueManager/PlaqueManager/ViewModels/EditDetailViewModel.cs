using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Resources;
using Caliburn.Micro;
using Infralution.Localization.Wpf;
using PlaqueManager.Helper;
using PlaqueData;

namespace PlaqueManager.ViewModels
{
    public class EditDetailViewModel : Screen, IHandle<UpdateTabHeaderMsg>
    {
        public string ContactInfo { get; set; }
        public string LiveHeader { get; set; }
        public string DeadHeader { get; set; }
        public string AncestorHeader { get; set; }

        public LiveDetailViewModel LiveDetail { get; set; }
        public DeadDetailViewModel DeadDetail { get; set; }
        public AncestorDetailViewModel AncestorDetail { get; set; }

        public ObservableCollection<Ancestor> AncestorList { get; set; }
        public int SelectedTabIndex { get; set; }
        private Contact current_contact;
        private IEventAggregator eventag;
        private IDataManager _dataManager;

        public EditDetailViewModel(IDataManager dataManager, IEventAggregator eventAggregator)
        {
            ContactInfo = "";
            CultureManager.UICultureChanged += CultureManagerOnUiCultureChanged;
            LiveDetail = new LiveDetailViewModel(eventAggregator, dataManager);
            DeadDetail = new DeadDetailViewModel(eventAggregator, dataManager);
            AncestorDetail = new AncestorDetailViewModel(eventAggregator, dataManager);

            _dataManager = dataManager;
            eventag = eventAggregator;
            eventag.Subscribe(this);
        }

        public void Handle(UpdateTabHeaderMsg message)
        {
            CultureManagerOnUiCultureChanged(null, null);
        }

        private void CultureManagerOnUiCultureChanged(object sender, EventArgs eventArgs)
        {
            ResourceManager rm = new ResourceManager(Constants.ResourceName, Assembly.GetExecutingAssembly());
            RefreshHeader(rm);
            rm.ReleaseAllResources();
        }

        public void Delete()
        {
            if (SelectedTabIndex == 0) LiveDetail.Delete();
            else if (SelectedTabIndex == 1) DeadDetail.Delete();
            else if (SelectedTabIndex == 2) AncestorDetail.Delete();
        }

        public void Print()
        {
            if (SelectedTabIndex == 0) LiveDetail.Print();
            else if (SelectedTabIndex == 1) DeadDetail.Print();
            else if (SelectedTabIndex == 2) AncestorDetail.Print();
        }

        public void RemovePrint()
        {
            if (SelectedTabIndex == 0) LiveDetail.RemovePrint();
            else if (SelectedTabIndex == 1) DeadDetail.RemovePrint();
            else if (SelectedTabIndex == 2) AncestorDetail.RemovePrint();
        }

        public void ClearDetails()
        {
            LiveDetail.Clear();
            DeadDetail.Clear();
            AncestorDetail.Clear();
            current_contact = null;
            CultureManagerOnUiCultureChanged(null, null);
        }

        public void ShowDetail(Contact contact)
        {
            current_contact = contact;
            LiveDetail.SetCurrentContact(contact);
            LiveDetail.Load();

            DeadDetail.SetCurrentContact(contact);
            DeadDetail.Load();

            AncestorDetail.SetCurrentContact(contact);
            AncestorDetail.Load();

            CultureManagerOnUiCultureChanged(null, null);
        }

        public void RefreshHeader(ResourceManager rm)
        {
            LiveHeader = rm.GetString(Constants.EditDetail_Live_TabName)+" (" + LiveDetail.Total.ToString() + ")";
            DeadHeader = rm.GetString(Constants.EditDetail_Dead_TabName) + " (" + DeadDetail.Total.ToString() + ")";
            AncestorHeader = rm.GetString(Constants.EditDetail_Ancestor_TabName) + " (" + AncestorDetail.Total.ToString() + ")";

            if (current_contact != null)
                ContactInfo = rm.GetString(Constants.EditDetail_ContactInfo) + ": " + current_contact.Name;
            else
                ContactInfo = rm.GetString(Constants.EditDetail_SelectContract);

            NotifyOfPropertyChange(nameof(LiveHeader));
            NotifyOfPropertyChange(nameof(DeadHeader));
            NotifyOfPropertyChange(nameof(AncestorHeader));
            NotifyOfPropertyChange(nameof(ContactInfo));
        }
    }
}
