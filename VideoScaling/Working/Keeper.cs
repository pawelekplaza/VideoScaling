using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoScaling.ViewModels;
using VideoScaling.Views;

namespace VideoScaling.Working
{
    public class Keeper
    {
        public MainView KeepMainPage { get; set; }
        public MainViewModel KeepMainContext { get; set; }
        public SecondView KeepSecondPage { get; set; }
        public SecondViewModel KeepSecondContext { get; set; }
    }
}
