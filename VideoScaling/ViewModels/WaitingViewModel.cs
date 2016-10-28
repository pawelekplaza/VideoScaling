using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoScaling.Models;

namespace VideoScaling.ViewModels
{
    public class WaitingViewModel : ViewModelBase
    {
        private WaitingModel Model;

        public WaitingViewModel()
        {
            Model = new WaitingModel();
        }
    }
}
