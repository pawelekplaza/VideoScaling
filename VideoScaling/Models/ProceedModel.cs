using VideoScaling.Working;

namespace VideoScaling.Models
{
    public class ProceedModel
    {
        public VideoInfo Vid { get; set; }        

        public ProceedModel()
        {
            Vid = new VideoInfo();
        }
    }   
}
