using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Media;

namespace PlaqueManager.Generator
{
    public class Configure
    {
        public const string DefaultChineseFont = "方正报宋繁体";
        public const string DefaultEnglishFont = "Arial";

        public static int IndexColsPerPage = 1;
        public static int IndexRecPerColumn = 30;

        public static int DetailRecPerColumn = 39;
        public static int DetailColPerPage = 2;

        public static double Width = 21 / 2.54 * 96;
        public static double Height = 29.7 / 2.54 * 96;

        //range for chinese in unicode（0x4e00～0x9fff）convert to int（chfrom～chend）
        public static int ChineseFrom = Convert.ToInt32("4e00", 16);    
        public static int ChineseEnd = Convert.ToInt32("9fff", 16);

        public static double BlockWidth = 4 / 2.54 * 96;
        public static double BlockHeight = 13.3 / 2.54 * 96;

        public static double MarginLeft = 0.5 / 2.54 * 96;
        public static double MarginTop = 2 / 2.54 * 96;

        public static FontFamily MainFont = new FontFamily(ChineseFont);

        public static double MainFontSize = 16;// * 96 / 72;
        public static double SideFontSize = 12;// * 96 / 72;

        public static FontFamily ArialFont = new FontFamily(EnglishFont);
        public static double PageNoFont = 12 * 96 / 72;
        public static double PageNoLeft = 18 / 2.54 * 96;
        public static double PageNoTop = 1.4 / 2.54 * 96;

        public static double PageNameLeft = 17 / 2.54 * 96;
        public static double PageNameTop = 28.5 / 2.54 * 96;

        public static string ChineseFont
        {
            get
            {
                string cfont = ConfigurationManager.AppSettings["ChineseFont"];
                return String.IsNullOrEmpty(cfont) ? DefaultChineseFont : cfont;
            }
        }

        public static string EnglishFont
        {
            get
            {
                string cfont = ConfigurationManager.AppSettings["EnglishFont"];
                return String.IsNullOrEmpty(cfont) ? DefaultEnglishFont : cfont;
            }
        }

    }
}
