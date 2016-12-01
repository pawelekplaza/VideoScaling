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
using VideoScaling.ViewModels;

namespace VideoScaling.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl, ISwitchable
    {
        private MainViewModel context;
        public MainView()
        {
            InitializeComponent();
            context = new MainViewModel();
            DataContext = context;

            Height = double.NaN;
            Width = double.NaN;

            context.ChangeWindowSizeEvent += ChangeWindowSize;
            context.RectangleMouseDownEvent += RectangleCanvasSetStartPoint;
            context.RectangleMouseMoveEvent += RectangleCanvasMouseMove;
            context.ShowSecondPageEvent += ShowSecondPage;
            context.DeleteRectangleSelectionEvent += DeleteSelection;                    
        }

        public void UtilizeState(object state)
        {
            //var tmp = state as Keeper;
            //context.SecondContext = tmp.KeepSecondContext;
            //context.SecondPage = tmp.KeepSecondPage;
            var tmp = state as SecondView;
            context.SecondPage = tmp;

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

            if (x.Height > 600)
                x.Height = 600;
            if (x.Width > 1024)
                x.Width = 1024;

            Height = double.NaN;
            Width = double.NaN;                        
        }

        private void SelectionRectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(Frame);
            context.SelectionMouseDown(e, new System.Drawing.Point((int)p.X, (int)p.Y));
        }    
        private void RectangleCanvasSetStartPoint(object sender, MyArguments e)
        {
            Models.Selection arg = sender as Models.Selection;
            Rectangle rect = arg.Rect;
            Canvas.SetLeft(rect, e.StartPoint.X);
            Canvas.SetTop(rect, e.StartPoint.Y);
            if (RectangleCanvas.Children.Count > 1)
                RectangleCanvas.Children.RemoveAt(1);
            RectangleCanvas.Children.Add(rect);
        }


        private void SelectionRectangle_MouseMove(object sender, MouseEventArgs e)
        {            
            context.SelectionMouseMove(sender, e, e.GetPosition(Frame));
        }
        private void RectangleCanvasMouseMove(object sender, MyArguments e)
        {
            Models.Selection arg = sender as Models.Selection;
            Rectangle rect = arg.Rect;
            Canvas.SetLeft(rect, e.RectangleX);
            Canvas.SetTop(rect, e.RectangleY);
        }
        private void ShowSecondPage(object sender, MyArguments e)
        {
            if (e.SecondPage != null)
            {                
                Switcher.Switch(e.SecondPage, this);
            }
            else
                //Switcher.Switch(new SecondView(), new Keeper { KeepMainContext = sender as MainViewModel, KeepMainPage = this });
                Switcher.Switch(new SecondView(), this);
        }
        public Models.Selection GetSelection()
        {
            return context.Selection;
        }
        private void DeleteSelection(object sender, MyArguments e)
        {
            if (RectangleCanvas.Children.Count > 1)
                RectangleCanvas.Children.RemoveAt(1);
        }
    }
}
