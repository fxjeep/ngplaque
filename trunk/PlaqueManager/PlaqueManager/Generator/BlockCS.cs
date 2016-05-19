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
    public class BlockCS : UserControl
    {
        public string LiveName { get; set; }

        public NameSegement MainName { get; set; }

        public Canvas Root { get; set; }

        public System.Windows.Controls.Image ImageBk { get; set; }

        public System.Windows.Controls.StackPanel MainNameStack { get; set; }

        public BlockCS(string name)
        {
            this.LiveName = name;
            MainName = new NameSegement(name);

            Root = new Canvas();
            this.AddChild(Root);

            //insert image
            ImageBk = new System.Windows.Controls.Image();
            ImageBk.Source = new BitmapImage(new Uri(@"pack://application:,,,/Views/Images/changsheng.jpg"));
            //background.Stretch = Stretch.Fill;

            Root.Children.Add(ImageBk);

            Canvas.SetLeft(ImageBk, 0);
            Canvas.SetTop(ImageBk, 0);
            
            //set border

            this.BorderThickness = new System.Windows.Thickness(1);
            this.BorderBrush = new SolidColorBrush(Colors.Black);

            //print textbox
            MainNameStack = new StackPanel();
            Canvas.SetLeft(MainNameStack, 1 / 2.54 * 96);
            Canvas.SetTop(MainNameStack, 6.3 / 2.54 * 96);
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

        protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
        {
            this.Root.Width = this.MaxWidth-2;
            this.Root.Height = this.MaxHeight-2;

            this.ImageBk.Width = this.MaxWidth - 2;
            this.ImageBk.Height = this.MaxHeight - 2;
            
            return new System.Windows.Size(this.MaxWidth, this.MaxHeight);
        }
    }
}
