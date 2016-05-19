using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Xps;
using System.Windows.Markup;
using System.Windows.Controls;
using System.Windows.Xps.Packaging;
using System.Windows.Documents;
using PlaqueData;

namespace PlaqueManager.Generator
{
    public class PrintListXPS
    {
        string Xps;
        string Name;

        public PrintListXPS(string filename)
        {
            FileInfo info = new FileInfo(filename);
            this.Name = info.Name;
            this.Xps = Path.Combine(info.Directory.FullName, info.Name + "_prnlist.xps");
        }

        public void Print(ObservableCollection<ContactCount> list)
        {
            if (list == null || list.Count == 0) return;
            var groups = list.Batch(Configure.IndexRecPerColumn);
            int totalpage = (int)Math.Ceiling((double)list.Count/Configure.IndexRecPerColumn);
            PrintContactList(groups, totalpage);
        }

        public void PrintContactList(IEnumerable<IEnumerable<ContactCount>> list, int totalpage)
        {
            FixedDocument doc = new FixedDocument();
            doc.DocumentPaginator.PageSize = new Size(Configure.Width, Configure.Height);
            if (File.Exists(this.Xps)) File.Delete(this.Xps);

            XpsDocument xpsd = new XpsDocument(this.Xps, FileAccess.ReadWrite);
            XpsDocumentWriter xw = XpsDocument.CreateXpsDocumentWriter(xpsd);

            //print each blocks
            int p = 1;
            foreach (var onepage in list)
            {
                PageContent pc = CreatePrintContactListPage(onepage, p, doc, totalpage);
                doc.Pages.Add(pc);
                p++;
            }

            xw.Write(doc);
            xpsd.Close();
        }

        public PageContent CreatePrintContactListPage(IEnumerable<ContactCount> onepagerec, 
                        int page, FixedDocument doc, int totalpage)
        {
            PageContent pc = new PageContent();
            var fp = new FixedPage
            {
                Width = doc.DocumentPaginator.PageSize.Width,
                Height = doc.DocumentPaginator.PageSize.Height
            };

            var list = onepagerec.ToList();

            ContactReportPage contrpt = new ContactReportPage();
            contrpt.Title.Text = contrpt.Title.Text.Replace("[date]", 
                DateTime.Now.ToString("yyyyMMdd") + " Page " +
                page + " of " + totalpage);
            
            contrpt.Width = Configure.Width;
            contrpt.Height = Configure.Height;
            
            contrpt.LeftTable.DataContext = list;
            
            fp.Children.Add(contrpt);

            fp.Measure(new Size(Configure.Width, Configure.Height));
            fp.Arrange(new Rect(new Point(), new Size(Configure.Width, Configure.Height)));
            fp.UpdateLayout();
            ((IAddChild)pc).AddChild(fp);
            return pc;
        }
    }
}
