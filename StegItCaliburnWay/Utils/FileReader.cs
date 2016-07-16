using System;
using System.IO;

namespace StegItCaliburnWay
{
    class FileReader
    {
        public static byte[] ReadFile(String fileName)
        {
            var fs = File.OpenRead(fileName);
            var bytes = new byte[fs.Length];

            fs.Read(bytes, 0, (int)fs.Length);
            fs.Close();

            return bytes;
        }
    }
}
