using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoScaling.Models;
using VideoScaling.Views;
using VideoScaling.Working;

namespace VideoScaling.Events
{
    public class MyArguments : EventArgs
    {
        public double WindowHeight { get; set; }
        public double WindowWidth { get; set; }
        public System.Windows.Point StartPoint { get; set; }
        public double RectangleX { get; set; }
        public double RectangleY { get; set; }
        public System.Windows.Shapes.Rectangle BaseSelectionRectangle { get; set; }
        public System.Windows.Shapes.Rectangle SecondSelectionRectangle { get; set; }

        public List<SingleFrame> ImageSourceList { get; set; }
        public int Index { get; set; }
        public int FrameRate { get; set; }

        public double PBValue { get; set; }
        public double PBMaximum { get; set; }

        public MainView MainPage { get; set; }
        public SecondView SecondPage { get; set; }

        public VideoInfo VidInfo { get; set; }
    }
}
