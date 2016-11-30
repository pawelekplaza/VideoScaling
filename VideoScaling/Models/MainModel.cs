using AForge.Video.FFMPEG;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Imaging;
using VideoScaling.Views;

namespace VideoScaling.Models
{
    public struct SingleFrame
    {
        public BitmapImage bitmapImage;
        public Bitmap bitmap;
    }

    public class Selection
    {
        public System.Drawing.Point StartPoint { get; set; }
        public System.Windows.Shapes.Rectangle Rect { get; set; }

        public Selection()
        {
            StartPoint = new System.Drawing.Point();
            Rect = new System.Windows.Shapes.Rectangle();
        }

        public System.Drawing.Point GetCenter()
        {
            System.Drawing.Point result = new System.Drawing.Point();
            result.X = StartPoint.X + ((int)Rect.ActualWidth / 2);
            result.Y = StartPoint.Y + ((int)Rect.ActualHeight / 2);

            return result;
        }
    }

    public class MainModel
    {
        public string FilePath { get; set; }
        public BitmapImage ImageSource { get; set; }
        public List<SingleFrame> ImageSourceList { get; set; }
        public int ImageSourceListIndex { get; set; }
        public VideoFileReader VideoReader { get; set; }
        //public System.Windows.Point SelectionStartPoint { get; set; }
        //public System.Windows.Shapes.Rectangle SelectionRectangle { get; set; }        
        public Selection SelectionRectangle { get; set; }
        public SecondView SecondPage { get; set; }        

        public MainModel()
        {
            ImageSourceList = new List<SingleFrame>();
            VideoReader = new VideoFileReader();
        }   
    }
}
