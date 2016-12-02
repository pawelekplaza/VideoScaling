using AForge.Video.FFMPEG;
using System.Collections.Generic;
using System.Drawing;
using VideoScaling.Views;
using VideoScaling.Working;

namespace VideoScaling.Models
{
    public class SecondModel
    {
        public VideoInfo Vid { get; set; }
        public Models.Selection BaseSelectionRectangle { get; set; }
        public Models.Selection SelectionRectangle { get; set; }
        public MainView MainPage { get; set; }
        public Size? ImageSize { get; set; }

        public SecondModel()
        {
            Vid = new VideoInfo();
            Vid.ImageSourceList = new List<SingleFrame>();
            Vid.VideoReader = new VideoFileReader();
            MainPage = new MainView();
        }
    }
}
