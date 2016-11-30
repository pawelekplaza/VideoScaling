using AForge.Video.FFMPEG;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media.Imaging;
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
                //CreateSampleVideo();                
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
                                Bitmap newFrame = ResizeImage(newFr, Size.Width, Size.Height);
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

                                if (VidInfo.SecondCenter.X + (w / 2) > writer.Width)
                                {
                                    if (VidInfo.SecondCenter.Y + (h / 2) > writer.Height)
                                        scaledBitmap = CropImage(newFr, new Rectangle { Location = new System.Drawing.Point(writer.Width - w, writer.Height - h), Width = w, Height = h });     // bottom right
                                    else if (VidInfo.SecondCenter.Y - (h / 2) < 0)
                                        scaledBitmap = CropImage(newFr, new Rectangle { Location = new System.Drawing.Point(writer.Width - w, 0), Width = w, Height = h });     // top right
                                    else
                                        scaledBitmap = CropImage(newFr, new Rectangle { Location = new System.Drawing.Point(writer.Width - w, VidInfo.SecondCenter.Y - (h / 2)), Width = w, Height = h });      // right
                                }
                                else if (VidInfo.SecondCenter.X - (w / 2) < 0)
                                {
                                    if (VidInfo.SecondCenter.Y + (h / 2) > writer.Height)
                                        scaledBitmap = CropImage(newFr, new Rectangle { Location = new System.Drawing.Point(0, writer.Height - h), Width = w, Height = h });        // bottom left
                                    else if (VidInfo.SecondCenter.Y - (h / 2) < 0)
                                        scaledBitmap = CropImage(newFr, new Rectangle { Location = new System.Drawing.Point(0, 0), Width = w, Height = h });        // top left
                                    else
                                        scaledBitmap = CropImage(newFr, new Rectangle { Location = new System.Drawing.Point(0, VidInfo.SecondCenter.Y - (h / 2)), Width = w, Height = h });     // left
                                }
                                else if (VidInfo.SecondCenter.Y + (h / 2) > writer.Height)
                                    scaledBitmap = CropImage(newFr, new Rectangle { Location = new System.Drawing.Point(VidInfo.SecondCenter.X - (w / 2), writer.Height - h), Width = w, Height = h });     // bottom
                                else if (VidInfo.SecondCenter.Y - (h / 2) < 0)
                                    scaledBitmap = CropImage(newFr, new Rectangle { Location = new System.Drawing.Point(VidInfo.SecondCenter.X - (w / 2), 0), Width = w, Height = h });     // top
                                else
                                    scaledBitmap = CropImage(newFr, new Rectangle { Location = new System.Drawing.Point(VidInfo.SecondCenter.X - (w / 2), VidInfo.SecondCenter.Y - (h / 2)), Width = w, Height = h });      // center



                                Bitmap newFrame = ResizeImage(scaledBitmap, Size.Width, Size.Height);                                
                                VidInfo.ImageSourceListIndex++;                                
                                writer.WriteVideoFrame(newFrame);                                
                                scaledBitmap.Dispose();
                                newFr.Dispose();
                                newFrame.Dispose();
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

        //private Bitmap cropImage(Bitmap b, System.Windows.Point p, int width, int height)
        //{
        //    //Bitmap nb = new Bitmap(width, height);
        //    //Graphics g = Graphics.FromImage(nb);
        //    //g.DrawImage(b, -(int)p.X, -(int)p.Y, width, height);
        //    //nb.Save("halo\\SIEMA.bmp");
        //    //return nb;
        //    Image image = b;                        
        //}


        private Bitmap CropImage(Image originalImage, Rectangle sourceRectangle, Rectangle? destinationRectangle = null)
        {
            if (destinationRectangle == null)
            {
                destinationRectangle = new Rectangle(System.Drawing.Point.Empty, sourceRectangle.Size);
            }

            var croppedImage = new Bitmap(destinationRectangle.Value.Width,
                destinationRectangle.Value.Height);
            using (var graphics = Graphics.FromImage(croppedImage))
            {
                graphics.DrawImage(originalImage, destinationRectangle.Value,
                    sourceRectangle, GraphicsUnit.Pixel);
            }
            return croppedImage;
        }


        // Temporar method
        private void CreateSampleVideo()
        {
            using (VideoFileWriter writer = new VideoFileWriter())
            {
                Bitmap bm = new Bitmap("a.bmp");
                var size = new System.Drawing.Size(640, 480);
                Bitmap newFrame = new Bitmap(bm, size);
                bm.Dispose();

                writer.Open(OutputDir + "\\" + Directories.OutputPath + "SAMPLEVIDEO" + ".mp4", 640, 480, 30, VideoCodec.MPEG4, 8000000);
                for (int i = 0; i < 200; i++)
                    writer.WriteVideoFrame(newFrame);

                newFrame.Dispose();
                writer.Dispose();
            }
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }        
    }
}
