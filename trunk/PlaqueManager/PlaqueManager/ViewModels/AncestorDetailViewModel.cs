using System.Collections.ObjectModel;
using System.Windows.Controls;
using Caliburn.Micro;
using PlaqueData;
using PlaqueManager.Helper;

namespace PlaqueManager.ViewModels
{
    public class AncestorDetailViewModel : Screen
    {
        public ObservableCollection<Ancestor> List { get; set; }
        public ObservableCollection<Ancestor> SelectedList { get; set; }
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

        public AncestorDetailViewModel(IEventAggregator eventAggregator, IDataManager dataManager)
        {
            eventag = eventAggregator;
            _dataManager = dataManager;
        }

        public void SetCurrentContact(Contact contact)
        {
            CurrentContact = contact;
        }

        public int Load()
        {
            List = new ObservableCollection<Ancestor>();
            SelectedList = new ObservableCollection<Ancestor>();
            var list = _dataManager.AncestorRepo.GetAll(CurrentContact.Id);
            List.Clear();
            foreach (var Ancestor in list)
            {
                List.Add(Ancestor);
            }
            NotifyOfPropertyChange(nameof(List));
            return list.Count;
        }

        public void Delete()
        {
            if (SelectedList == null) return;

            for (int i = SelectedList.Count - 1; i >= 0; i--)
            {
                _dataManager.AncestorRepo.Delete(SelectedList[i]);
                List.Remove(SelectedList[i]);
            }
            NotifyOfPropertyChange(nameof(List));
            eventag.PublishOnUIThread(new UpdateTabHeaderMsg());
        }

        public void Print()
        {
            if (SelectedList == null) return;

            foreach (var Ancestor in SelectedList)
            {
                Ancestor.IsPrint = true;
                _dataManager.AncestorRepo.SaveOrUpdate(Ancestor);
            }
            NotifyOfPropertyChange(nameof(List));
        }

        public void RemovePrint()
        {
            if (SelectedList == null) return;

            foreach (var Ancestor in SelectedList)
            {
                Ancestor.IsPrint = false;
                _dataManager.AncestorRepo.SaveOrUpdate(Ancestor);
            }
            NotifyOfPropertyChange(nameof(List));
        }

        public void EditRow(ActionExecutionContext context)
        {
            var evtarg = context.EventArgs as DataGridRowEditEndingEventArgs;
            if (evtarg == null) return;

            var Ancestor = evtarg.Row.Item as Ancestor;
            Ancestor.ContactId = CurrentContact.Id;
            _dataManager.AncestorRepo.SaveOrUpdate(Ancestor);
            eventag.PublishOnUIThread(new UpdateTabHeaderMsg());
        }

        public void OnSelectionChanged(ActionExecutionContext context)
        {
            var evtarg = context.EventArgs as SelectionChangedEventArgs;
            if (evtarg == null) return;

            foreach (var addedRow in evtarg.AddedItems)
            {
                var a = addedRow as Ancestor;
                if (a!=null) SelectedList.Add(a);
            }

            foreach (var removedRow in evtarg.RemovedItems)
            {
                var a = removedRow as Ancestor;
                if ( a!=null ) SelectedList.Remove(a);
            }
        }
    }
}
