using GalaSoft.MvvmLight;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using VideoScaling.Models;
using VideoScaling.Utils;
using VideoScaling.Working;

namespace VideoScaling.ViewModels
{
    public class WaitingViewModel : ViewModelBase
    {
        private WaitingModel Model;        

        public WaitingViewModel()
        {
            Model = new WaitingModel();            
        }

        public WaitingViewModel(VideoInfo vid)
        {
            Model = new WaitingModel(vid);
        }

        public double ProgressBarValue
        {
            get { return Model.PBValue; }
            set { Model.PBValue = value; RaisePropertyChanged("ProgressBarValue"); }
        }
        public double ProgressBarMaximum
        {
            get { return Model.PBMaximum; }
            set { Model.PBMaximum = value; RaisePropertyChanged("ProgressBarMaximum"); }
        }
        public VideoInfo VidInfo
        {
            get { return Model.Vid; }
            set { Model.Vid = value; }
        }

        public void LoadVideo()
        {
            try
            {
                ProgressBarMaximum = Model.Vid.VideoReader.FrameCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }          
        }
        public BitmapImage ReadNextFrame()
        {
            try
            {
                string framePath = Directories.TmpPath + "\\firstFrame_" + Time.GetTime() + ".bmp";
                var firstFrame = Model.Vid.VideoReader.ReadVideoFrame();
                if (firstFrame != null)
                {
                    firstFrame.Save(framePath);
                    var result = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, framePath)));
                    Model.Vid.ImageSourceList.Add(new SingleFrame { bitmap = firstFrame, bitmapImage = result });
                    Model.Vid.ImageSourceListIndex++;
                    return result;
                }

                Model.Vid.ImageSourceListIndex++;
                return null;
            }
            catch (Exception ex)
            {
                Logger.Log(string.Concat(ex.Message, "\r\n", ex.StackTrace));
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
