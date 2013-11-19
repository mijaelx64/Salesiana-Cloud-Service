using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salesiana.Cloud.Common.Model
{
    public interface ITransferFile
    {
        /// <summary>
        /// Upload a file: the path indicates where this file is
        /// </summary>
        /// <param name="path"></param>
        void UploadFile(string path,string fileName);

        /// <summary>
        /// Download a file in the next path
        /// </summary>
        /// <param name="path"></param>
        void DownloadFile(string path, string fileName);
    }
}
