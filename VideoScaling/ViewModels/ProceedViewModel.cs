using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Threading.Tasks;
using System.Windows;
using VideoScaling.Events;
using VideoScaling.Models;
using VideoScaling.Utils;
using VideoScaling.Views;
using VideoScaling.Working;

namespace VideoScaling.ViewModels
{
    public class ProceedViewModel : ViewModelBase
    {
        private ProceedModel Model;

        public ProceedViewModel()
        {
            Model = new ProceedModel();
            OpenNewVideoFileIsEnabled = true;
            OutputPathTextBox = "Select output directory";

            BrowseOutputDirectory = new RelayCommand(() =>
            {
                try
                {
                    OutputPathTextBox = Utils.Directories.BrowseDirectory(OutputPathTextBox);
                    if (OutputPathTextBox != null)
                        StartIsEnabled = true;
                }
                catch (Exception ex)
                {
                    Logger.Log(string.Concat(ex.Message, "\r\n", ex.StackTrace));
                    MessageBox.Show(ex.Message);
                }
            });

            Start = new RelayCommand(async () =>
            {
                try
                {
                    StartIsEnabled = false;
                    OpenNewVideoFileIsEnabled = false;
                    var maker = new VideoMaker();
                    maker.PBValEvent += SetPBValue;
                    maker.CurrentTimeProceededEvent += SetCurrTimeProc;
                    maker.VidInfo = VidInfo;
                    maker.IfOpenNewVideo = OpenNewVideoFileIsChecked;
                    maker.OutputDir = OutputPathTextBox;
                    ProgressBarMax = Model.Vid.VideoReader.FrameCount;
                    if (ProgressBarMax == 0)
                        ProgressBarIndeterminate = true;
                    maker.PBIndeterminate = ProgressBarIndeterminate;
                    bool result = await Task.Factory.StartNew(() => maker.MakeVideo());
                    if (result)
                    {
                        Switcher.Switch(new SuccesView());
                    }
                    else
                    {
                        MessageBox.Show("Could not create a video file.");
                        Switcher.Switch(new MainView());
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(string.Concat(ex.Message, "\r\n", ex.StackTrace));
                    MessageBox.Show(ex.Message);
                }
            });
            BackToFirstVideo = new RelayCommand(() =>
            {
                Model = null;
                Switcher.Switch(new MainView());
            });
        }

        public VideoInfo VidInfo
        {
            get { return Model.Vid; }
            set { Model.Vid = value; }
        }


        public RelayCommand BrowseOutputDirectory { get; set; }
        public RelayCommand Start { get; set; }
        public RelayCommand BackToFirstVideo { get; set; }

        private string outputPathTextBox;
        public string OutputPathTextBox
        {
            get { return outputPathTextBox; }
            set { outputPathTextBox = value; RaisePropertyChanged("OutputPathTextBox"); }
        }

        private double progressBarValue;
        public double ProgressBarValue
        {
            get { return progressBarValue; }
            set { progressBarValue = value; RaisePropertyChanged("ProgressBarValue"); }
        }
        private double progressBarMax;
        public double ProgressBarMax
        {
            get { return progressBarMax; }
            set { progressBarMax = value; RaisePropertyChanged("ProgressBarMax"); }
        }

        private bool progressBarIndeterminate;
        public bool ProgressBarIndeterminate
        {
            get { return progressBarIndeterminate; }
            set { progressBarIndeterminate = value; RaisePropertyChanged("ProgressBarIndeterminate"); }
        }

        private string currentTimeProcedeed;
        public string CurrentTimeProcedeed
        {
            get { return currentTimeProcedeed; }
            set { currentTimeProcedeed = "Current time proceeded: " + value; RaisePropertyChanged("CurrentTimeProcedeed"); }
        }


        private bool openNewVideoFileIsChecked;
        public bool OpenNewVideoFileIsChecked
        {
            get { return openNewVideoFileIsChecked; }
            set { openNewVideoFileIsChecked = value; RaisePropertyChanged("OpenNewVideoFileIsChecked"); }
        }

        private bool openNewVideoFileIsEnabled;
        public bool OpenNewVideoFileIsEnabled
        {
            get { return openNewVideoFileIsEnabled; }
            set { openNewVideoFileIsEnabled = value; RaisePropertyChanged("OpenNewVideoFileIsEnabled"); }
        }


        private bool startIsEnabled;
        public bool StartIsEnabled
        {
            get { return startIsEnabled; }
            set { startIsEnabled = value; RaisePropertyChanged("StartIsEnabled"); }
        }


        public void SetPBValue(object sender, ProgressBarArguments e)
        {
            ProgressBarValue = e.PBValue;
        }

        public void SetCurrTimeProc(object sender, ProgressBarArguments e)
        {
            CurrentTimeProcedeed = e.TimeProceeded;
        }                
    }
}
