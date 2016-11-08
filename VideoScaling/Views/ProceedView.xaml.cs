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
using VideoScaling.ViewModels;
using VideoScaling.Working;

namespace VideoScaling.Views
{
    /// <summary>
    /// Interaction logic for ProceedView.xaml
    /// </summary>
    public partial class ProceedView : UserControl, ISwitchable
    {
        private ProceedViewModel context;
        public ProceedView()
        {
            InitializeComponent();
            context = new ProceedViewModel();
            DataContext = context;

            Height = double.NaN;
            Width = double.NaN;
        }

        public void UtilizeState(object state)
        {
            context.VidInfo = state as VideoInfo;
            context.ProgressBarMax = context.VidInfo.VideoReader.FrameCount;
            context.ProgressBarValue = 0;
        }
    }
}
