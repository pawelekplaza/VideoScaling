using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoScaling.Events
{
    public class ProgressBarArguments : EventArgs
    {
        public double PBValue { get; set; }
        public string TimeProceeded { get; set; }
    }
}
