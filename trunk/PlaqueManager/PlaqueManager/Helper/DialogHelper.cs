using System;
using System.Reflection;
using System.Resources;
using System.Windows;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace PlaqueManager.Helper
{
    public class DialogHelper
    {
        public static string OpenFile(string filter)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = filter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            return "";
        }

        public static string OpenFolder()
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            return "";
        }

        public static MessageBoxResult ShowMessageBox(string titleKey, string textKey, params object[] objects)
        {
            ResourceManager rm = new ResourceManager(Constants.ResourceName, Assembly.GetExecutingAssembly());
            string title = rm.GetString(titleKey);
            string mesg = String.Format(rm.GetString(textKey), objects);
            rm.ReleaseAllResources();
            return System.Windows.MessageBox.Show(mesg, title, MessageBoxButton.OKCancel);
        }

        public static MessageBoxResult ShowMessageBox(string key)
        {
            ResourceManager rm = new ResourceManager(Constants.ResourceName, Assembly.GetExecutingAssembly());
            string mesg = rm.GetString(key);
            rm.ReleaseAllResources();
            return System.Windows.MessageBox.Show(mesg);
        }        
    }

    public static class Extension
    {
        public static void Sort<TSource, TKey>(this ObservableCollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            List<TSource> sortedList = source.OrderBy(keySelector).ToList();
            source.Clear();
            foreach (var sortedItem in sortedList)
                source.Add(sortedItem);
        }

        public static string GetResourceText(string key)
        {
            ResourceManager rm = new ResourceManager(Constants.ResourceName, Assembly.GetExecutingAssembly());
            string text = rm.GetString(key);
            rm.ReleaseAllResources();
            return text;
        }
    }
}
