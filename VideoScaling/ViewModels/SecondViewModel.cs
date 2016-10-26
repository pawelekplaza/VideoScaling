using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoScaling.Models;
using VideoScaling.Views;

namespace VideoScaling.ViewModels
{
    public class SecondViewModel : ViewModelBase
    {
        protected SecondModel Model;

        public SecondViewModel()
        {
            Model = new SecondModel();
        }

        public System.Windows.Shapes.Rectangle BaseSelection
        {
            get { return Model.BaseSelectionRectangle; }
            set { Model.BaseSelectionRectangle = value; }
        }

        public MainView MainView
        {
            get { return Model.MainView; }
            set { Model.MainView = value; }
        }
    }
}
