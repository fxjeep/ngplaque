using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using PlaqueManager.Generator;

namespace PlaqueManager.Generator
{
    public class PrintHelper
    {
        public static BlockCS PrintCSBlock(string name, FixedPage fp, int pos)
        {
            int row = (int)Math.Floor((double)(pos / 5));
            int col = (int)pos % 5;
            BlockCS block = new BlockCS(name);
            block.MaxWidth = Configure.BlockWidth;
            block.MaxHeight = Configure.BlockHeight;
            block.Margin = new Thickness(0);
            block.Padding = new Thickness(0);
            FixedPage.SetLeft(block, Configure.MarginLeft + col * Configure.BlockWidth);
            FixedPage.SetTop(block, Configure.MarginTop + row * Configure.BlockHeight);
            return block;
        }

        public static BlockYQ PrintYQBlock(string name, FixedPage fp, int pos)
        {
            int row = (int)Math.Floor((double)(pos / 5));
            int col = (int)pos % 5;
            BlockYQ block = new BlockYQ(name);
            block.MaxWidth = Configure.BlockWidth;
            block.MaxHeight = Configure.BlockHeight;
            block.Margin = new Thickness(0);
            block.Padding = new Thickness(0);
            FixedPage.SetLeft(block, Configure.MarginLeft + col * Configure.BlockWidth);
            FixedPage.SetTop(block, Configure.MarginTop + row * Configure.BlockHeight);
            return block;
        }

        public static BlockWS PrintWSBlock(string name, string livename, FixedPage fp, int pos)
        {
            int row = (int)Math.Floor((double)(pos / 5));
            int col = (int)pos % 5;
            BlockWS block = new BlockWS(name, livename);
            block.MaxWidth = Configure.BlockWidth;
            block.MaxHeight = Configure.BlockHeight;
            block.Margin = new Thickness(0);
            block.Padding = new Thickness(0);
            FixedPage.SetLeft(block, Configure.MarginLeft + col * Configure.BlockWidth);
            FixedPage.SetTop(block, Configure.MarginTop + row * Configure.BlockHeight);
            return block;
        }

        public static BlockZX PrintZXBlock(string surname, string livename, FixedPage fp, int pos)
        {
            int row = (int)Math.Floor((double)(pos / 5));
            int col = (int)pos % 5;
            BlockZX block = new BlockZX(surname, livename);
            block.MaxWidth = Configure.BlockWidth;
            block.MaxHeight = Configure.BlockHeight;
            block.Margin = new Thickness(0);
            block.Padding = new Thickness(0);
            FixedPage.SetLeft(block, Configure.MarginLeft + col * Configure.BlockWidth);
            FixedPage.SetTop(block, Configure.MarginTop + row * Configure.BlockHeight);
            return block;
        }

        public static void PrintPageNoPageName(FixedPage fp, int p, string name)
        {
            //print page number and file name 
            Label pageno = new Label();
            pageno.FontFamily = Configure.ArialFont;
            pageno.FontSize = Configure.PageNoFont;
            pageno.Content = "No." + p.ToString("000000");
            FixedPage.SetLeft(pageno, Configure.PageNoLeft);
            FixedPage.SetTop(pageno, Configure.PageNoTop);
            fp.Children.Add(pageno);

            Label pagename = new Label();
            pagename.FontFamily = Configure.ArialFont;
            pagename.FontSize = Configure.PageNoFont;
            pagename.Content = name;
            FixedPage.SetLeft(pagename, Configure.PageNameLeft);
            FixedPage.SetTop(pagename, Configure.PageNameTop);
            fp.Children.Add(pagename);
        }
    }
}
