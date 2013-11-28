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
using Salesiana.Cloud.Common.Model;
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
            fileList.ItemsSource = fileTransfer.CloudFiles();
        }

        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog uploadFile_dialogBox = new Microsoft.Win32.OpenFileDialog();
            uploadFile_dialogBox.FileName = ""; // Default file name
            uploadFile_dialogBox.Title = "One Drive - Upload File";
            uploadFile_dialogBox.DefaultExt = ".*"; // Default file extension
            uploadFile_dialogBox.Filter = "All files (.*)|*.*"; // Filter files by extension 
            
            // Show open file dialog box
            Nullable<bool> result = uploadFile_dialogBox.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(uploadFile_dialogBox.FileName);
                fileTransfer.UploadFile(fileInfo.DirectoryName, fileInfo.Name);
            }
            RefreshContent();
        }

        private void downloadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fileName = ((FileData)fileList.SelectedItem).Name;
                Microsoft.Win32.SaveFileDialog downloadFile_dialogBox = new Microsoft.Win32.SaveFileDialog();
                downloadFile_dialogBox.Title = "One Drive - Download File";
                downloadFile_dialogBox.FileName = fileName;
                downloadFile_dialogBox.Filter = "All files (.*)|*.*";

                Nullable<bool> result = downloadFile_dialogBox.ShowDialog();

                if (result == true)
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(downloadFile_dialogBox.FileName);
                    fileTransfer.DownloadFile(fileInfo.DirectoryName, fileName, downloadFile_dialogBox.FileName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Select an item to download.","One Drive - Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
