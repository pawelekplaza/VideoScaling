using AForge.Video.FFMPEG;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using VideoScaling.Events;
using VideoScaling.Utils;

namespace VideoScaling.Working
{
    public class VideoMaker
    {
        public event EventHandler<ProgressBarArguments> PBValEvent;
        public event EventHandler<ProgressBarArguments> CurrentTimeProceededEvent;   
        public bool IfOpenNewVideo { get; set; }
        public bool PBIndeterminate { get; set; }
        public VideoInfo VidInfo { get; set; }
        public string OutputDir { get; set; }
        public string FileTime { get; set; }
        private VideoFileReader Reader { get; set; }

        public VideoMaker()
        {
            Reader = new VideoFileReader();            
       }

        public bool MakeVideo()
        {
            try
            {
                Reader.Open(VidInfo.FilePath);
                VidInfo.ImageSourceListIndex = 0;

                using (VideoFileWriter writer = new VideoFileWriter())                
                {                    
                    int width = MultipleOfTwo(VidInfo.VideoReader.Width * VidInfo.ScaleWidth);
                    int height = MultipleOfTwo(VidInfo.VideoReader.Height * VidInfo.ScaleHeight);
                    FileTime = Time.GetTime();

                    if (width <= VidInfo.VideoReader.Width && height <= VidInfo.VideoReader.Height)
                    {                        
                        writer.Open(OutputDir + "\\" + Directories.OutputPath + FileTime + ".mp4", width, height, VidInfo.VideoReader.FrameRate, VideoCodec.MPEG4, 8000000);
                        long seconds = 0;
                        var Size = new System.Drawing.Size(writer.Width, writer.Height);

                        for (int i = 0; i <= VidInfo.ImageSourceListIndex; i++)
                        {
                            Bitmap newFr = Reader.ReadVideoFrame();
                            if (newFr != null)
                            {
                                VidInfo.ImageSourceListIndex++;
                                Bitmap newFrame = new Bitmap(newFr, Size);
                                writer.WriteVideoFrame(newFrame);
                                newFrame.Dispose();
                                newFr.Dispose();
                                if (!PBIndeterminate && i % 100 == 0)
                                    PBValEvent?.Invoke(this, new ProgressBarArguments { PBValue = i });


                                var tmpSec = seconds;
                                seconds = i / VidInfo.VideoReader.FrameRate;
                                long hProc = seconds / 3600;
                                seconds -= 3600 * hProc;
                                long mProc = seconds / 60;
                                seconds -= 60 * mProc;

                                if (tmpSec != seconds)
                                    CurrentTimeProceededEvent?.Invoke(this, new ProgressBarArguments { TimeProceeded = hProc.ToString() + ":" + mProc.ToString() + ":" + seconds.ToString() });
                            }
                        }
                    }
                    else
                    {
                        writer.Open(OutputDir + "\\" + Directories.OutputPath + FileTime + ".mp4", VidInfo.VideoReader.Width, VidInfo.VideoReader.Height, VidInfo.VideoReader.FrameRate, VideoCodec.MPEG4, 8000000);
                        long seconds = 0;
                        var Size = new System.Drawing.Size(writer.Width, writer.Height);

                        for (int i = 0; i <= VidInfo.ImageSourceListIndex; i++)
                        {
                            Bitmap newFr = Reader.ReadVideoFrame();                            
                            if (newFr != null)
                            {
                                int w = (int)(writer.Width * (1 / VidInfo.ScaleWidth));
                                int h = (int)(writer.Height * (1 / VidInfo.ScaleHeight));
                                Bitmap scaledBitmap;

                                if (VidInfo.SecondSelection.StartPoint.X + w > writer.Width)
                                {
                                    if (VidInfo.SecondSelection.StartPoint.Y + h > writer.Height)
                                    {
                                        scaledBitmap = cropImage(newFr, )
                                    }

                                }                                                
                                scaledBitmap = cropImage(newFr, VidInfo.SecondSelection.StartPoint, w, h);
                                VidInfo.ImageSourceListIndex++;
                                //Bitmap newFrame = new Bitmap(newFr, Size);
                                writer.WriteVideoFrame(scaledBitmap);
                                scaledBitmap.Dispose();
                                newFr.Dispose();
                                if (!PBIndeterminate && i % 100 == 0)
                                    PBValEvent?.Invoke(this, new ProgressBarArguments { PBValue = i });


                                var tmpSec = seconds;
                                seconds = i / VidInfo.VideoReader.FrameRate;
                                long hProc = seconds / 3600;
                                seconds -= 3600 * hProc;
                                long mProc = seconds / 60;
                                seconds -= 60 * mProc;

                                if (tmpSec != seconds)
                                    CurrentTimeProceededEvent?.Invoke(this, new ProgressBarArguments { TimeProceeded = hProc.ToString() + ":" + mProc.ToString() + ":" + seconds.ToString() });
                            }
                        }
                    }

                    OpenNewVideo();
                    writer.Dispose();
                    PBValEvent?.Invoke(this, new ProgressBarArguments { PBValue = 0 });
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
                    Process.Start(OutputDir + "\\" + Directories.OutputPath + FileTime + ".mp4");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private Bitmap cropImage(Bitmap b, System.Windows.Point p, int width, int height)
        {
            Bitmap nb = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(nb);
            g.DrawImage(b, (int)p.X, (int)p.Y, width, height);
            return nb;
        }
    }
}
