using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Drawing2D;


namespace Paiwei
{
    /// <summary>
    /// DefaultPaiWeiPrinter will print
    /// </summary>
    public class DefaultPaiWeiPrinter 
    {
        protected Document CurrentPDF;
        protected PdfWriter CurrentPDFWriter;
        protected BaseFont pagefont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, false);

        public RectangleF _namebox;
        public RectangleF NameBox
        {
            get { return _namebox; }
            set { _namebox = value; }
        }

        public TypeValues DocType { get; set; }

        private int namecount;

        public void Initial(string pdffilename)
        {
            CurrentPDF = new Document(PageSize.A4);
            CurrentPDFWriter = PdfWriter.GetInstance(CurrentPDF, new FileStream(pdffilename, FileMode.Create));
            CurrentPDF.Open();
        }

        public void FinalisePDF()
        {
            CurrentPDF.Close();
        }

        /// <summary>
        /// Print a list of names under a context. Context contains the setting for font, positions
        /// and background images.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="namedata"></param>
        public void PrintData(PDFCreationParameter context, List<List<string>> namedata)
        {

            bool stop = false;
            namecount = context.StartNo;
            int pagecount = 1;
            while (!stop)
            {
                //create new page
                CurrentPDF.NewPage();
                for (int y = 0; y < context.NumPrintY; y++)
                {
                    for (int x = 0; x < context.NumPrintX; x++)
                    {
                        //print image and texts
                        float img_x = x * context.ImageWidth + context.MarginLeft;
                        float img_y = y * context.ImageHeight + context.MargionTop;

                        List<string> liststring = GetNextValidName(namedata);
                        if (liststring != null)
                        {
                            PrintImage(context, img_x, img_y);


                            if (liststring.Count != 0 && !String.IsNullOrEmpty(liststring[0]))
                            {
                                this.PrintName(context, liststring, CurrentPDFWriter, new PointF(img_x, img_y));
                            }
                        }
                        else
                        {
                            // now all the names are printed, we still need to print blank images
                            // to fill the A4 page.
                            stop = true;
                            PrintImage(context, img_x, img_y);
                        }
                    }
                }

                //print page number
                string page = "No. " + pagecount.ToString("00000");
                CurrentPDFWriter.DirectContent.BeginText();
                CurrentPDFWriter.DirectContent.SetFontAndSize(pagefont, 10f);
                CurrentPDFWriter.DirectContent.ShowTextAligned(
                        Element.ALIGN_CENTER,
                        page, 
                        PDFHelp.Cm2Unit(10.5f), 
                        PDFHelp.Cm2Unit(1), 
                        0 );
                CurrentPDFWriter.DirectContent.EndText();
                pagecount++;

            }
        }

        private List<string> GetNextValidName(List<List<string>> namedata)
        {
            bool stop = false;
            while (!stop)
            {
                if (namecount < namedata.Count)
                {
                    List<string> liststring = namedata[namecount];
                    this.namecount++;
                    if (liststring.Count != 0 && !String.IsNullOrEmpty(liststring[0]))
                    {
                        return liststring;
                    }
                }
                else
                {
                    stop = true;
                }
            }
            return null;
        }

        /// <summary>
        /// Main function to print name
        /// </summary>
        /// <param name="liststring"></param>
        /// <param name="writer"></param>
        /// <param name="lefttop"></param>
        public virtual void PrintName(PDFCreationParameter context, List<string> liststring, PdfWriter writer, PointF lefttop)
        {
            //calculate font size according to the number of characters and 
            //size of box.
            string name = liststring[0];

            //we now get a List<string>, since we think the string may mixed with chinese, english.
            NameSegments name_segments = new NameSegments(name);

            //PrintParameter printparam = this.SetBoxPrintParameter(name_segments.Length, this.NameBox, this.FontSize);
            this.PrintMainBox(context, name_segments, writer, lefttop);
        }

