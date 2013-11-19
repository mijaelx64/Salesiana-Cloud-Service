using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salesiana.Cloud.ServiceManager.TransferServiceModel;
//using System.IO;

namespace Salesiana.Cloud.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            ITransferService transfer = new TransferServiceClient();
            RemoteFileInfo result = new RemoteFileInfo();
            string fileName = "En Mi Lado Del Sofa Videoclip La Oreja de Van Gogh. By elixir.avi";
            try
            {
                string filePath = System.IO.Path.Combine(@"F:\SalesianaDrive", fileName);
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

                // check if exists
                if (!fileInfo.Exists)
                    throw new System.IO.FileNotFoundException("File not found",
                                                              fileName);

                // open stream
                System.IO.FileStream stream = new System.IO.FileStream(filePath,
                          System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // return result 
                result.FileName = fileName;
                result.Length = fileInfo.Length;
                result.FileByteStream = stream;

                transfer.UploadFile(result);
                Console.WriteLine("Successfully uploaded");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error "+ex);
            }
            ////////////////////////
            Console.WriteLine("Press any key to continue -- to test Donwload");
            Console.ReadLine();

            
            DownloadRequest downRequest = new DownloadRequest("En Mi Lado Del Sofa Videoclip La Oreja de Van Gogh. By elixir.avi");
            
            RemoteFileInfo remoteFile = transfer.DownloadFile(downRequest);

            FileStream targetStream = null;
            Stream sourceStream = remoteFile.FileByteStream;

            string uploadFolder = @"f:\SalesianaDrive Down\";
            string path = Path.Combine(uploadFolder, remoteFile.FileName);
            using (targetStream = new FileStream(path, FileMode.Create,
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
            } */

            FileTransfer test = new FileTransfer();
            try
            {
                test.DownloadFile(@"f:\SalesianaDrive Down", "En Mi Lado Del Sofa Videoclip La Oreja de Van Gogh. By elixir.avi");
                Console.Write("Success");
            }
            catch (Exception e)
            {
               Console.Write("Error" + e.Message);
            }
            Console.ReadLine();
            
        }
    }
}
