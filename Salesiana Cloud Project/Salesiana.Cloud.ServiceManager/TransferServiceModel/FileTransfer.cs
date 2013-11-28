using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salesiana.Cloud.ServiceManager.SalesianaCloudTransferService;
using Salesiana.Cloud.Common.Model;
using System.IO;

namespace Salesiana.Cloud.ServiceManager.TransferServiceModel
{
    public class FileTransfer : ITransferFile
    {
        public FileInformation[] Files { get{return TransferService.Files();} }
        
        public FileTransfer()
        {
            TransferService = new TransferServiceClient();
        }

        public ITransferService TransferService { get; set; }

        public void UploadFile(string path, string fileName)
        {

            RemoteFileInfo result = new RemoteFileInfo();
            try
            {
                string filePath = System.IO.Path.Combine(path, fileName);
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

                // check if exists
                if (!fileInfo.Exists)
                    throw new System.IO.FileNotFoundException("File not found",
                                                              filePath);

                // open stream
                System.IO.FileStream stream = new System.IO.FileStream(filePath,
                          System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // return result 
                result.FileName = fileName;
                result.Length = fileInfo.Length;
                result.FileByteStream = stream;

                TransferService.UploadFile(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        public void DownloadFile(string path, string fileName)
        {
            DownloadRequest fileRequest = new DownloadRequest(fileName);
            RemoteFileInfo remoteFile = TransferService.DownloadFile(fileRequest);

            FileStream targetStream = null;
            Stream sourceStream = remoteFile.FileByteStream;

            string filePath = Path.Combine(path, remoteFile.FileName);
            using (targetStream = new FileStream(filePath, FileMode.Create,
                                  FileAccess.Write, FileShare.None))
            {
                //read from the input stream in 65000 byte chunks

                const int bufferLen = 65000;
                byte[] buffer = new byte[bufferLen];
                int count = 0;
                while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                {
                    // save to output stream
                    targetStream.Write(buffer, 0, count);
                }
                targetStream.Close();
                sourceStream.Close();
            }
        }


        public DirectoryInfo CloudInformation()
        {
            return TransferService.FolderInformation();
        }

        public List<FileData> CloudFiles()
        {
            List<FileData> fileList = new List<FileData>();
            foreach (FileInformation item in TransferService.Files())
            {
                fileList.Add(new FileData() { Name = item.Name, Size = item.Size / (float)1048576, LastUpdate = item.LastModified });                                                
            }
            return fileList;
        }
    }
}
