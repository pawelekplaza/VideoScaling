﻿using System;
using System.IO;

namespace VideoScaling.Utils
{
    public static class Logger
    {
        public static string LogFileName = "log.txt";

        public static void Log(string message)
        {
            using (FileStream File = new FileStream(LogFileName, FileMode.Append, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(File))
            {
                writer.WriteLine(DateTime.Now.ToString() + "\t" + message);
            }
        }

        public static void Clear()
        {
            string x = "log.txt";

            if (File.Exists(x))
                File.Delete(x);
        }
    }
}
