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
    public class PrintDetailXPS
    {
        string Xps;
        string Name;
        string divid = "----------";
        private XpsDocument xpsd;
        private XpsDocumentWriter xw;
        private FixedDocument doc;
        private IDataManager _dataManager;

        public PrintDetailXPS(string filename, IDataManager dataManager)
        {
            FileInfo info = new FileInfo(filename);
            this.Name = info.Name;
            _dataManager = dataManager;
            this.Xps = Path.Combine(info.Directory.FullName, info.Name + "_prndetail.xps");
        }

        public void Print(Contact c)
        {
            CreateDoc();
            PrintContact(c);
            xw.Write(doc);
            CloseDoc();
        }

        public void Print(ObservableCollection<ContactCount> list)
        {
            if (list == null || list.Count == 0) return;

            CreateDoc();
            
            foreach (var cc in list)
            {
                PrintContact(cc.GetContact());
            }
            xw.Write(doc);
            CloseDoc();
        }

        public void PrintContact(Contact c)
        {
            var details = GetDetails(c.Id);
            var groups = details.Batch(Configure.DetailRecPerColumn * 2);
            int totalpage = (int)Math.Ceiling((double)details.Count / (Configure.DetailRecPerColumn * 2));
            PrintDetail(groups, totalpage, c, doc);

            //add blank page.
            if (totalpage % 2 == 1)
            {
                PageContent pc = new PageContent();
                FixedPage fp = new FixedPage();

                fp.Measure(new Size(Configure.Width, Configure.Height));
                fp.Arrange(new Rect(new Point(), new Size(Configure.Width, Configure.Height)));
                fp.UpdateLayout();
                ((IAddChild)pc).AddChild(fp);
                doc.Pages.Add(pc);
            }
        }

        public void CreateDoc()
        {
            doc = new FixedDocument();
            doc.DocumentPaginator.PageSize = new Size(Configure.Width, Configure.Height);
            if (File.Exists(this.Xps)) File.Delete(this.Xps);

            xpsd = new XpsDocument(this.Xps, FileAccess.ReadWrite);
            xw = XpsDocument.CreateXpsDocumentWriter(xpsd);
        }

        public void CloseDoc()
        {
            xpsd.Close();
        }

        public void PrintDetail(IEnumerable<IEnumerable<DetailRec>> list, int totalpage, 
            Contact cc, FixedDocument doc)
        {
            //print each blocks
            int p = 1;
            foreach (var onepage in list)
            {
                PageContent pc = CreateDetailPage(onepage, p, doc, totalpage, cc);
                doc.Pages.Add(pc);
                p++;
            }
        }

        public PageContent CreateDetailPage(IEnumerable<DetailRec> onepagerec,
                        int page, FixedDocument doc, int totalpage, Contact cc)
        {
            PageContent pc = new PageContent();
            var fp = new FixedPage
            {
                Width = doc.DocumentPaginator.PageSize.Width,
                Height = doc.DocumentPaginator.PageSize.Height
            };

            var list = onepagerec.ToList();
            for (int i = list.Count; i < Configure.DetailRecPerColumn*2; i++)
            {
                list.Add(new DetailRec());
            }

            LiveReportPage pagectrl = new LiveReportPage();
            pagectrl.Title.Text = pagectrl.Title.Text.Replace("[date]",
                    DateTime.Now.ToString("yyyyMMdd"));
            pagectrl.Title.Text = pagectrl.Title.Text.Replace("[contact]",
                cc.Name);
            pagectrl.Title.Text = pagectrl.Title.Text.Replace("[code]",
                cc.Code);
            pagectrl.Title.Text = pagectrl.Title.Text + " Page " +
                page.ToString() + " of " + (totalpage).ToString();

            pagectrl.Width = Configure.Width;
            pagectrl.Height = Configure.Height;

            pagectrl.LeftTable.DataContext =
                    list.GetRange(0, Configure.DetailRecPerColumn);

            pagectrl.RightTable.DataContext =
                 list.GetRange(Configure.DetailRecPerColumn, Configure.DetailRecPerColumn);

            fp.Children.Add(pagectrl);

            fp.Measure(new Size(Configure.Width, Configure.Height));
            fp.Arrange(new Rect(new Point(), new Size(Configure.Width, Configure.Height)));
            fp.UpdateLayout();
            ((IAddChild)pc).AddChild(fp);
            return pc;
        }

        public List<DetailRec> GetDetails(int contactid)
        {
            List<DetailRec> detailList = new List<DetailRec>();
            List<Live> livelist = _dataManager.LiveRepo.GetAll(contactid);
            List<Dead> deadlist = _dataManager.DeadRepo.GetAll(contactid);
            List<Ancestor> ancestorlist = _dataManager.AncestorRepo.GetAll(contactid);

            livelist.ForEach(l => detailList.Add(new DetailRec { Column1 = l.Name, Column2 = "", Column3 = "" }));

            if (deadlist.Count>0)
                detailList.Add(new DetailRec { Column1 = "--DeadName--", Column2 = "--LiveName--", Column3 = "--Relation--" });
            deadlist.ForEach(d => detailList.Add(new DetailRec { Column1 = d.DeadName, Column2 = d.LiveName, Column3 = d.Relation }));

            if (ancestorlist.Count>0)
                detailList.Add(new DetailRec { Column1 = "--Surname--", Column2 = "--Name--", Column3 = divid });
            ancestorlist.ForEach(a => detailList.Add(new DetailRec { Column1 = a.Surname, Column2 = a.Name, Column3 = "" }));
            
            return detailList;
        }
    }

    public class DetailRec
    {
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
    }
}
