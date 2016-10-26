using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoScaling.Utils
{
    public static class Time
    {
        public static string GetTime()
        {
            StringBuilder s = new StringBuilder();
            DateTime now = DateTime.Now;
            s.Append(now.Year.ToString() + "_");
            s.Append(now.Month.ToString() + "_");
            s.Append(now.Day.ToString() + "_");
            s.Append(now.Hour.ToString() + "_");
            s.Append(now.Minute.ToString() + "_");
            s.Append(now.Second.ToString() + "_");
            s.Append(now.Millisecond.ToString());

            return s.ToString();
        }
    }
}
