﻿using System;
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
            var tmp = state as MainView;
            context.MainPage = tmp;
            context.BaseSelection = tmp.GetSelection();

            tmp.Visibility = Visibility.Hidden;
            this.Visibility = Visibility.Visible;
        }

        private System.Drawing.Size ChangeWindowSize(MyArguments e)
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

            return new System.Drawing.Size((int)RectangleCanvas.ActualWidth, (int)RectangleCanvas.ActualHeight);
        }
        private void ShowMainPage(object sender, MyArguments e)
        {                   
            Switcher.Switch(e.MainPage, this);
        }
        private void ShowWaitingPage(object sender, MyArguments e)
        {
            Switcher.Switch(new WaitingView(), new MyArguments { VidInfo = e.VidInfo, SecondPage = this });            
        }

        private void SelectionRectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point P = e.GetPosition(Frame);
            context.SelectionMouseDown(e, new System.Drawing.Point((int)P.X, (int)P.Y));
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
        private void DeleteSelection(object sender, MyArguments e)
        {
            if (RectangleCanvas.Children.Count > 1)
                RectangleCanvas.Children.RemoveAt(1);
        }
    }
}
