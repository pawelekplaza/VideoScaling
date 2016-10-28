using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using VideoScaling.Events;
using VideoScaling.Models;
using VideoScaling.Views;
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

        public SecondView SecondPage
        {
            get { return Model.SecondPage; }
            set { Model.SecondPage = value; }
        }

        public void LoadVideo()
        {
            ProgressBarMaximum = Model.Vid.VideoReader.FrameCount;
            while (Model.Vid.ImageSourceListIndex < Model.Vid.VideoReader.FrameCount)
            {
                ProgressBarValue = Model.Vid.ImageSourceListIndex;
                ReadNextFrame();
            }

            //ShowProceedPageEvent?.Invoke(this, new MyArguments { VidInfo = this.VidInfo });
        }
        public BitmapImage ReadNextFrame()
        {
            string framePath = Utils.Directories.TmpPath + "\\firstFrame_" + Utils.Time.GetTime() + ".bmp";
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
    }
}
