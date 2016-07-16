using System;
using System.Text;

namespace StegItCaliburnWay
{
    class FileWriter
    {
        public static void WriteToFile(String name, byte[] bytesToSave)
        {
            System.IO.FileStream fw = System.IO.File.OpenWrite(name);

            fw.Write(bytesToSave, 0, bytesToSave.Length);

            fw.Close();
        }
    }
}
