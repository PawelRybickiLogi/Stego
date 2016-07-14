using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegIt.Text
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
