﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Salesiana.Cloud.Service
{
    class TransferService : ITransferService
    {
        public DirectoryInfo Repository { get; set; }

        public TransferService()
        {
            Repository = new DirectoryInfo(@"c:\SalesianaCloud\");
        }

        public RemoteFileInfo DownloadFile(DownloadRequest request)
        {
            RemoteFileInfo result = new RemoteFileInfo();
            try
            {
                string filePath = System.IO.Path.Combine(@"c:\SalesianaCloud\", request.FileName);
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

                // check if exists
                if (!fileInfo.Exists)
                    throw new System.IO.FileNotFoundException("File not found",
                                                              request.FileName);
                // open stream
                System.IO.FileStream stream = new System.IO.FileStream(filePath,
                          System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // return result 
                result.FileName = request.FileName;
                result.Length = fileInfo.Length;
                result.FileByteStream = stream;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result; 
        }

        public void UploadFile(RemoteFileInfo request)
        {
            FileStream targetStream = null;
            Stream sourceStream = request.FileByteStream;

            string uploadFolder = @"c:\SalesianaCloud\";
            string filePath = Path.Combine(uploadFolder, request.FileName);
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


        public DirectoryInfo FolderInformation()
        {
            return Repository;
        }

        public List<FileInformation> Files()
        {
            //DirectoryInfo dir = fileTransfer.CloudInformation();
            FileInfo[] Files = Repository.GetFiles("*.*"); //Getting Text files
            List<FileInformation> files = new List<FileInformation>();
            foreach (FileInfo file in Files)
            {
                files.Add(new FileInformation() { Name = file.Name, Size = file.Length, LastModified = file.LastWriteTime.ToString() });
            }
            return files;
        }
    }
}
