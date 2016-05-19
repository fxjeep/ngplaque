using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Xps.Packaging;
using System.Windows.Documents;
using PlaqueData;

namespace PlaqueManager.Generator
{
    public class AncestorXPS
    {
        string WSXps;
        string Name;

        public AncestorXPS(string filename)
        {
            FileInfo info = new FileInfo(filename);
            this.Name = info.Name;
            this.WSXps = Path.Combine(info.Directory.FullName, info.Name + "_ZX.xps");
        }

        public void Print(List<Ancestor> list)
        {
            if (list == null || list.Count == 0) return;
            var blocklist = list.Batch(10);

            FixedDocument cs_doc = new FixedDocument();
            cs_doc.DocumentPaginator.PageSize = new Size(Configure.Width, Configure.Height);

            if (File.Exists(this.WSXps)) File.Delete(this.WSXps);
            var xpsd = new XpsDocument(this.WSXps, FileAccess.ReadWrite);
            var xw = XpsDocument.CreateXpsDocumentWriter(xpsd);

            int p = 1;
            foreach (var onepage in blocklist)
            {
                PageContent pc = CreateZXPage(onepage, p, cs_doc);
                cs_doc.Pages.Add(pc);
                p++;
            } //end of all cs print

            xw.Write(cs_doc);
            xpsd.Close();
        }

        public PageContent CreateZXPage(IEnumerable<Ancestor> onepagerec, int page, FixedDocument doc)
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
                var ancestor = (i < list.Count()) ? list[i] : new Ancestor();
                BlockZX block = PrintHelper.PrintZXBlock(ancestor.Surname, ancestor.Name, fp, i);
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
