using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using VideoScaling.Events;
using VideoScaling.Models;
using VideoScaling.Views;

namespace VideoScaling.ViewModels
{
    public class SecondViewModel : ViewModelBase
    {
        protected SecondModel Model;

        public event EventHandler<MyArguments> ChangeWindowSizeEvent;
        public event EventHandler<MyArguments> DeleteRectangleSelectionEvent;
        public event EventHandler<MyArguments> EnableProceedWindowEvent;
        public event EventHandler<MyArguments> DisableProceedWindowEvent;
        public event EventHandler<MyArguments> ShowMainPageEvent;
        public event EventHandler<MyArguments> ShowWaitingPageEvent;

        public SecondViewModel()
        {
            Model = new SecondModel();
            PreviousFrameIsEnabled = false;
            NextFrameIsEnabled = false;            

            BrowseFile = new RelayCommand(() =>
            {
                try { BrowseFileTryContent(); }
                catch (Exception ex) { BrowseFileCatchContent(ex); }
            });
            SelectPreviousFrame = new RelayCommand(() =>
            {
                try
                {
                    NextFrameIsEnabled = true;
                    if (FrameIndex > 0)
                    {
                        ImageSource = Model.Vid.ImageSourceList[--FrameIndex].bitmapImage;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
            SelectNextFrame = new RelayCommand(() =>
            {
                try
                {
                    if (FrameIndex == Model.Vid.ImageSourceList.Count - 1)
                        ImageSource = ReadNextFrame();
                    else
                        ImageSource = Model.Vid.ImageSourceList[++FrameIndex].bitmapImage;

                    if (FrameIndex == Model.Vid.VideoReader.FrameCount - 1)
                        NextFrameIsEnabled = false;
                }
                catch (Exception)
                {
                    NextFrameIsEnabled = false;
                }
            });
            PreviousVideo = new RelayCommand(() =>
            {
                ShowMainPageEvent?.Invoke(this, new MyArguments { MainPage = this.MainPage });
            });
            OpenProceedWindow = new RelayCommand(() =>
            {
                Model.Vid.ScaleHeight = BaseSelection.Height / Model.SelectionRectangle.Height;
                Model.Vid.ScaleWidth = BaseSelection.Width / Model.SelectionRectangle.Width;
                ShowWaitingPageEvent?.Invoke(this, new MyArguments { VidInfo = Model.Vid, MainPage = this.MainPage });
            });
        }              

        public MainView MainPage
        {
            get { return Model.MainPage; }
            set { Model.MainPage = value; }

        }
        public MainViewModel MainContext
        {
            get { return Model.MainContext; }
            set { Model.MainContext = value; }
        }

        public RelayCommand BrowseFile { get; set; }
        public RelayCommand SelectPreviousFrame { get; set; }
        public RelayCommand SelectNextFrame { get; set; }
        public RelayCommand PreviousVideo { get; set; }
        public RelayCommand OpenProceedWindow { get; set; }


        public System.Windows.Shapes.Rectangle BaseSelection
        {
            get { return Model.BaseSelectionRectangle; }
            set { Model.BaseSelectionRectangle = value; }
        }
        public string FilePathTextBox
        {
            get { return Model.Vid.FilePath; }
            set { Model.Vid.FilePath = value; RaisePropertyChanged("FilePathTextBox"); }
        }
        public BitmapImage ImageSource
        {
            get { return Model.Vid.ImageSource; }
            set { Model.Vid.ImageSource = value; RaisePropertyChanged("ImageSource"); }
        }
        public string CurrentFrameTextBlock
        {
            get { return "Current frame: " + FrameIndex.ToString(); }
        }
        public int FrameIndex
        {
            get { return Model.Vid.ImageSourceListIndex; }
            set
            {
                Model.Vid.ImageSourceListIndex = value;
                if (Model.Vid.ImageSourceListIndex > 0)
                    PreviousFrameIsEnabled = true;
                else
                    PreviousFrameIsEnabled = false;
                RaisePropertyChanged("CurrentFrameTextBlock");
            }
        }
        protected bool previousFrameIsEnabled;
        public bool PreviousFrameIsEnabled
        {
            get { return previousFrameIsEnabled; }
            set { previousFrameIsEnabled = value; RaisePropertyChanged("PreviousFrameIsEnabled"); }
        }

        protected bool nextFrameIsEnabled;
        public bool NextFrameIsEnabled
        {
            get { return nextFrameIsEnabled; }
            set { nextFrameIsEnabled = value; RaisePropertyChanged("NextFrameIsEnabled"); }
        }

        private bool proceedIsEnabled;
        public bool ProceedIsEnabled
        {
            get { return proceedIsEnabled; }
            set { proceedIsEnabled = value; RaisePropertyChanged("ProceedIsEnabled"); }
        }


        public BitmapImage ReadNextFrame()
        {
            string framePath = Utils.Directories.TmpPath + "\\firstFrame_" + Utils.Time.GetTime() + ".bmp";
            var firstFrame = Model.Vid.VideoReader.ReadVideoFrame();
            firstFrame.Save(framePath);
            var result = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, framePath)));
            Model.Vid.ImageSourceList.Add(new SingleFrame { bitmap = firstFrame, bitmapImage = result });
            FrameIndex++;
            RaisePropertyChanged("CurrentFrameTextBlock");

            return result;
        }

        public event EventHandler<MyArguments> RectangleMouseDownEvent;
        public void SelectionMouseDown(MouseButtonEventArgs e, Point startPoint)
        {
            Model.SelectionStartPoint = startPoint;
            Model.SelectionRectangle = new System.Windows.Shapes.Rectangle
            {
                Stroke = System.Windows.Media.Brushes.Yellow,
                StrokeThickness = 3
            };

            ProceedIsEnabled = true;
            RectangleMouseDownEvent?.Invoke(Model.SelectionRectangle, new MyArguments { StartPoint = startPoint });            
            EnableProceedWindowEvent?.Invoke(Model.SelectionRectangle, new MyArguments());
        }

        public event EventHandler<MyArguments> RectangleMouseMoveEvent;
        public void SelectionMouseMove(object sender, MouseEventArgs e, Point position)
        {
            if (e.LeftButton == MouseButtonState.Released || Model.SelectionRectangle == null)
                return;

            var x = Math.Min(position.X, Model.SelectionStartPoint.X);
            var y = Math.Min(position.Y, Model.SelectionStartPoint.Y);

            var w = Math.Max(position.X, Model.SelectionStartPoint.X) - x;
            var h = Math.Max(position.Y, Model.SelectionStartPoint.Y) - y;

            Model.SelectionRectangle.Width = w;
            Model.SelectionRectangle.Height = h;

            RectangleMouseMoveEvent?.Invoke(Model.SelectionRectangle, new MyArguments { RectangleX = x, RectangleY = y });
        }

        public void ExitWindow(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        protected void BrowseFileTryContent()
        {
            bool? selected;
            FilePathTextBox = Utils.Directories.BrowseFile(FilePathTextBox, out selected);
            string framePath = Utils.Directories.TmpPath + "\\firstFrame_" + Utils.Time.GetTime() + ".bmp";
            if ((bool)selected)
            {
                Model.Vid.ImageSourceList.Clear();
                FrameIndex = 0;
                RaisePropertyChanged("CurrentFrameTextBlock");
                Model.Vid.VideoReader.Close();

                Model.Vid.VideoReader.Open(FilePathTextBox);
                var firstFrame = Model.Vid.VideoReader.ReadVideoFrame();
                firstFrame.Save(framePath);

                ChangeWindowSizeEvent?.Invoke(this, new MyArguments { WindowHeight = firstFrame.Height, WindowWidth = firstFrame.Width });
                ImageSource = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, framePath)));
                Model.Vid.ImageSourceList.Add(new SingleFrame { bitmap = firstFrame, bitmapImage = ImageSource });
                NextFrameIsEnabled = true;
            }
        }
        protected void BrowseFileCatchContent(Exception ex)
        {
            MessageBox.Show(ex.Message);
            ImageSource = null;
            PreviousFrameIsEnabled = false;
            NextFrameIsEnabled = false;            
            DeleteRectangleSelectionEvent?.Invoke(this, new MyArguments());
            DisableProceedWindowEvent?.Invoke(null, new MyArguments());
        }
    }
}
