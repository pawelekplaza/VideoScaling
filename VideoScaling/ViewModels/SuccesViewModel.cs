using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
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

            Exit = new RelayCommand(() =>
            {
                Environment.Exit(0);
            });
        }

        public RelayCommand StartAgain { get; set; }
        public RelayCommand Exit { get; set; }
    }
}
