using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PlaqueData
{
    public enum Language { En, Ch }

    public class PlaqueConfig
    {
        public const string LanguageConfig = "Language";
        public const string DataFolderConfig = "DataFolder";
        public const string Section = "PlaqueManager";

        public const string DefaultDataFileName = "pwdatabase.db3";
        public const string DefaultFolder = "./DefaulDataFolder";

        public static string LanguageValue { get; private set; }
        public static string CurrentFolder { get; set; }

        public static string CurrentDataFile
        {
            get
            {
                if (!Directory.Exists(CurrentFolder))
                {
                    try
                    {
                        Directory.CreateDirectory(CurrentFolder);
                    }
                    catch
                    {
                        //if ini file has incorrect path configuration
                        //so we failed to create path.
                        return ".\\"+DefaultDataFileName;
                    }
                }  

                return Path.Combine(CurrentFolder, DefaultDataFileName);
            }
        }

        public static string GetExportFolder()
        {
            string path = PlaqueConfig.CurrentFolder + "\\Export\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        public static string GetPrintFolder()
        {
            string path = PlaqueConfig.CurrentFolder + "\\Print\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        public static string GetBackupFolder()
        {
            string path = PlaqueConfig.CurrentFolder + "\\Backup\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }


        public static Language LanguageState
        {
            get { return (Language)Enum.Parse(typeof(Language), LanguageValue); }
            set { LanguageValue = value.ToString(); }
        }

        private static string _exePath = Assembly.GetExecutingAssembly().CodeBase;
        private static IniFile _iniReader;

        static PlaqueConfig()
        {
            FileInfo info = new FileInfo(_exePath.Replace("file:///", ""));
            string inifile = Path.Combine(info.Directory.FullName, "PlaqueManager.ini");
            if (!File.Exists(inifile))
                File.Create(inifile).Dispose();
            
            _iniReader = new IniFile(inifile);

            ReadAllConfig();
        }

        public static void ReadAllConfig()
        {
            LanguageValue = PlaqueConfig.ReadConfig(LanguageConfig);
            if (String.IsNullOrEmpty(LanguageValue)) LanguageValue = "En";

            string dataFile = PlaqueConfig.ReadConfig(DataFolderConfig);
            CurrentFolder = (string.IsNullOrEmpty(dataFile)) ? DefaultFolder : dataFile;

            var dirInfo = new DirectoryInfo(CurrentFolder);
            CurrentFolder = dirInfo.FullName;
        }

        public static void SaveAllConfig()
        {
            PlaqueConfig.SaveConfig(LanguageConfig, LanguageValue);
            PlaqueConfig.SaveConfig(DataFolderConfig, CurrentFolder);
        }

        public static string ReadConfig(string key)
        {
            return _iniReader.IniReadValue(Section, key);
        }

        public static void SaveConfig(string key, string value)
        {
            _iniReader.IniWriteValue(Section, key, value);
        }
    }

    #region inifile reader/writer
    public class IniFile
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public IniFile(string INIPath)
        {
            path = INIPath;
        }
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
                                            255, this.path);
            return temp.ToString();

        }
    }
    #endregion
}
