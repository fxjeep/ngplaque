using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Caliburn.Micro;
using Infralution.Localization.Wpf;
using PlaqueManager.Generator;
using PlaqueManager.Helper;
using PlaqueData;

namespace PlaqueManager.ViewModels
{
    public class SearchViewModel : Screen, IHandle<ClearSearchResultMsg>
    {
        public IEditDetail Edit { get; set; }
        private IDataManager _datamanager;
        private IEventAggregator _aggregator;
        private IWindowManager _winManager;

        #region Binding Properties
        public Contact SelectedContact { get; set; }
        public ObservableCollection<Contact> ContactList { get; set; }

        public ObservableCollection<string> SearchOption { get; set; }
        public int SelectedOptionIndex { get; set; }
        public string SearchText { get; set; }

        public string Opt1Text { get; set; }
        public string Opt2Text { get; set; }


        public string PrintContactMenu { get; set; }
        public string GenerateDetailMenu { get; set; }
        public string ExportDataMenu { get; set; }
        public string MergeContactMenu { get; set; }

        public MergeContactViewModel MergeContactDialog { get; set; }
        #endregion



        public SearchViewModel(IDataManager datamanager, IEventAggregator aggregator, IWindowManager winManager)
        {
            CultureManager.UICultureChanged += CultureManagerOnUiCultureChanged;
            ContactList = new ObservableCollection<Contact>();
            SearchOption = new ObservableCollection<string>();
            _datamanager = datamanager;
            _aggregator = aggregator;
            _winManager = winManager;
            _aggregator.Subscribe(this);
        }

        private void CultureManagerOnUiCultureChanged(object sender, EventArgs eventArgs)
        {
            ResourceManager rm = new ResourceManager(Constants.ResourceName, Assembly.GetExecutingAssembly());
            SetupSearchParameterBinding(rm);
            rm.ReleaseAllResources();
        }

        public void SetupSearchParameterBinding(ResourceManager rm)
        {
            SearchOption.Clear();
            SearchOption.Add(rm.GetString(Constants.SearchParameter_Contact_Name));
            SearchOption.Add(rm.GetString(Constants.SearchParameter_Contact_Code));
            SelectedOptionIndex = 0;
            NotifyOfPropertyChange(() => SearchOption);
            NotifyOfPropertyChange(() => SelectedOptionIndex);
        }

        public void ContextMenuOpening(ActionExecutionContext context)
        {
            if (SelectedContact != null)
            {
                ResourceManager rm = new ResourceManager(Constants.ResourceName, Assembly.GetExecutingAssembly());
                PrintContactMenu = String.Format(rm.GetString(Constants.Search_PrintContactMenu), SelectedContact.Name);
                ExportDataMenu = String.Format(rm.GetString(Constants.Search_ExportDataMenu), SelectedContact.Name);
                GenerateDetailMenu = String.Format(rm.GetString(Constants.Search_GenerateDetailMenu), SelectedContact.Name);
                MergeContactMenu = String.Format(rm.GetString(Constants.Search_MergeContactMenu), SelectedContact.Name);
                rm.ReleaseAllResources();

                NotifyOfPropertyChange(nameof(PrintContactMenu));
                NotifyOfPropertyChange(nameof(ExportDataMenu));
                NotifyOfPropertyChange(nameof(GenerateDetailMenu));
                NotifyOfPropertyChange(nameof(MergeContactMenu));
            }
        }

        public void EditContactRow(ActionExecutionContext context)
        {
            var evtarg = context.EventArgs as DataGridRowEditEndingEventArgs;
            if (evtarg == null) return;

            var contact = evtarg.Row.Item as Contact;
            _datamanager.ContactRepo.UpdateContact(contact);
        }

        public void ContactRowSelected(ActionExecutionContext context)
        {
            //send event to AppViewModel to show detail of selected contact.
            if (Edit != null && SelectedContact !=null ) Edit.ShowDetail(SelectedContact);
        }
        
        #region New,Delete,Add/Remove Print, Add/Remove Report
        public void NewContact()
        {
            var contact = _datamanager.ContactRepo.GetNewContact();
            ContactList.Add(contact);
        }

        public void DelContact()
        {
            if (SelectedContact != null)
            {
                var result = DialogHelper.ShowMessageBox(Constants.Search_DeleteContactConfirmTitle, Constants.Search_DeleteContactConfirm, SelectedContact.Name, SelectedContact.Code);
                if (result == MessageBoxResult.OK)
                {
                    _datamanager.DeleteContact(SelectedContact);
                    ContactList.Remove(SelectedContact);    
                }
            }

            if (Edit != null)
                Edit.ClearDetails();
        }

        public void AddPrint()
        {
            if (SelectedContact != null)
            {
                SelectedContact.IsPrint = true;
                _datamanager.ContactRepo.SetContactToPrint(SelectedContact);
                ContactRowSelected(null);
            }
        }

        public void PrintContact(ActionExecutionContext context)
        {
            AddPrint();
        }

        public void GenerateDetail(ActionExecutionContext context)
        {
            GenerateReport();
        }

        public void GenerateReport()
        {
            if (SelectedContact == null) return;

            string path = PlaqueConfig.GetPrintFolder() + DateTime.Now.ToString("yyyyMMdd");
            string name = String.Format("PW_{0}_{1}_{2}", 
                            SelectedContact.Name, 
                            SelectedContact.Code, 
                            DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            string filename = Path.Combine(path, name);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            var dxps = new PrintDetailXPS(filename, _datamanager);
            dxps.Print(SelectedContact);
            DialogHelper.ShowMessageBox(Constants.Search_GenerateContactReportTitle,
                                    Constants.Search_GenerateContactReportFinish, SelectedContact.Name);
        }

        public void ExportData(ActionExecutionContext context)
        {
            Export();
        }

        public void Export()
        {
            if (SelectedContact != null)
            {
                string path = PlaqueConfig.GetExportFolder();
                string surfix = SelectedContact.Name + "_"+ SelectedContact.Code + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string livexportfile = Path.Combine(path, surfix+"_live.txt");
                string deadxportfile = Path.Combine(path, surfix+"_dead.txt");
                string ancestorxportfile = Path.Combine(path, surfix+"_ancestor.txt");

                _datamanager.LiveRepo.Export(SelectedContact, livexportfile);
                _datamanager.DeadRepo.Export(SelectedContact, deadxportfile);
                _datamanager.AncestorRepo.Export(SelectedContact, ancestorxportfile);

                DialogHelper.ShowMessageBox(Constants.Search_ExportContactTitle,
                                    Constants.Search_ExportContactFinish, SelectedContact.Name);
            }
        }

        public void MergeContact(ActionExecutionContext context)
        {
            if (SelectedContact != null)
            {
                MergeContactDialog = new MergeContactViewModel(_datamanager, SelectedContact);
                var result = _winManager.ShowDialog(MergeContactDialog);
            }
        }
        #endregion

        #region Search
        public void Search(ActionExecutionContext context)
        {
            var arg = context.EventArgs as KeyEventArgs;
            if (arg == null) return;

            if (arg.Key == Key.Enter)
            {
                DoContactSearch();
            }
        }

        public void DoContactSearch()
        {
            string searchword = (String.IsNullOrEmpty(SearchText)) ? "" : SearchText;
            var contactlist = _datamanager.ContactRepo.SearchContact(searchword, SelectedOptionIndex);
            ContactList.Clear();
            foreach (var c in contactlist) ContactList.Add(c);
        }

        public void Handle(ClearSearchResultMsg msg)
        {
            ContactList.Clear();
        }
        #endregion

    }
}
