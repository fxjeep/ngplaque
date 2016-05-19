using System.Collections.ObjectModel;
using System.Windows.Controls;
using Caliburn.Micro;
using PlaqueData;
using PlaqueManager.Helper;

namespace PlaqueManager.ViewModels
{
    public class LiveDetailViewModel : Screen
    {
        public ObservableCollection<Live> List { get; set; }
        public ObservableCollection<Live> SelectedList { get; set; }
        private IEventAggregator _eventag;

        private Contact CurrentContact;
        private IDataManager _dataManager;

        public int Total
        {
            get { return List==null?0:List.Count; }
        }

        public LiveDetailViewModel(IEventAggregator eventAggregator, IDataManager dataManager)
        {
            _eventag = eventAggregator;
            _dataManager = dataManager;
        }

        public void SetCurrentContact(Contact contact)
        {
            CurrentContact = contact;
        }

        public void Clear()
        {
            if (List == null) return;
            List.Clear();
            SelectedList.Clear();
        }

        public int Load()
        {
            List = new ObservableCollection<Live>();
            SelectedList = new ObservableCollection<Live>();
            var list = _dataManager.LiveRepo.GetAll(CurrentContact.Id);
            List.Clear();
            foreach (var live in list)
            {
                List.Add(live);
            }
            NotifyOfPropertyChange(nameof(List));
            return list.Count;
        }

        public void Delete()
        {
            if (SelectedList == null) return;

            for (int i = SelectedList.Count - 1; i >= 0; i--)
            {
                _dataManager.LiveRepo.Delete(SelectedList[i]);
                List.Remove(SelectedList[i]);
            }
            NotifyOfPropertyChange(nameof(List));
            _eventag.PublishOnUIThread(new UpdateTabHeaderMsg());
        }

        public void Print()
        {
            if (SelectedList == null) return;

            foreach (var live in SelectedList)
            {
                live.IsPrint = true;
                _dataManager.LiveRepo.SaveOrUpdate(live);
            }
            NotifyOfPropertyChange(nameof(List));
        }

        public void RemovePrint()
        {
            if (SelectedList == null) return;

            foreach (var live in SelectedList)
            {
                live.IsPrint = false;
                _dataManager.LiveRepo.SaveOrUpdate(live);
            }
            NotifyOfPropertyChange(nameof(List));
        }

        public void EditRow(ActionExecutionContext context)
        {
            var evtarg = context.EventArgs as DataGridRowEditEndingEventArgs;
            if (evtarg == null) return;

            var live = evtarg.Row.Item as Live;
            live.ContactId = CurrentContact.Id;
            _dataManager.LiveRepo.SaveOrUpdate(live);
            _eventag.PublishOnUIThread(new UpdateTabHeaderMsg());
        }

        public void OnSelectionChanged(ActionExecutionContext context)
        {
            var evtarg = context.EventArgs as SelectionChangedEventArgs;
            if (evtarg == null) return;

            foreach (var addedRow in evtarg.AddedItems)
            {
                var live = addedRow as Live;
                if (live!=null) SelectedList.Add(addedRow as Live);
            }

            foreach (var removedRow in evtarg.RemovedItems)
            {
                var live = removedRow as Live;
                if (live != null ) SelectedList.Remove(live);
            }
        }
    }
}