        public void PrintMainBox(PDFCreationParameter context, NameSegments segments, PdfWriter writer, PointF lefttop)
        {
            float LineHeight = context.MaximumMainLineHeight(segments) + context.ExtraLineSpaceMain;
            int CharsPerCol = (int)(this.NameBox.Height / LineHeight);

            int count = 0;

            //get char_x, char_x is always the middle of the box.
            float char_x = lefttop.X + this.NameBox.Left;

            float char_y = lefttop.Y + this.NameBox.Top;

            if (context.IsPrintNameBox)
            {
                PDFHelp.PrintBox(writer,
                    new System.Drawing.RectangleF(lefttop.X + this.NameBox.Left, char_y, this.NameBox.Width,
                 this.NameBox.Height));
            }

            
            // align to center
            if (segments.Length < CharsPerCol)
            {
                //if we put it centred, we don't move 1 line down.
                char_y = lefttop.Y + this.NameBox.Top + (this.NameBox.Height - segments.Length * LineHeight) / 2;
            }
            else
            {
                //otherwise, we move one line down.
                //this is because char_y is baseline position. If we don't move.
                //it will print on top of the rectangle box.
                char_y += LineHeight;
            }
            
            
            foreach (Segment seg in segments.namelists)
            {
                string c = seg.Text;
                
                writer.DirectContent.BeginText();
                writer.DirectContent.SetFontAndSize(context.GetBaseFont(seg), context.GetMainFontSize(seg));
                writer.DirectContent.ShowTextAligned(Element.ALIGN_CENTER,
                        c, PDFHelp.Cm2Unit(char_x+this.NameBox.Width/2), PDFHelp.Cm2Unit(29.7f - char_y), 0);
                writer.DirectContent.EndText();
                char_y += LineHeight;
                
                count++;

            }
        }

        /// <summary>
        /// Print background image with a box.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void PrintImage(PDFCreationParameter context, float x, float y)
        {
            context.Image.SetAbsolutePosition(PDFHelp.Cm2Unit(x),
                PDFHelp.Cm2Unit(29.7f - y - context.ImageHeight));
            CurrentPDF.Add(context.Image);

            RectangleF box = new RectangleF(x, y, context.ImageWidth, context.ImageHeight);
            PDFHelp.PrintBox(CurrentPDFWriter, box);
        }

