using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Caliburn.Micro;
using PlaqueManager.Helper;
using PlaqueData;
using System.Windows;
using System.Threading.Tasks;
using System.Threading;

namespace PlaqueManager.ViewModels
{
    public class DataViewModel : Screen
    {
        public string SucessFileName { get; set; }
        public string ErrorFileName { get; set; }
        public string DuplicatedFileName { get; set; }
        public string SucessInfo { get; set; }
        public string ErrorInfo { get; set; }
        public string DuplicatedInfo { get; set; }
        public double ExportProgressValue { get; set; }
        public string ExportProgressText { get; set; }
        public Visibility ExportProgressVisibility { get; set; }
        public Visibility ExportButtonVisibility { get; set; }

        public string CurrentDataFile { get { return PlaqueConfig.CurrentDataFile; } }

        public const string Filter = "Text files (*.txt)|*.txt|CSV files (*.csv)|*.csv|All files (*.*)|*.*";

        private IDataManager _dataManager;
        private IEventAggregator _aggregator;
        private CancellationTokenSource cts;

        public DataViewModel(IDataManager db, IEventAggregator aggregator)
        {
            _dataManager = db;
            _aggregator = aggregator;
            HideExportProgress();
        }

        #region Change data file
        public void ChangeFile()
        {
            var folder = DialogHelper.OpenFolder();
            if (!string.IsNullOrEmpty(folder))
            {
                PlaqueConfig.CurrentFolder = folder;
                _dataManager.ConnectDatabase(PlaqueConfig.CurrentDataFile);
                NotifyOfPropertyChange(nameof(CurrentDataFile));
                _aggregator.PublishOnCurrentThread(new ClearSearchResultMsg());
            }
        }
        #endregion

        #region Export
        public async void Export()
        {
            List<Contact> ContactList = _dataManager.ContactRepo.GetByName("");
            if (ContactList.Count == 0) return;

            string surfix = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            int total = ContactList.Count;
            var progress = new Progress<int>(count =>{UpdateExportProgress(count, total);});            
            cts = new CancellationTokenSource();

            ShowExportProgress();
            try
            {
                await Task.Run(() => ExportAllContacts(surfix, ContactList, progress, cts.Token), cts.Token);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception messages:"+e.ToString());
            }
            finally
            {
                cts.Dispose();
            }

            HideExportProgress();
        }

        public void ExportAllContacts(string surfix, List<Contact> contactList, IProgress<int> progress, CancellationToken token)
        {
            string livefile = Path.Combine(PlaqueConfig.GetExportFolder(), surfix + "_Live.txt");
            string deadfile = Path.Combine(PlaqueConfig.GetExportFolder(), surfix + "_Dead.txt");
            string ancestorfile = Path.Combine(PlaqueConfig.GetExportFolder(), surfix + "_Ancestor.txt");

            int count = 0;
            foreach (Contact c in contactList)
            {
                count++;
                progress.Report(count);

                _dataManager.LiveRepo.Export(c, livefile);
                _dataManager.DeadRepo.Export(c, deadfile);
                _dataManager.AncestorRepo.Export(c, ancestorfile);

                if (token.IsCancellationRequested)
                    break;
            }
        }

        public void ExportCancel()
        {
            cts.Cancel();
            HideExportProgress();
        }

        public void UpdateExportProgress(int count, int total)
        {
            ExportProgressValue = Math.Floor(((double)count) * 100 / ((double)total));
            ExportProgressText = "Export " + count + " of " + total + " contacts";
            RefreshProgress();
        }

        public void ShowExportProgress()
        {
            ExportProgressVisibility = Visibility.Visible;
            ExportButtonVisibility = Visibility.Collapsed;
            RefreshProgress();
        }

        public void HideExportProgress()
        {
            ExportProgressVisibility = Visibility.Collapsed;
            ExportButtonVisibility = Visibility.Visible;
            RefreshProgress();
        }

        public void RefreshProgress()
        {
            NotifyOfPropertyChange(nameof(ExportProgressVisibility));
            NotifyOfPropertyChange(nameof(ExportButtonVisibility));
            NotifyOfPropertyChange(nameof(ExportProgressValue));
            NotifyOfPropertyChange(nameof(ExportProgressText));
        }
        #endregion

        #region Backup
        public void Backup()
        {
            string backupfolder = PlaqueConfig.GetBackupFolder();

            File.Copy(PlaqueConfig.CurrentDataFile, backupfolder + "\\pwdatabase_backup_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".db");

            DialogHelper.ShowMessageBox("Data_BackupSuccessFull");
        }
        #endregion

        #region Import
        public void ImportLive()
        {
            string filename = DialogHelper.OpenFile(Filter);
            if (String.IsNullOrEmpty(filename) || !File.Exists(filename)) return;

            var import = new ImportBase<LiveImportRecord>(_dataManager);
            import.Import(filename);
            UpdateResult(import);
        }

        public void ImportDead()
        {
            string filename = DialogHelper.OpenFile(Filter);
            if (String.IsNullOrEmpty(filename) || !File.Exists(filename)) return;

            var import = new ImportBase<DeadImportRecord>(_dataManager);
            import.Import(filename);

            UpdateResult(import);
            
        }

        public void ImportAncestor()
        {
            string filename = DialogHelper.OpenFile(Filter);
            if (String.IsNullOrEmpty(filename) || !File.Exists(filename)) return;

            var import = new ImportBase<AncestorImportRecord>(_dataManager);
            import.Import(filename);

            UpdateResult(import);
        }

       

        public void UpdateResult(IImport import)
        {
            SucessFileName = import.Success.ResultFileName;
            ErrorFileName = import.Error.ResultFileName;
            DuplicatedFileName = import.Duplicated.ResultFileName;
            SucessInfo = import.Success.Count + " imported.";
            ErrorInfo = import.Error.Count + " error.";
            DuplicatedInfo = import.Duplicated.Count + " duplicated.";

            NotifyOfPropertyChange(nameof(SucessInfo));
            NotifyOfPropertyChange(nameof(ErrorInfo));
            NotifyOfPropertyChange(nameof(DuplicatedInfo));
        }
        #endregion



        public void ViewSucess()
        {
            if (String.IsNullOrEmpty(SucessFileName)) return;
            Process.Start(SucessFileName);
        }

        public void ViewDuplicated()
        {
            if (String.IsNullOrEmpty(DuplicatedFileName)) return;
            Process.Start(DuplicatedFileName);
        }

        public void ViewError()
        {
            if (String.IsNullOrEmpty(ErrorFileName)) return;
            Process.Start(ErrorFileName);
        }
    }
}
