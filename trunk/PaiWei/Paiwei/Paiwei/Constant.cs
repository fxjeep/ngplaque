using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing;

namespace Paiwei
{
	public class Prints
	{
		public static List<PrintType> TypeList =
			new List<PrintType>() { 
				new PrintType("长生禄位", TypeValues.ChangeSheng),
				new PrintType("冤亲债主", TypeValues.YuanQing),
				new PrintType("往生莲位", TypeValues.WangSheng),
				new PrintType("历代祖先", TypeValues.ZhuXian),
			};
	}

	public class PrintType
	{
		public string Name { get; set; }
		public TypeValues Value { get; set; }

		public PrintType(string name, TypeValues value)
		{
			this.Name = name;
			this.Value = value;
		}
	}

	public enum TypeValues
	{
		ChangeSheng,
		YuanQing,
		WangSheng,
		ZhuXian
	}

	public class PDFHelp
	{
		public static float Cm2Unit(float cm)
		{
			return (float)cm / 2.54f * 72f;
		}

		public static void PrintBox(PdfWriter CurrentPDFWriter, RectangleF box)
		{
			CurrentPDFWriter.DirectContent.SetColorStroke(new GrayColor(1));
			CurrentPDFWriter.DirectContent.MoveTo(PDFHelp.Cm2Unit(box.Left), PDFHelp.Cm2Unit(29.7f - box.Top));
			CurrentPDFWriter.DirectContent.LineTo(PDFHelp.Cm2Unit(box.Right), PDFHelp.Cm2Unit(29.7f - box.Top));
			CurrentPDFWriter.DirectContent.LineTo(PDFHelp.Cm2Unit(box.Right), PDFHelp.Cm2Unit(29.7f - box.Bottom));
			CurrentPDFWriter.DirectContent.LineTo(PDFHelp.Cm2Unit(box.Left), PDFHelp.Cm2Unit(29.7f - box.Bottom));
			CurrentPDFWriter.DirectContent.LineTo(PDFHelp.Cm2Unit(box.Left), PDFHelp.Cm2Unit(29.7f - box.Top));
			CurrentPDFWriter.DirectContent.Stroke();
		}
	}

    /// <summary>
    /// To make my life easyer, this class accept left, top and width and height.
    /// but it still uses pdf coordinate. to bottom = top -height, right = left+width.
    /// </summary>
    public class PDFRectangle
    {
        private int _left;
        public int Left { get { return _left; } }

        private int _top;
        public int Top { get { return _top; } }

        private int _bottom;
        public int Bottom { get { return _bottom; } }

        private int _right;
        public int Right { get { return _right; } }

        public PDFRectangle(float left, float top, float width, float height)
        {
            _left = (int)(PDFHelp.Cm2Unit(left));
            _right = (int)(PDFHelp.Cm2Unit(left + width));

            _top = (int)(PDFHelp.Cm2Unit(top));
            _bottom = (int)(PDFHelp.Cm2Unit(top - height));
        }
    }
}
