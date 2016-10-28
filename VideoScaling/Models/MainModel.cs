using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using VideoScaling.ViewModels;
using VideoScaling.Views;

namespace VideoScaling.Models
{
    public struct SingleFrame
    {
        public BitmapImage bitmapImage;
        public Bitmap bitmap;
    }

    public class MainModel
    {
        public string FilePath { get; set; }
        public BitmapImage ImageSource { get; set; }
        public List<SingleFrame> ImageSourceList { get; set; }
        public int ImageSourceListIndex { get; set; }
        public VideoFileReader VideoReader { get; set; }
        public System.Windows.Point SelectionStartPoint { get; set; }
        public System.Windows.Shapes.Rectangle SelectionRectangle { get; set; }        
        public SecondView SecondPage { get; set; }
        public SecondViewModel SecondContext { get; set; }

        public MainModel()
        {
            ImageSourceList = new List<SingleFrame>();
            VideoReader = new VideoFileReader();
        }   
    }
}
