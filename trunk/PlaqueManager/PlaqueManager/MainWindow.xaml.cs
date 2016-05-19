using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using Infralution.Localization.Wpf;
using PlaqueData;

namespace PlaqueManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public string DataFolder {get;set;}

        public MainWindow()
        {
            InitializeComponent();

            Loaded += (sender, args) =>
            {
                SetCulture(PlaqueConfig.LanguageState);
                CultureManager.UICulture = Thread.CurrentThread.CurrentCulture;
                CultureManager.UICultureChanged += HandleUICultureChanged;
            };

            Closed += (sender, args) =>
            {
                PlaqueConfig.SaveAllConfig();
            };
        }

        void HandleUICultureChanged(object sender, EventArgs args)
        {
        }

        private void LangButton_OnClick(object sender, RoutedEventArgs e)
        {
            PlaqueConfig.LanguageState = (PlaqueConfig.LanguageState == PlaqueData.Language.Ch) ? PlaqueData.Language.En : PlaqueData.Language.Ch;

            SetCulture(PlaqueConfig.LanguageState);
        }

        public void SetCulture(Language state)
        {
            if (state == PlaqueData.Language.En)
            {
                LangText.Text = "En";
                var culture = new CultureInfo("en-AU");
                CultureManager.UICulture = culture;
            }
            else if (state == PlaqueData.Language.Ch)
            {
                LangText.Text = "中";
                var culture = new CultureInfo("zh-CN");
                CultureManager.UICulture = culture;
            }
        }
    }

    
}
