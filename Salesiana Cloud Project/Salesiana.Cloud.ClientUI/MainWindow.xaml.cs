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

namespace Salesiana.Cloud.ClientUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        public string FileName { get; set; }
        public string FilePath { get; set; }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".*"; // Default file extension
            dlg.Filter = "Text documents (.*)|*.*"; // Filter files by extension 

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                this.uploadPathTextBox.Text = dlg.FileName;
            }
            
        }

        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            FileTransfer file = new FileTransfer();
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(this.uploadPathTextBox.Text);
            file.UploadFile(fileInfo.DirectoryName,fileInfo.Name);
        }
    }
}
