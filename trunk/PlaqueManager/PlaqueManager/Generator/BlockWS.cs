using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PlaqueManager.Generator
{
    public class BlockWS : UserControl
    {
        public NameSegement MainName { get; set; }
        public NameSegement SideName { get; set; }

        public Canvas Root { get; set; }

        public System.Windows.Controls.Image ImageBk { get; set; }

        public System.Windows.Controls.StackPanel MainNameStack { get; set; }
        public System.Windows.Controls.StackPanel SideNameStack { get; set; }

        public BlockWS(string deadname, string sidename)
        {
            MainName = new NameSegement(deadname);
            SideName = new NameSegement(sidename);

            Root = new Canvas();
            this.AddChild(Root);

            //insert image
            ImageBk = new System.Windows.Controls.Image();
            ImageBk.Source = new BitmapImage(new Uri(@"pack://application:,,,/Views/Images/wangshen.jpg"));

            Root.Children.Add(ImageBk);

            Canvas.SetLeft(ImageBk, 0);
            Canvas.SetTop(ImageBk, 0);

            //set border

            this.BorderThickness = new System.Windows.Thickness(1);
            this.BorderBrush = new SolidColorBrush(Colors.Black);

            //print maintext
            this.PrintMainText();

            this.PrintSideText();
        }

        public void PrintMainText()
        {
            MainNameStack = new StackPanel();
            Canvas.SetLeft(MainNameStack, 1.1 / 2.54 * 96);
            Canvas.SetTop(MainNameStack, 5.7 / 2.54 * 96);
            MainNameStack.Width = 2 / 2.54 * 96;
            MainNameStack.Height = 3.3 / 2.54 * 96;
            MainNameStack.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

            Root.Children.Add(MainNameStack);

            foreach (TextSegement seg in MainName.NameSegements)
            {
                TextBlock tb = new TextBlock();
                tb.FontFamily = Configure.MainFont;
                tb.FontSize = Configure.MainFontSize;
                tb.TextAlignment = System.Windows.TextAlignment.Center;
                tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                tb.Margin = new System.Windows.Thickness(0);
                tb.Padding = new System.Windows.Thickness(0);
                tb.Text = seg.Text;
                //tb.BorderBrush = new SolidColorBrush(Colors.Black);
                //tb.BorderThickness = new System.Windows.Thickness(1);
                MainNameStack.Children.Add(tb);
            }          
        }

        public void PrintSideText()
        {
            //print textbox
            if (!SideName.HasEnglish)
            {
                SideNameStack = new StackPanel();
                Canvas.SetLeft(SideNameStack, 0.1 / 2.54 * 96);
                Canvas.SetTop(SideNameStack, 4.8 / 2.54 * 96);
                SideNameStack.Width = 0.6 / 2.54 * 96;
                SideNameStack.Height = 4.5 / 2.54 * 96;
                SideNameStack.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

                Root.Children.Add(SideNameStack);

                foreach (TextSegement seg in SideName.NameSegements)
                {
                    TextBlock tb = new TextBlock();
                    tb.FontFamily = Configure.MainFont;
                    tb.FontSize = Configure.SideFontSize;
                    tb.TextAlignment = System.Windows.TextAlignment.Center;
                    tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    tb.Margin = new System.Windows.Thickness(0);
                    tb.Padding = new System.Windows.Thickness(0);
                    tb.Text = seg.Text;
                    SideNameStack.Children.Add(tb);
                }
            }
            else
            {
                //mixed with english and chinese. we should output a wrap
                string text = SideName.SideString();
                TextBlock tb = new TextBlock();
                tb.FontFamily = Configure.MainFont;
                tb.FontSize = Configure.SideFontSize;
                tb.TextAlignment = System.Windows.TextAlignment.Left;
                tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                tb.Margin = new System.Windows.Thickness(0);
                tb.Padding = new System.Windows.Thickness(0);
                tb.Text = text;
                Canvas.SetLeft(tb, 0.2 / 2.54 * 96);
                Canvas.SetTop(tb, 4.8 / 2.54 * 96);
                tb.Height = 0.6 / 2.54 * 96;
                tb.Width = 4.5 / 2.54 * 96;

                RotateTransform tran = new RotateTransform(90);
                tb.LayoutTransform = tran;
                Root.Children.Add(tb);
            }
        }

        protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
        {
            this.Root.Width = this.MaxWidth - 2;
            this.Root.Height = this.MaxHeight - 2;

            this.ImageBk.Width = this.MaxWidth - 2;
            this.ImageBk.Height = this.MaxHeight - 2;

            return new System.Windows.Size(this.MaxWidth, this.MaxHeight);
        }
    }
}
