using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salesiana.Cloud.Client.Salesiana_Cloud_Service;

namespace Salesiana.Cloud.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            RemoteFileInfo result = new RemoteFileInfo();
            string fileName = "Camila - Alejate  De Mi Showcase Buenos Aires(480p_H.264-AAC).flv";
            try
            {
                string filePath = System.IO.Path.Combine(@"c:\Uploadfiles", fileName);
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

                TransferServiceClient transfer = new TransferServiceClient();
                transfer.UploadFile(fileName, fileInfo.Length, stream);
                Console.WriteLine("Successfully uploaded");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error "+ex);
            }
            Console.ReadLine();
        }
    }
}
