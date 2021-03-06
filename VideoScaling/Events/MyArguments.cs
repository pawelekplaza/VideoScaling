﻿using System;
using System.Windows.Media.Imaging;
using VideoScaling.Views;
using VideoScaling.Working;

namespace VideoScaling.Events
{
    public class MyArguments : EventArgs
    {
        public double WindowHeight { get; set; }
        public double WindowWidth { get; set; }
        public System.Drawing.Point StartPoint { get; set; }
        public double RectangleX { get; set; }
        public double RectangleY { get; set; }        
        public Models.Selection BaseSelectionRectangle { get; set; }
        public MainView MainPage { get; set; }
        public SecondView SecondPage { get; set; }
        public VideoInfo VidInfo { get; set; }      
    }
}