        /// <summary>
        /// This function will print wrapped english or vietanmese 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="box"></param>
        /// <param name="rotate"></param>
        /// <param name="font"></param>
        /// <param name="size"></param>
        public void PrintWrapTextInBox(NameSegments segments,
                                   System.Drawing.RectangleF box,
                                   TextOrientation rotate,
                                   PDFCreationParameter context,
                                   bool isprintbox
                                   )
        {
            PdfPTable table = new PdfPTable(1);
            table.SetTotalWidth(new float[] { PDFHelp.Cm2Unit(box.Width) });

            PdfPCell cell = new PdfPCell();
            Phrase p = new Phrase();
            float maxline = 0;
            foreach(Segment seg in segments.namelists)
            {
                float fontsize = context.GetSideFontSize(seg);
                iTextSharp.text.Font font = new iTextSharp.text.Font(context.GetBaseFont(seg),
                    fontsize, iTextSharp.text.Font.NORMAL);
                float lineheight = context.GetSideLineHeight(seg);

                if (lineheight > maxline)
                {
                    maxline = lineheight;
                }

                if (seg.Text == "")
                {
                    p.Add(new Chunk("  ", font));
                }
                else
                {
                    Chunk c = new Chunk(seg.Text, font);
                    p.Add(c);

                    if (seg.Type != TextType.Chinese)
                    {
                        p.Add(new Chunk("  ", font));
                    }
                }                
            }
            cell.AddElement(p);
            
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.PaddingTop = 0;
            cell.PaddingLeft = 0;
            cell.PaddingRight = 0;
            cell.PaddingBottom = 0;

 
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Rotation = 270;

            cell.FixedHeight = PDFHelp.Cm2Unit(box.Height);
            table.AddCell(cell);
            table.WriteSelectedRows(0, 1, 0, 1,
                                    PDFHelp.Cm2Unit(box.X + box.Width - maxline), //we print 1 line below the right edge.
                                    PDFHelp.Cm2Unit(29.7f - box.Y),
                                    this.CurrentPDFWriter.DirectContent);

        }
    }


    public class TwoBoxesPrinter : DefaultPaiWeiPrinter
    {
        public RectangleF LiveNameBox { get; set; }

        /// <summary>
        /// Print name on side box. If it is chinese, we print each characters vertically.
        /// If it is english or vietanmese we should rotate text 90 degree.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="segments"></param>
        /// <param name="writer"></param>
        /// <param name="lefttop"></param>
        public virtual void PrintSideBox(PDFCreationParameter context, NameSegments segments, PdfWriter writer, PointF lefttop)
        {
            //this requires that texts in all segments are same type.
            //since we can not print a rotated english/vietanmese with vertical chinese texts.
            if (!segments.IsSegmentsSameType)
            {
                throw new ArgumentException("The names printed on left side in "+segments.FullText 
                                            + " is mixed with Chinese, English or Vietanmese");
            }



            //if it is chinese , we print each segment in one line
            if (segments.FirstType == TextType.Chinese)
            {
                float LineHeight = context.MaximumMainLineHeight(segments) + context.ExtraLineSpaceSide;
                int CharsPerCol = (int)(this.LiveNameBox.Height / LineHeight);

                int count = 0;

                //get char_x, char_x is always the middle of the box.
                float char_x = lefttop.X + this.LiveNameBox.Left + this.LiveNameBox.Width / 2;

                float char_y = lefttop.Y + this.LiveNameBox.Top;

                if (context.IsPrintNameBox)
                {
                    PDFHelp.PrintBox(writer, new System.Drawing.RectangleF(lefttop.X + this.LiveNameBox.Left, char_y, this.LiveNameBox.Width,
                     this.LiveNameBox.Height));
                }

                foreach (Segment seg in segments.namelists)
                {
                    string c = seg.Text;
                    char_y += LineHeight;
                    writer.DirectContent.BeginText();
                    writer.DirectContent.SetFontAndSize(context.GetBaseFont(seg), context.GetSideFontSize(seg));
                    writer.DirectContent.ShowTextAligned(Element.ALIGN_CENTER,
                            c, PDFHelp.Cm2Unit(char_x), PDFHelp.Cm2Unit(29.7f - char_y), 0);
                    writer.DirectContent.EndText();
                    count++;

                }
            }
            /*else
            {
                //it should use column text to get wrap effect.
                //if it is english or vietanmese, we print segements vertically, rotated.
                float LineHeight = context.MaximumMainLineHeight(segments) + context.ExtraLineSpaceSide;
                int CharsPerCol = (int)(this.LiveNameBox.Width / LineHeight);

                int count = 0;

                //get char_x, char_x is always the middle of the box.
                float char_x = lefttop.X + this.LiveNameBox.Left;
                float char_y = lefttop.Y + this.LiveNameBox.Top;

                string c = segments.FullText;
                float fontsize = context.GetSideFontSize(segments.namelists[0]);
                iTextSharp.text.Font font = new iTextSharp.text.Font(context.GetBaseFont(segments.namelists[0]),
                    fontsize, iTextSharp.text.Font.NORMAL);

                PrintWrapTextInBox(c,
                                new System.Drawing.RectangleF(char_x, char_y, this.LiveNameBox.Width, this.LiveNameBox.Height),
                                TextOrientation.Vertical270, 
                                font, 
                                fontsize, 
                                context.IsPrintNameBox);

                count++;
            }*/
        }

 
    }

	/// <summary>
	/// The parameters for 长生禄位
	/// </summary>
	public class ChangShengYuanQingPrinter : DefaultPaiWeiPrinter
	{
        public ChangShengYuanQingPrinter()
		{
			NameBox = new RectangleF(1.82f, 6.1f, 0.4f, 3.6f);
			this.DocType = TypeValues.ChangeSheng;
		}
	}

    /// <summary>
    /// printer for 冤亲债主
    /// </summary>
    public class YuanQingPrinter : DefaultPaiWeiPrinter
	{
        public YuanQingPrinter()
		{
            NameBox = new RectangleF(0.1f, 7.328f, 0.6f, 3.3f);
		}

        /// <summary>
        /// Because yuanqin will print name on side. so we override default PrintName function.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="liststring"></param>
        /// <param name="writer"></param>
        /// <param name="lefttop"></param>
        public override void PrintName(PDFCreationParameter context, List<string> liststring, PdfWriter writer, PointF lefttop)
        {
            //calculate font size according to the number of characters and 
            //size of box.
            string name = liststring[0];

            //we now get a List<string>, since we think the string may mixed with chinese, english.

            NameSegments segments = new NameSegments(name);


            if (segments.Type != TextType.Chinese || segments.Type == TextType.Mixed)
            {
                //for english or vietanmese we print name vertically
                float LineHeight = context.MaximumMainLineHeight(segments) + context.ExtraLineSpaceMain;
                int CharsPerCol = (int)(this.NameBox.Width / LineHeight);

                //get char_x, char_x is always the middle of the box.

                float char_x = lefttop.X + 0;
                float char_y = lefttop.Y + this.NameBox.Top;

                //string c = segments.FullText;

                if (context.IsPrintNameBox)
                {
                    PDFHelp.PrintBox(writer,
                        new System.Drawing.RectangleF(lefttop.X + this.NameBox.Left, char_y, this.NameBox.Width,
                     this.NameBox.Height));
                }

                float fontsize = context.GetSideFontSize(segments.namelists[0]);
                iTextSharp.text.Font font = new iTextSharp.text.Font(context.GetBaseFont(segments.namelists[0]),
                    fontsize, iTextSharp.text.Font.NORMAL);

                PrintWrapTextInBox(segments, new System.Drawing.RectangleF(char_x, char_y, this.NameBox.Width, this.NameBox.Height),
                                    TextOrientation.Vertical270, context, context.IsPrintNameBox);
            }
            else
            {
                base.PrintName(context, liststring,writer, lefttop);
            }
        }

       
	}

    /// <summary>
    /// 往生莲位
    /// </summary>
	public class WangShengPrinter : TwoBoxesPrinter
	{
        public WangShengPrinter()
		{
			NameBox = new RectangleF(1.88f, 5.3f, 0.4f, 4.2f);
			LiveNameBox = new RectangleF(0.01f, 6.2f, 0.8f, 4.4f);
		}


        /// <summary>
        /// Print name for wangsheng lianwei.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="liststring"></param>
        /// <param name="writer"></param>
        /// <param name="lefttop"></param>
        public override void PrintName(PDFCreationParameter context, List<string> liststring, PdfWriter writer, PointF lefttop)
		{

            //calculate font size according to the number of characters and 
            //size of box.
            string name = liststring[0];

            //we now get a List<string>, since we think the string may mixed with chinese, english.
            NameSegments name_segments = new NameSegments(name);

            //PrintParameter printparam = this.SetBoxPrintParameter(name_segments.Length, this.NameBox, this.FontSize);
            this.PrintMainBox(context, name_segments, writer, lefttop);

            //we have following different styles:
            //1. relationship is in chinese, name is in chinese
            //2. relationship is in chinese, name is in english/vietanmese
            //3. relationship is in english/vietanmese, name is in chinese --- illegal
            //4. relationship is in english/vietanmese, name is in english/vietanmese
            //name must be in same type.
            NameSegments relationship_seg = new NameSegments(liststring[2]);
            NameSegments sidename_seg = new NameSegments(liststring[1]);

            if (!sidename_seg.IsSegmentsSameType)
            {
                throw new ArgumentException("Name " + liststring[1] + " is mixed with Chinese/English/Vietnamese, please using just one language");
            }

            if (!relationship_seg.IsSegmentsSameType)
            {
                throw new ArgumentException("Relationship " + liststring[2] + " is mixed with Chinese/English/Vietnamese, please using just one language");
            }

            if (relationship_seg.namelists.Count != 0 &&
                 sidename_seg.namelists.Count != 0)
            {
                if (relationship_seg.namelists[0].Type != TextType.Chinese &&
                     sidename_seg.namelists[0].Type == TextType.Chinese)
                {
                    throw new ArgumentException("Relationship " + liststring[2] + " is not in Chinese, but Name " + liststring[1] + " is in chinese. please use both Chinese");
                }

                if (relationship_seg.namelists[0].Type == TextType.Chinese &&
                     sidename_seg.namelists[0].Type == TextType.Chinese)
                {
                    string sidename = liststring[2] + "　" + liststring[1];
                    NameSegments side_segment = new NameSegments(sidename);
                    this.PrintSideBox(context, side_segment, writer, lefttop);
                }
                else if (relationship_seg.namelists[0].Type == sidename_seg.namelists[0].Type)
                {
                    //same type, either english or vietnamese
                    string sidename = liststring[2] + "　" + liststring[1];
                    NameSegments side_segment = new NameSegments(sidename);
                    this.PrintSideBox(context, side_segment, writer, lefttop);
                }
                else if (relationship_seg.namelists[0].Type == TextType.Chinese &&
                          sidename_seg.namelists[0].Type != TextType.Chinese)
                {
                    //relationsip is chinese, but name is in english or vietnamese
                    //we print relationship, then print name.
                    this.PrintSideBox(context, relationship_seg, writer, lefttop);
                }
            }
            else
            {
                //if one of them is empty
                if (relationship_seg.namelists.Count == 0 && sidename_seg.namelists.Count == 0)
                {

                }
                else
                {
                    throw new ArgumentException("Name: " + name + " does not have relationship or lived name");
                }
            }
		}
	}

    public class ZhuXianPrinter : TwoBoxesPrinter
	{
        public ZhuXianPrinter()
		{
			NameBox = new RectangleF(1.67f, 5.65f, 0.4f, 0.8f);
			LiveNameBox = new RectangleF(0.01f, 7.5f, 0.8f, 2.5f);

			//load box and font information from configuration file.
		}

        public override void PrintName(PDFCreationParameter context, List<string> liststring, PdfWriter writer, PointF lefttop)
        {
            //calculate font size according to the number of characters and 
            //size of box.
            string name = liststring[1];

            //we now get a List<string>, since we think the string may mixed with chinese, english.
            NameSegments name_segments = new NameSegments(name);

            //PrintParameter printparam = this.SetBoxPrintParameter(name_segments.Length, this.NameBox, this.FontSize);
            this.PrintMainBox(context, name_segments, writer, lefttop);

            //please know the space we add between two names is CHINESE two-byte space
            //not english space ( which you enter by press space bar
            string sidename = liststring[0];
            NameSegments side_segment = new NameSegments(sidename);
            this.PrintSideBox(context, side_segment, writer, lefttop);
		}
	}

    public enum TextOrientation
    {
        Normal0,
        Vertical270
    }
}
