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
                        ImageSource = Model.ImageSourceList[--FrameIndex].bitmapImage;
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
                    if (FrameIndex == Model.ImageSourceList.Count - 1)
                        ImageSource = ReadNextFrame();
                    else
                        ImageSource = Model.ImageSourceList[++FrameIndex].bitmapImage;

                    if (FrameIndex == Model.VideoReader.FrameCount - 1)
                        NextFrameIsEnabled = false;
                }
                catch (Exception)
                {
                    NextFrameIsEnabled = false;
                }
            });
            PreviousVideo = new RelayCommand(() =>
            {
                Switcher.Switch(Model.MainView, this);
            });
        }

        public System.Windows.Shapes.Rectangle BaseSelection;



        public RelayCommand BrowseFile { get; set; }
        public RelayCommand SelectPreviousFrame { get; set; }
        public RelayCommand SelectNextFrame { get; set; }
        public RelayCommand PreviousVideo { get; set; }

        public string FilePathTextBox
        {
            get { return Model.FilePath; }
            set { Model.FilePath = value; RaisePropertyChanged("FilePathTextBox"); }
        }
        public BitmapImage ImageSource
        {
            get { return Model.ImageSource; }
            set { Model.ImageSource = value; RaisePropertyChanged("ImageSource"); }
        }
        public string CurrentFrameTextBlock
        {
            get { return "Current frame: " + FrameIndex.ToString(); }
        }
        public int FrameIndex
        {
            get { return Model.ImageSourceListIndex; }
            set
            {
                Model.ImageSourceListIndex = value;
                if (Model.ImageSourceListIndex > 0)
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
        public BitmapImage ReadNextFrame()
        {
            string framePath = Utils.Directories.TmpPath + "\\firstFrame_" + Utils.Time.GetTime() + ".bmp";
            var firstFrame = Model.VideoReader.ReadVideoFrame();
            firstFrame.Save(framePath);
            var result = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, framePath)));
            Model.ImageSourceList.Add(new SingleFrame { bitmap = firstFrame, bitmapImage = result });
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
                Model.ImageSourceList.Clear();
                FrameIndex = 0;
                RaisePropertyChanged("CurrentFrameTextBlock");
                Model.VideoReader.Close();

                Model.VideoReader.Open(FilePathTextBox);
                var firstFrame = Model.VideoReader.ReadVideoFrame();
                firstFrame.Save(framePath);

                ChangeWindowSizeEvent?.Invoke(this, new MyArguments { WindowHeight = firstFrame.Height, WindowWidth = firstFrame.Width });
                ImageSource = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, framePath)));
                Model.ImageSourceList.Add(new SingleFrame { bitmap = firstFrame, bitmapImage = ImageSource });
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
