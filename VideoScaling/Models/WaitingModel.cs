using VideoScaling.Working;

namespace VideoScaling.Models
{
    public class WaitingModel
    {
        public double PBMaximum { get; set; }
        public double PBValue { get; set; }
        public VideoInfo Vid { get; set; }        

        public WaitingModel()
        {
            Vid = new VideoInfo();
        }

        public WaitingModel(VideoInfo vid)
        {
            Vid = vid;
        }
    }
}
