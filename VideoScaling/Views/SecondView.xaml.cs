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
using VideoScaling.Events;
using VideoScaling.Models;
using VideoScaling.ViewModels;
using VideoScaling.Working;

namespace VideoScaling.Views
{
    /// <summary>
    /// Interaction logic for SecondView.xaml
    /// </summary>
    public partial class SecondView : UserControl, ISwitchable
    {
        private SecondViewModel context;        
        public SecondView()
        {            
            InitializeComponent();
            context = new SecondViewModel();
            DataContext = context;

            Height = double.NaN;
            Width = double.NaN;

            context.ShowMainPageEvent += ShowMainPage;
        }

        public void UtilizeState(object state)
        {
            //var tmp = state as Keeper;
            //context.MainContext = tmp.KeepMainContext;
            //context.MainPage = tmp.KeepMainPage;

            var tmp = state as MainView;
            context.MainPage = tmp;

            tmp.Visibility = Visibility.Hidden;
            this.Visibility = Visibility.Visible; 
        }

        private void ChangeWindowSize(object sender, MyArguments e)
        {
            var x = Parent as Window;
            x.Height = e.WindowHeight + FirstRow.Height.Value + 63;
            x.Width = e.WindowWidth + 61;

            Height = double.NaN;
            Width = double.NaN;
        }
        private void ShowMainPage(object sender, MyArguments e)
        {
            //Switcher.Switch(e.MainPage, new Keeper { KeepSecondContext = sender as SecondViewModel, KeepSecondPage = this });            
            Switcher.Switch(e.MainPage, this);
        }
    }
}
