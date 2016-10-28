using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using VideoScaling.ViewModels;
using VideoScaling.Views;
using VideoScaling.Working;

namespace VideoScaling.Models
{   
    public class SecondModel
    {
        public VideoInfo Vid { get; set; }
        public System.Windows.Point SelectionStartPoint { get; set; }
        public System.Windows.Shapes.Rectangle SelectionRectangle { get; set; }
        public System.Windows.Shapes.Rectangle BaseSelectionRectangle { get; set; }
        public MainView MainPage { get; set; }
        public MainViewModel MainContext { get; set; }

        public SecondModel()
        {
            Vid = new VideoInfo();
            Vid.ImageSourceList = new List<SingleFrame>();
            Vid.VideoReader = new VideoFileReader();
            MainPage = new MainView();
        }
    }
}
