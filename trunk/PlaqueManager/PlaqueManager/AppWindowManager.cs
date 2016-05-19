using System.ComponentModel.Composition;
using Caliburn.Metro.Core;
using Caliburn.Micro;
using MahApps.Metro.Controls;

namespace PlaqueManager
{
    [Export(typeof(IWindowManager))]
    public class AppWindowManager : MetroWindowManager
    {
        public override MetroWindow CreateCustomWindow(object view, bool windowIsView)
        {
            if (windowIsView)
            {
                return view as MainWindow;
            }

            return new MainWindow
            {
                Content = view
            };
        }
    }
}
