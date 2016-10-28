using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VideoScaling.Utils
{
    public static class Directories
    {
        public static string TmpPath { get; } = "tmp";
        public static string OutputPath { get; } = "NewVideo.mp4";


        public static void ClearTmpFolder()
        {
            try
            {
                string tmpPath = Directory.GetCurrentDirectory() + "\\" + TmpPath;
                string[] filesList = Directory.GetFiles(tmpPath);

                foreach (string s in filesList)
                    File.Delete(s);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public static string BrowseFile(string currentValue, out bool? selected)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                bool? result = dlg.ShowDialog();
                selected = result;

                if (result == true)
                    return dlg.FileName;
                return currentValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                selected = false;
                return currentValue;
            }
        }

        public static string BrowseDirectory(string currentValue)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
                var result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                    return fbd.SelectedPath;
                else
                    return currentValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return currentValue;
            }
        }
    }
}
