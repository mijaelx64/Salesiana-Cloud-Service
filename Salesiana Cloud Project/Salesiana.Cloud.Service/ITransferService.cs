﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Salesiana.Cloud.Service
{
    [ServiceContract]
    interface ITransferService
    {
        [OperationContract]
        RemoteFileInfo DownloadFile(DownloadRequest request);

        [OperationContract]
        void UploadFile(RemoteFileInfo request);

        [OperationContract]
        DirectoryInfo FolderInformation();

        [OperationContract]
        List<FileInformation> Files();
    }

    
    [MessageContract]
    public class DownloadRequest
    {
        [MessageBodyMember]
        public string FileName;
    }

    [MessageContract]
    public class RemoteFileInfo : IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public string FileName;

        [MessageHeader(MustUnderstand = true)]
        public long Length;

        [MessageBodyMember(Order = 1)]
        public System.IO.Stream FileByteStream;

        public void Dispose()
        {
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }
    }

    [MessageContract]
    public class FileInformation
    {
        [MessageBodyMember]
        public string Name;
        
        [MessageBodyMember]
        public long Size;
        
        [MessageBodyMember]
        public string LastModified;
    }
}
