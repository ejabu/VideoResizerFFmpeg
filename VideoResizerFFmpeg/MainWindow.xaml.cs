using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace VideoResizerFFmpeg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String fileName;

        public MainWindow()
        {
            InitializeComponent();
        }


        static void _ConvertFile(string command)
        {
            ProcessStartInfo ps = new ProcessStartInfo();
            ps.FileName = "cmd.exe";
            ps.WindowStyle = ProcessWindowStyle.Normal;
            ps.Arguments = @"/k " + command;
            Process.Start(ps);
        }

        static void ProcessFilePath(string fileName)
        {
            // taking full path of a file
            string strPath = "C:// myfiles//ref//file1.txt";

            // initialize the value of filename
            string filename = null;

            // using the method
            filename = Path.GetFileName(strPath);
            Console.WriteLine("Filename = " + filename);

            Console.ReadLine();
        }
        static string _getCommand(string fullPath, string newFullPath)
        {

            // taking full path of a file
            string command = "ffmpeg -i \"" + fullPath +  "\"  -vf \"scale=ceil(iw/2)*2:ceil(ih/2)*2\" \"" + newFullPath + "\"";
            Debug.WriteLine(command);

            // initialize the value of filename
            return command;

        }
        static string _getNewFullPath(string fullPath)
        {
            // taking full path of a file
            string strPath = "C:// myfiles//ref//file1.txt";
            strPath = fullPath;

            // initialize the value of filename
            string fileName, fileExtension, fileDirectory;
            string newFileName, newFullPath;

            fileName = Path.GetFileNameWithoutExtension(strPath);
            fileDirectory = Path.GetDirectoryName(strPath);
            fileExtension = Path.GetExtension(strPath);

            // using the method
            newFileName = fileName + DateTime.Now.ToString(" HHmmss") + fileExtension;
            newFullPath = Path.Combine(fileDirectory, newFileName);
            return newFullPath;
        }
        static void _ProcessFile(string fullPath)
        {
            // taking full path of a file
            var newFullPath = _getNewFullPath(fullPath);
            string command = _getCommand(fullPath, newFullPath); 
            _ConvertFile(command);
        }
        private void Grid_Drop(object sender, DragEventArgs e)
        {
            try
            {
                var droppedFiles = (System.Array)e.Data.GetData(DataFormats.FileDrop);
                foreach (string dropFile in droppedFiles)
                {
                    Debug.WriteLine(dropFile);
                    _ProcessFile(dropFile);
                }
                // fileName = droppedFileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(fileName);

        }
    }
}
