using System;
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
        public MainView MainPage { get; set; }
        public SecondView SecondPage { get; set; }
        public VideoInfo VidInfo { get; set; }        
    }
}
