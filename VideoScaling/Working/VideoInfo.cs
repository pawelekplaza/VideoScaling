using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
