using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoScaling.Views;

namespace VideoScaling.ViewModels
{
    public class SuccesViewModel : ViewModelBase
    {
        public SuccesViewModel()
        {
            StartAgain = new RelayCommand(() =>
            {
                Switcher.Switch(new MainView());
            });
        }

        public RelayCommand StartAgain { get; set; }
    }
}
