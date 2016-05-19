using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Paiwei
{
    public class PDFCreationParameter
    {
        public float    MargionTop  { get; set; }
        public float    MarginLeft  { get; set; }
        public int      StartNo     {get; set; }
        public int      EndNo       { get; set; }
        public FontParameter ChineseFontParameter       { get; set; }
        public FontParameter EnglishFontParameter       { get; set; }
        public FontParameter VietnameseFontParameter    { get; set; }
        public TypeValues Type { get; set; }
        public DefaultPaiWeiPrinter Printer { get; set; }
        public float ExtraLineSpaceMain { get; set; }
        public float ExtraLineSpaceSide { get; set; }

        public bool IsPrintNameBox { get; set; }

        public iTextSharp.text.Image Image { get; set; }
        private string _imagefile;
        public string ImageFile 
        {
            get {
                return _imagefile;
            } 
            set{
                _imagefile = value;
                System.Drawing.Image winimg = System.Drawing.Image.FromFile(_imagefile);
                float res = winimg.HorizontalResolution / 2.54f;

                Image = iTextSharp.text.Image.GetInstance(_imagefile);
                Image.ScaleAbsolute(this.ImageWidth * res, this.ImageHeight * res);
            } 
        }
	    public int NumPrintX { get { return 5; } }
		public int NumPrintY { get { return 2; } }
        public float ImageWidth { get { return  4f; } }
        public float ImageHeight { get { return 13.7f; } }

        public void PrintData(List<List<string>> namedata)
        {
            this.Printer.PrintData(this, namedata);
        }

        public float MaximumMainLineHeight(NameSegments segments)
        {
            float height = 0;
            foreach ( Segment seg in segments.namelists )
            {
                if (seg.Type == TextType.Chinese)
                {
                    if (height < this.ChineseFontParameter.MainLineHeight )
                    {
                        height = this.ChineseFontParameter.MainLineHeight;
                    }
                }
                else if (seg.Type == TextType.Vietnamese)
                {
                    if (height < this.VietnameseFontParameter.MainLineHeight )
                    {
                        height = this.VietnameseFontParameter.MainLineHeight;
                    }
                }
                else if (seg.Type == TextType.English)
                {
                    if (height < this.EnglishFontParameter.MainLineHeight)
                    {
                        height = this.EnglishFontParameter.MainLineHeight;
                    }
                }
                else
                {
                    throw new ArgumentException("incorrect value of segement type: " + seg.Type.ToString());
                }
            }

            return height;
        }

        public BaseFont GetBaseFont(Segment seg)
        {
            if (seg.Type == TextType.Chinese)
            {
                return this.ChineseFontParameter.BaseFont;
            }
            else if (seg.Type == TextType.English)
            {
                return this.EnglishFontParameter.BaseFont; 
            }
            else if (seg.Type == TextType.Vietnamese)
            {
                return this.VietnameseFontParameter.BaseFont;
            }

            return null;
        }

        public float GetMainFontSize(Segment seg)
        {
            if (seg.Type == TextType.Chinese)
            {
                return (float)this.ChineseFontParameter.MainSize;
            }
            else if (seg.Type == TextType.English)
            {
                return (float)this.EnglishFontParameter.MainSize;
            }
            else if (seg.Type == TextType.Vietnamese)
            {
                return (float)this.VietnameseFontParameter.MainSize;
            }

            return 0f;

        }

        public float GetSideLineHeight(Segment seg)
        {
            if (seg.Type == TextType.Chinese)
            {
                return (float)this.ChineseFontParameter.SideLineHeight;
            }
            else if (seg.Type == TextType.English)
            {
                return (float)this.EnglishFontParameter.SideLineHeight;
            }
            else if (seg.Type == TextType.Vietnamese)
            {
                return (float)this.VietnameseFontParameter.SideLineHeight;
            }

            return 0f;

        }

        public float GetSideFontSize(Segment seg)
        {
            if (seg.Type == TextType.Chinese)
            {
                return (float)this.ChineseFontParameter.SideSize;
            }
            else if (seg.Type == TextType.English)
            {
                return (float)this.EnglishFontParameter.SideSize;
            }
            else if (seg.Type == TextType.Vietnamese)
            {
                return (float)this.VietnameseFontParameter.SideSize;
            }

            return 0f;

        }

    }

    public class FontParameter
    {
        public string   FontName    { get; set; }
        public string   FontFile    { get; set; }
        public decimal  MainSize    { get; set; }
        public decimal  SideSize    { get; set; }
        public int      LineSpacing { get; set; }
        public TextType FontType { get; set; }
        public float    MainLineHeight { get; set; }
        public float    SideLineHeight { get; set; }
        public int      EmHeight    { get; set; }
        public BaseFont BaseFont { get; set; }

        public PrivateFontCollection pfc = new PrivateFontCollection();

        public FontParameter(string name, string file, decimal main, decimal side, TextType type)
        {
            this.FontName = name;
            this.FontFile = file;
            this.MainSize = main;
            this.SideSize = side;
            this.FontType = type;

            if (File.Exists(this.FontFile))
            {
                if (this.FontType == TextType.Chinese)
                {
                    this.BaseFont = BaseFont.CreateFont(this.FontFile+",0", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                }
                else if ( this.FontType == TextType.English)
                {
                    this.BaseFont = BaseFont.CreateFont(this.FontFile, BaseFont.CP1250, BaseFont.NOT_CACHED);
                }
                else if (this.FontType == TextType.Vietnamese)
                {
                    this.BaseFont = BaseFont.CreateFont(this.FontFile, BaseFont.CP1250, BaseFont.NOT_CACHED);
                }
                
                pfc.AddFontFile(this.FontFile);
                FontFamily ff = pfc.Families[0];
                this.LineSpacing = ff.GetLineSpacing(FontStyle.Regular);
                this.EmHeight = ff.GetEmHeight(FontStyle.Regular);

                this.MainLineHeight = (float)(( ((float)this.MainSize) * this.LineSpacing / this.EmHeight) * 2.54 / 96);
                this.SideLineHeight = (float)(( ((float)this.SideSize) * this.LineSpacing / this.EmHeight) * 2.54 / 96);
            }
            else
            {
                throw new ArgumentException(this.FontFile + " font does not exist");
            }
        }
    }
}
