using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using VideoScaling.Events;
using VideoScaling.ViewModels;
using VideoScaling.Working;

namespace VideoScaling.Views
{
    /// <summary>
    /// Interaction logic for WaitingView.xaml
    /// </summary>
    public partial class WaitingView : UserControl, ISwitchable
    {
        private WaitingViewModel context;
        public WaitingView()
        {
            InitializeComponent();
            context = new WaitingViewModel();
            DataContext = context;

            Height = double.NaN;
            Width = double.NaN;

            context.ShowProceedPageEvent += ShowProceedPage;                
        }

        public void UtilizeState(object state)
        {
            var tmp = state as MyArguments;            
            context.VidInfo = tmp.VidInfo;
            context.SecondPage = tmp.SecondPage;
            context.ProgressBarMaximum = context.VidInfo.VideoReader.FrameCount;
            tmp.MainPage = null;

            LoadVideo();
        }

        private void ShowProceedPage(object sender, MyArguments e)
        {
            Switcher.Switch(new SecondView(), e.VidInfo);
        }

        private async void LoadVideo()
        {
            await Task.Factory.StartNew(() => context.LoadVideo());            
            Switcher.Switch(new ProceedView(), context.VidInfo);
        }
    }
}
