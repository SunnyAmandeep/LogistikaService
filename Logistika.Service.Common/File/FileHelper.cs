using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Logistika.Service.Common.FileUtil
{
    public static class FileHelper
    {
        public static bool CreateFolder(string RootPath,string FolderName) {
            var completePath= Path.Combine(RootPath, FolderName);
            if (!string.IsNullOrEmpty(RootPath) && !string.IsNullOrEmpty(FolderName)) {
                if (!Directory.Exists(completePath))
                {
                    Directory.CreateDirectory(completePath);
                }
                else {
                    throw new Exception("Directory already exists");
                }
            }
            return Directory.Exists(completePath);
        }

        public static bool DeleteFolder(string RootPath)
        { 
            if (Directory.Exists(RootPath))
            {
                Directory.Delete(RootPath,true);
            }
            return !Directory.Exists(RootPath);
        }
        public static bool CopyFiles(IList<string> FilePaths, string DestinationFolder)
        { 
            if (FilePaths != null && FilePaths.Count() > 0)
            {
                if (Directory.Exists(DestinationFolder))
                {
                    foreach (var file in FilePaths) {
                        if (System.IO.File.Exists(file))
                        {
                            var destnationFile =  Path.Combine(DestinationFolder,Path.GetFileName(file));
                            System.IO.File.Copy(file,destnationFile); 
                        }
                    }
                    return Directory.GetFiles(DestinationFolder).Count() > 0;
                }
            }
            return false;
        }
    }
}
