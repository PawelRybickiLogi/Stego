using System;
using System.Text;

namespace StegItCaliburnWay
{
    class FileWriter
    {
        public static void WriteToFile(String name, byte[] bytesToSave)
        {
            System.IO.File.WriteAllBytes(name, bytesToSave);
        }
    }
}
