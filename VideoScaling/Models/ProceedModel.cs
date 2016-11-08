using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
