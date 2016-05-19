using System;
using System.Collections.Generic;
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
    public class LiveXPS
    {
        string YQXps;
        string CSXps;
        string RootPath;
        string Name;

        public LiveXPS(string filename)
        {
            FileInfo info = new FileInfo(filename);
            Name = info.Name;
            YQXps = Path.Combine(info.Directory.FullName, info.Name+"_YQ.xps");
            CSXps = Path.Combine(info.Directory.FullName, info.Name+"_CS.xps");
            RootPath = info.Directory.FullName;
        }

        public void Print(List<Live> list)
        {
            if (list == null || list.Count == 0) return;

            var blocklist = list.Batch(10);
            PrintCS(blocklist);
            PrintYQ(blocklist);
        }

        public void PrintCS(IEnumerable<IEnumerable<Live>> list)
        {
            FixedDocument cs_doc = new FixedDocument();
            cs_doc.DocumentPaginator.PageSize = new Size(Configure.Width, Configure.Height);
            if (File.Exists(this.CSXps)) File.Delete(this.CSXps);

            XpsDocument xpsd = new XpsDocument(this.CSXps, FileAccess.ReadWrite);
            XpsDocumentWriter xw = XpsDocument.CreateXpsDocumentWriter(xpsd);

            //print each blocks
            int p = 1;
            foreach(var onepage in list)
            {
                PageContent pc = CreateCSPage(onepage, p, cs_doc);
                cs_doc.Pages.Add(pc);
                p++;
            } //end of all cs print

            xw.Write(cs_doc);
            xpsd.Close();
        }

        public PageContent CreateCSPage(IEnumerable<Live> onepagerec, int page, FixedDocument doc)
        {
            PageContent pc = new PageContent();
            var fp = new FixedPage{Width = doc.DocumentPaginator.PageSize.Width,
                                   Height = doc.DocumentPaginator.PageSize.Height};

            PrintHelper.PrintPageNoPageName(fp, page, this.Name);

            var list = onepagerec.ToList();

            for (int i = 0; i < 10; i++)
            {
                var live = (i < list.Count()) ? list[i] : new Live();
                BlockCS block = PrintHelper.PrintCSBlock(live.Name, fp, i);
                fp.Children.Add(block);
            }
            fp.Measure(new Size(Configure.Width, Configure.Height));
            fp.Arrange(new Rect(new Point(), new Size(Configure.Width, Configure.Height)));
            fp.UpdateLayout();
            ((IAddChild)pc).AddChild(fp);
            return pc;
        }

        public void PrintYQ(IEnumerable<IEnumerable<Live>> list)
        {
            FixedDocument yq_doc = new FixedDocument();
            yq_doc.DocumentPaginator.PageSize = new Size(Configure.Width, Configure.Height);

            if (File.Exists(this.YQXps)) File.Delete(this.YQXps);
            XpsDocument xpsd = new XpsDocument(this.YQXps, FileAccess.ReadWrite);
            XpsDocumentWriter xw = XpsDocument.CreateXpsDocumentWriter(xpsd);


            //print each blocks
            int p = 1;
            foreach (var onepage in list)
            {
                PageContent pc = CreateYQPage(onepage, p, yq_doc);
                yq_doc.Pages.Add(pc);
                p++;
            } //end of all cs print

            xw.Write(yq_doc);
            xpsd.Close();
        }

        public PageContent CreateYQPage(IEnumerable<Live> onepagerec, int page, FixedDocument doc)
        {
            PageContent pc = new PageContent();
            var fp = new FixedPage
            {
                Width = doc.DocumentPaginator.PageSize.Width,
                Height = doc.DocumentPaginator.PageSize.Height
            };

            PrintHelper.PrintPageNoPageName(fp, page, this.Name);

            var list = onepagerec.ToList();

            for (int i = 0; i < 10; i++)
            {
                var live = (i < list.Count()) ? list[i] : new Live();
                BlockYQ block = PrintHelper.PrintYQBlock(live.Name, fp, i);
                fp.Children.Add(block);
            }
            fp.Measure(new Size(Configure.Width, Configure.Height));
            fp.Arrange(new Rect(new Point(), new Size(Configure.Width, Configure.Height)));
            fp.UpdateLayout();
            ((IAddChild)pc).AddChild(fp);
            return pc;
        }
    }
}
