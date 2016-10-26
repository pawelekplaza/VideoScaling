using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using VideoScaling.Views;

namespace VideoScaling.Models
{
    public class SecondModel
    {
        public string FilePath { get; set; }
        public BitmapImage ImageSource { get; set; }
        public List<SingleFrame> ImageSourceList { get; set; }
        public int ImageSourceListIndex { get; set; }
        public VideoFileReader VideoReader { get; set; }
        public System.Windows.Point SelectionStartPoint { get; set; }
        public System.Windows.Shapes.Rectangle SelectionRectangle { get; set; }
        public System.Windows.Shapes.Rectangle BaseSelectionRectangle { get; set; }
        public MainView MainView { get; set; }

        public SecondModel()
        {
            ImageSourceList = new List<SingleFrame>();
            VideoReader = new VideoFileReader();
        }
    }
}
