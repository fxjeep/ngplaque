using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaqueData
{
    public interface IImport
    {
        ImportResult Success { get; }
        ImportResult Error { get; }
        ImportResult Duplicated { get; }
    }

    public class ImportBase<T> : IImport
            where T : ImportRecordBase, new()
    {
        public ImportResult Success { get; private set; }
        public ImportResult Error { get; private set; }
        public ImportResult Duplicated { get; private set; }
        private IDataManager _dataManager;

        public ImportBase(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public void Import(string filename)
        {
            Success = new ImportResult(filename, "success_");
            Error = new ImportResult(filename, "error_");
            Duplicated = new ImportResult(filename, "duplicated_");

            var sr = new StreamReader(filename);
            string line = "";
            while (!String.IsNullOrEmpty(line = sr.ReadLine()))
            {
                ImportLine(line);
            }

            Success.Close();
            Duplicated.Close();
            Error.Close();
        }

        public void ImportLine(string line)
        {
            string[] fields = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var rec = new T();
            rec.Initial(fields);

            if (rec.HasError)
            {
                Error.Log(rec.GetOutputString());
                return;
            }

            rec.Import(_dataManager);
            if (rec.HasError)
            {
                Error.Log(rec.GetOutputString());
                return;
            }

            if (rec.IsDuplicated)
            {
                Duplicated.Log(rec.GetOutputString());
            }
            else
            {
                Success.Log(rec.GetOutputString());
            }
                
        }
    }

    public abstract class ImportRecordBase
    {
        public string ContactName { get; set; }
        public string ContactCode { get; set; }
        public string Error { get; set; }
        public bool IsDuplicated { get; set; }
        public bool HasError
        {
            get { return !string.IsNullOrEmpty(Error); }
        }

        public abstract void Initial(string[] fields);
        public abstract string GetOutputString();
        public abstract void Import(IDataManager dataManager);
    }

    public class ImportResult
    {
        public StreamWriter ResultFileSW;
        public string ResultFileName;
        public int Count;

        public ImportResult(string filename, string keyword)
        {
            FileInfo original = new FileInfo(filename);
            string surfix = DateTime.Now.ToString("yyyyMMdd_mmhhss") + ".txt";
            ResultFileName = Path.Combine(original.DirectoryName, original.Name + keyword + surfix);
            ResultFileSW = new StreamWriter(ResultFileName, true);
            Count = 0;
        }

        public void Log(string line)
        {
            ResultFileSW.WriteLine(line);
            Count++;
        }

        public void Close()
        {
            ResultFileSW.Close();
        }
    }
}
