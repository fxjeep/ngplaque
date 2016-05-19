using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlaqueManager.Views
{
    /// <summary>
    /// Interaction logic for LiveDetailView.xaml
    /// </summary>
    public partial class AncestorDetailView : UserControl
    {
        public AncestorDetailView()
        {
            InitializeComponent();
        }

        private void AncestorGrid_OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.Item == CollectionView.NewItemPlaceholder)
            {
                e.Row.Background = Brushes.Beige as Brush;
            }
        }

    }
}
