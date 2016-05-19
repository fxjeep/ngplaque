using Caliburn.Micro;
using PlaqueData;

namespace PlaqueManager.ViewModels
{
    public interface IEditDetail
    {
        void ShowDetail(Contact contact);
        void ClearDetails();
    }

    public class AppViewModel : Conductor<Screen>, IEditDetail
    {
        public SearchViewModel Search { get; set; }
        public EditDetailViewModel Edit { get; set; }
        public OutputViewModel Output { get; set; }
        public DataViewModel DataManager { get; set; }

        public AppViewModel(IEventAggregator eventAggregator, IDataManager dataManager, IWindowManager winManager)
        {
            Search = new SearchViewModel(dataManager, eventAggregator, winManager);
            Edit = new EditDetailViewModel(dataManager, eventAggregator);
            Output = new OutputViewModel(dataManager);
            DataManager = new DataViewModel(dataManager, eventAggregator);
            Search.Edit = this;
            DisplayName = "";
        }

        public void ShowDetail(Contact contact)
        {
            ActivateItem(Edit);
            if (contact!=null) Edit.ShowDetail(contact);
        }

        public void ClearDetails()
        {
            if (Edit != null) Edit.ClearDetails();
        }

        public void Data()
        {
            ActivateItem(DataManager);
        }

        public void Generate()
        {
            Output.Refresh();
            ActivateItem(Output);
        }
    }
}
