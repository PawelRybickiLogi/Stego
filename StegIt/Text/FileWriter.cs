using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegIt.Text
{
    class FileWriter
    {
        public static void WriteToFile(String name, char[] chars)
        {
            System.IO.FileStream fw = System.IO.File.OpenWrite(name);

            var decoded = Encoding.UTF8.GetBytes(chars);

            fw.Write(decoded, 0, decoded.Length);

            fw.Close();
        }
    }
}
