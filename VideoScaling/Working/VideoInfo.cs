using AForge.Video.FFMPEG;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using VideoScaling.Models;

namespace VideoScaling.Working
{
    public class VideoInfo
    {
        public string FilePath { get; set; }
        public BitmapImage ImageSource { get; set; }
        public List<SingleFrame> ImageSourceList { get; set; }
        public int ImageSourceListIndex { get; set; }
        public VideoFileReader VideoReader { get; set; }
        public double ScaleWidth { get; set; }
        public double ScaleHeight { get; set; }
        public System.Windows.Point baseCenter { get; set; }
        public System.Windows.Point secondCenter { get; set; }
    }
}
