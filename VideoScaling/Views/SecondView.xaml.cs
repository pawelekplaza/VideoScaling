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

            context.ChangeWindowSizeEvent += ChangeWindowSize;
            context.RectangleMouseDownEvent += RectangleCanvasSetStartPoint;
            context.RectangleMouseMoveEvent += RectangleCanvasMouseMove;
            context.ShowMainPageEvent += ShowMainPage;
            context.ShowWaitingPageEvent += ShowWaitingPage;
            context.DeleteRectangleSelectionEvent += DeleteSelection;
        }

        public void UtilizeState(object state)
        {
            //var tmp = state as Keeper;
            //context.MainContext = tmp.KeepMainContext;
            //context.MainPage = tmp.KeepMainPage;

            var tmp = state as MainView;
            context.MainPage = tmp;
            context.BaseSelection = tmp.GetSelection();

            tmp.Visibility = Visibility.Hidden;
            this.Visibility = Visibility.Visible; 
        }

        private void ChangeWindowSize(object sender, MyArguments e)
        {
            var x = Parent as Window;
            x.Height = e.WindowHeight + FirstRow.Height.Value + 63;
            x.Width = e.WindowWidth + 61;

            if (x.Height < 300)
                x.Height = 300;
            if (x.Width < 550)
                x.Width = 550;

            if (x.Height > 1080)
                x.Height = 1080;
            if (x.Width > 1980)
                x.Width = 1980;

            Height = double.NaN;
            Width = double.NaN;
        }
        private void ShowMainPage(object sender, MyArguments e)
        {
            //Switcher.Switch(e.MainPage, new Keeper { KeepSecondContext = sender as SecondViewModel, KeepSecondPage = this });            
            Switcher.Switch(e.MainPage, this);
        }
        private void ShowWaitingPage(object sender, MyArguments e)
        {
            Switcher.Switch(new WaitingView(), new MyArguments { VidInfo = e.VidInfo, SecondPage = this });            
        }

        private void SelectionRectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            context.SelectionMouseDown(e, e.GetPosition(Frame));
        }
        private void RectangleCanvasSetStartPoint(object sender, MyArguments e)
        {
            Canvas.SetLeft(sender as Rectangle, e.StartPoint.X);
            Canvas.SetTop(sender as Rectangle, e.StartPoint.Y);
            if (RectangleCanvas.Children.Count > 1)
                RectangleCanvas.Children.RemoveAt(1);
            RectangleCanvas.Children.Add(sender as Rectangle);
        }


        private void SelectionRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            context.SelectionMouseMove(sender, e, e.GetPosition(Frame));
        }
        private void RectangleCanvasMouseMove(object sender, MyArguments e)
        {
            Canvas.SetLeft(sender as Rectangle, e.RectangleX);
            Canvas.SetTop(sender as Rectangle, e.RectangleY);
        }
        private void DeleteSelection(object sender, MyArguments e)
        {
            if (RectangleCanvas.Children.Count > 1)
                RectangleCanvas.Children.RemoveAt(1);
        }
    }
}
