using System.Collections.ObjectModel;
using System.Windows.Controls;
using Caliburn.Micro;
using PlaqueData;
using PlaqueManager.Helper;

namespace PlaqueManager.ViewModels
{
    public class DeadDetailViewModel : Screen
    {
        public ObservableCollection<Dead> List { get; set; }
        public ObservableCollection<Dead> SelectedList { get; set; }
        private IEventAggregator eventag;
        private IDataManager _dataManager;

        private Contact CurrentContact;

        public int Total
        {
            get { return List == null ? 0 : List.Count; }
        }

        public void Clear()
        {
            if (List == null) return;
            List.Clear();
            SelectedList.Clear();
        }

        public void SetCurrentContact(Contact contact)
        {
            CurrentContact = contact;
        }

        public DeadDetailViewModel(IEventAggregator eventAggregator, IDataManager dataManager)
        {
            eventag = eventAggregator;
            _dataManager = dataManager;
        }

        public int Load()
        {
            List = new ObservableCollection<Dead>();
            SelectedList = new ObservableCollection<Dead>();
            var list = _dataManager.DeadRepo.GetAll(CurrentContact.Id);
            List.Clear();
            foreach (var Dead in list)
            {
                List.Add(Dead);
            }
            NotifyOfPropertyChange(nameof(List));
            return list.Count;
        }

        public void Delete()
        {
            if (SelectedList == null) return;

            for (int i = SelectedList.Count - 1; i >= 0; i--)
            {
                _dataManager.DeadRepo.Delete(SelectedList[i]);
                List.Remove(SelectedList[i]);
            }
            NotifyOfPropertyChange(nameof(List));
            eventag.PublishOnUIThread(new UpdateTabHeaderMsg());
        }

        public void Print()
        {
            if (SelectedList == null) return;

            foreach (var Dead in SelectedList)
            {
                Dead.IsPrint = true;
                _dataManager.DeadRepo.SaveOrUpdate(Dead);
            }
            NotifyOfPropertyChange(nameof(List));
        }

        public void RemovePrint()
        {
            if (SelectedList == null) return;

            foreach (var Dead in SelectedList)
            {
                Dead.IsPrint = false;
                _dataManager.DeadRepo.SaveOrUpdate(Dead);
            }
            NotifyOfPropertyChange(nameof(List));
        }

        public void EditRow(ActionExecutionContext context)
        {
            var evtarg = context.EventArgs as DataGridRowEditEndingEventArgs;
            if (evtarg == null) return;

            var Dead = evtarg.Row.Item as Dead;
            Dead.ContactId = CurrentContact.Id;
            _dataManager.DeadRepo.SaveOrUpdate(Dead);
            eventag.PublishOnUIThread(new UpdateTabHeaderMsg());
        }

        public void OnSelectionChanged(ActionExecutionContext context)
        {
            var evtarg = context.EventArgs as SelectionChangedEventArgs;
            if (evtarg == null) return;

            foreach (var addedRow in evtarg.AddedItems)
            {
                var d = addedRow as Dead;
                if (d!=null) SelectedList.Add(d);
            }

            foreach (var removedRow in evtarg.RemovedItems)
            {
                var d = removedRow as Dead;
                if (d!=null) SelectedList.Remove(d);
            }
        }
    }
}
