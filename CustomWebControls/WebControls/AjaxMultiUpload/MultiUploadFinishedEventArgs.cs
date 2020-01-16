using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace APTemplate
{
    public class MultiUploadFilesFinishedEventArgs : EventArgs
    {
        private List<FileInfo> _UploadedFiles;
        public MultiUploadFilesFinishedEventArgs(List<FileInfo> UploadedFiles)
        {
            _UploadedFiles = UploadedFiles;
        }
        public List<FileInfo> UploadedFiles
        {
            get { return _UploadedFiles; }
        }
    }
}
