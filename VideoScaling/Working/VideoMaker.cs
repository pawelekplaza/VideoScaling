using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VideoScaling.Events;
using VideoScaling.Utils;

namespace VideoScaling.Working
{
    public class VideoMaker
    {
        public event EventHandler<MyArguments> PBValEvent;        
        public bool IfOpenNewVideo { get; set; }
        public VideoInfo VidInfo { get; set; }
        public string OutputDir { get; set; }

        public bool MakeVideo()
        {
            try
            {
                using (VideoFileWriter writer = new VideoFileWriter())
                {
                    int width = MultipleOfTwo(VidInfo.VideoReader.Width * VidInfo.ScaleWidth);
                    int height = MultipleOfTwo(VidInfo.VideoReader.Height * VidInfo.ScaleHeight);
                    writer.Open(OutputDir + "\\" + Directories.OutputPath, width, height, VidInfo.VideoReader.FrameRate, VideoCodec.MPEG4, 8000000);

                    for (int i = 0; i < VidInfo.ImageSourceListIndex; i++) 
                    {
                        Bitmap newFrame = new Bitmap(VidInfo.ImageSourceList[i].bitmap, new System.Drawing.Size(writer.Width, writer.Height));                        
                        writer.WriteVideoFrame(newFrame);
                        VidInfo.ImageSourceList[i].bitmap.Dispose();                                      
                        PBValEvent?.Invoke(this, new MyArguments { PBValue = i });
                    }

                    OpenNewVideo();
                    writer.Dispose();
                    PBValEvent?.Invoke(this, new MyArguments { PBValue = 0 });                    
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private int MultipleOfTwo(double x)
        {
            int tmp = Convert.ToInt32(x);
            if (tmp % 2 == 1)
                tmp++;
            return tmp;
        }
        private void OpenNewVideo()
        {
            if (IfOpenNewVideo)
            {
                try
                {
                    Process.Start(OutputDir + "\\" + Directories.OutputPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
