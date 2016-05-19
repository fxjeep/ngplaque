using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace PlaqueManager.ViewModels
{
    public class ProgressDialogViewModel : Screen
    {
        public string Title { get; set; }
        public string SubText { get; set; }
        public int CurrentValue { get; set; }

        public ProgressDialogViewModel(string title)
        {
            Title = title;
        }

        public void SetSubText(string text)
        {
            SubText = text;
            NotifyOfPropertyChange(nameof(SubText));
        }

        public void UpdateProgress(string text, int count)
        {
            CurrentValue = count;
            SetSubText(text);
            NotifyOfPropertyChange(nameof(CurrentValue));
        }

    }
}
