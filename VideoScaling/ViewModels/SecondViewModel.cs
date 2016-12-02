using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using VideoScaling.Events;
using VideoScaling.Models;
using VideoScaling.Utils;
using VideoScaling.Views;

namespace VideoScaling.ViewModels
{
    public class SecondViewModel : ViewModelBase
    {
        protected SecondModel Model;

        public event ResizeImageDelegate ChangeWindowSizeEvent;
        public event EventHandler<MyArguments> DeleteRectangleSelectionEvent;
        public event EventHandler<MyArguments> EnableProceedWindowEvent;        
        public event EventHandler<MyArguments> ShowMainPageEvent;
        public event EventHandler<MyArguments> ShowWaitingPageEvent;

        public delegate System.Drawing.Size ResizeImageDelegate(MyArguments e);

        public SecondViewModel()
        {
            Model = new SecondModel();
            Model.SelectionRectangle = new Models.Selection();
            PreviousFrameIsEnabled = false;
            NextFrameIsEnabled = false;            

            BrowseFile = new RelayCommand(() =>
            {
                try { BrowseFileTryContent(); }
                catch (Exception ex)
                {
                    Logger.Log(string.Concat(ex.Message, "\r\n", ex.StackTrace));
                    BrowseFileCatchContent(ex);
                }
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
                    Logger.Log(string.Concat(ex.Message, "\r\n", ex.StackTrace));
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
                try
                {
                    ShowMainPageEvent?.Invoke(this, new MyArguments { MainPage = this.MainPage });
                }
                catch (Exception ex)
                {
                    Logger.Log(string.Concat(ex.Message, "\r\n", ex.StackTrace));
                    MessageBox.Show(ex.Message);
                }
            });
            OpenProceedWindow = new RelayCommand(() =>
            {
                try
                {
                    Model.Vid.ScaleHeight = BaseSelection.Rect.Height / Model.SelectionRectangle.Rect.Height;
                    Model.Vid.ScaleWidth = BaseSelection.Rect.Width / Model.SelectionRectangle.Rect.Width;
                    Model.Vid.BaseCenter = BaseSelection.GetCenter();
                    Model.Vid.SecondCenter = Model.SelectionRectangle.GetCenter();
                    Model.Vid.SecondSelection = Model.SelectionRectangle;                  
                    ShowWaitingPageEvent?.Invoke(this, new MyArguments { VidInfo = Model.Vid, MainPage = this.MainPage });
                }
                catch (Exception ex)
                {
                    Logger.Log(string.Concat(ex.Message, "\r\n", ex.StackTrace));
                    MessageBox.Show(ex.Message);
                }
            });
        }              

        public MainView MainPage
        {
            get { return Model.MainPage; }
            set { Model.MainPage = value; }

        }

        public RelayCommand BrowseFile { get; set; }
        public RelayCommand SelectPreviousFrame { get; set; }
        public RelayCommand SelectNextFrame { get; set; }
        public RelayCommand PreviousVideo { get; set; }
        public RelayCommand OpenProceedWindow { get; set; }


        public Models.Selection BaseSelection
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
            try
            {
                string framePath = Utils.Directories.TmpPath + "\\firstFrame_" + Utils.Time.GetTime() + ".bmp";
                var firstFrame = Model.Vid.VideoReader.ReadVideoFrame();
                if (firstFrame != null)
                {
                    firstFrame.Save(framePath);
                    var result = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, framePath)));
                    result = ConvertImages.Bitmap2BitmapImage(ConvertImages.ResizeImage(ConvertImages.BitmapImage2Bitmap(result), Model.ImageSize.Value));                    
                    Model.Vid.ImageSourceList.Add(new SingleFrame { bitmap = firstFrame, bitmapImage = result });
                    FrameIndex++;
                    RaisePropertyChanged("CurrentFrameTextBlock");

                    return result;
                }
                else
                {
                    NextFrameIsEnabled = false;
                    return Model.Vid.ImageSource;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(string.Concat(ex.Message, "\r\n", ex.StackTrace));
                MessageBox.Show(ex.Message);
                return null; 
            }
        }

        public event EventHandler<MyArguments> RectangleMouseDownEvent;
        public void SelectionMouseDown(MouseButtonEventArgs e, System.Drawing.Point startPoint)
        {
            try
            {
                Model.SelectionRectangle.StartPoint = startPoint;
                Model.SelectionRectangle.Rect = new System.Windows.Shapes.Rectangle
                {
                    Stroke = System.Windows.Media.Brushes.Yellow,
                    StrokeThickness = 3
                };

                ProceedIsEnabled = true;
                RectangleMouseDownEvent?.Invoke(Model.SelectionRectangle, new MyArguments { StartPoint = startPoint });
                EnableProceedWindowEvent?.Invoke(Model.SelectionRectangle, new MyArguments());
            }
            catch (Exception ex)
            {
                Logger.Log(string.Concat(ex.Message, "\r\n", ex.StackTrace));
                MessageBox.Show(ex.Message);
            }
        }

        public event EventHandler<MyArguments> RectangleMouseMoveEvent;
        public void SelectionMouseMove(object sender, MouseEventArgs e, Point position)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Released || Model.SelectionRectangle == null)
                    return;

                var x = Math.Min(position.X, Model.SelectionRectangle.StartPoint.X);
                var y = Math.Min(position.Y, Model.SelectionRectangle.StartPoint.Y);

                var w = Math.Max(position.X, Model.SelectionRectangle.StartPoint.X) - x;
                var h = Math.Max(position.Y, Model.SelectionRectangle.StartPoint.Y) - y;

                Model.SelectionRectangle.Rect.Width = w;
                Model.SelectionRectangle.Rect.Height = h;

                RectangleMouseMoveEvent?.Invoke(Model.SelectionRectangle, new MyArguments { RectangleX = x, RectangleY = y });
            }
            catch (Exception ex)
            {
                Logger.Log(string.Concat(ex.Message, "\r\n", ex.StackTrace));
                MessageBox.Show(ex.Message);
            }
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

                DeleteRectangleSelectionEvent?.Invoke(null, null);
                Model.ImageSize = ChangeWindowSizeEvent?.Invoke(new MyArguments { WindowHeight = firstFrame.Height, WindowWidth = firstFrame.Width });
                ImageSource = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, framePath)));
                NextFrameIsEnabled = true;
                ImageSource = ConvertImages.Bitmap2BitmapImage(ConvertImages.ResizeImage(ConvertImages.BitmapImage2Bitmap(ImageSource), Model.ImageSize.Value));
                Model.Vid.ImageSourceList.Add(new SingleFrame { bitmap = firstFrame, bitmapImage = ImageSource });                
            }
        }
        protected void BrowseFileCatchContent(Exception ex)
        {
            MessageBox.Show(ex.Message);
            ImageSource = null;
            PreviousFrameIsEnabled = false;
            NextFrameIsEnabled = false;            
            DeleteRectangleSelectionEvent?.Invoke(null, null);            
        }
    }
}
