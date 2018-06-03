using System;
using System.IO.Compression;

namespace Logistika.Service.Common.Compression
{
    public class Compresser
    {
        public static void Compress(String StartPath,String ZipPath)
        { 
            ZipFile.CreateFromDirectory(StartPath, ZipPath);
        }

        public static void Decompress(String ExtractPath, String ZipPath)
        {
            ZipFile.ExtractToDirectory(ZipPath, ExtractPath);
        }
    }
}
