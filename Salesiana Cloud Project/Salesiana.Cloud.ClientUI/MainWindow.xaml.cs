using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Salesiana.Cloud.ServiceManager.TransferServiceModel;
using System.IO;

namespace Salesiana.Cloud.ClientUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FileTransfer fileTransfer { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            fileTransfer = new FileTransfer();
            RefreshContent();
            
        }

        public string FileName { get; set; }
        public string FilePath { get; set; }

        public void RefreshContent() 
        {
            DirectoryInfo dir = fileTransfer.CloudInformation();
            FileInfo[] Files = dir.GetFiles("*.*"); //Getting Text files
            List<FileDescription> files = new List<FileDescription>();
            foreach (FileInfo file in Files)
            {
                files.Add(new FileDescription() { Name = file.Name, Size = file.Length / (float)1048576, Modified = file.LastWriteTime.ToString() });
            }
            fileList.ItemsSource = files;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = ""; // Default file name
            dlg.DefaultExt = ".*"; // Default file extension
            dlg.Filter = "All files (.*)|*.*"; // Filter files by extension 
            
            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(dlg.FileName);
                fileTransfer.UploadFile(fileInfo.DirectoryName,fileInfo.Name);
                // Open document 
                //this.uploadPathTextBox.Text = dlg.FileName;
                
            }
            RefreshContent();
            
        }

        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
           
            
            //FileTransfer file = new FileTransfer();
            //System.IO.FileInfo fileInfo = new System.IO.FileInfo(this.uploadPathTextBox.Text);
            //file.UploadFile(fileInfo.DirectoryName,fileInfo.Name);
        }

        private void downloadButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = ((FileDescription)fileList.SelectedItem).Name;
            Microsoft.Win32.SaveFileDialog savefile = new Microsoft.Win32.SaveFileDialog();
            savefile.FileName = Name;
            savefile.Filter = "All files (.*)|*.*";

            Nullable<bool> result = savefile.ShowDialog();

            if(result == true)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(savefile.FileName);
                fileTransfer.DownloadFile(fileInfo.DirectoryName, fileName);
            }
        }
    }

    public class FileDescription
    {
        public string Name { get; set; }

        public float Size { get; set; }

        public string Modified { get; set; }
    }
}
