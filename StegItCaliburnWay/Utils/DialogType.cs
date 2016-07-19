using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Utils
{
    public class DialogType
    {
        public static Type Text = new Type(".txt", "TXT Files (*.txt)|*.txt");
        public static Type Image = new Type(".bmp", "BMP Files (*.bmp)|*.bmp|GIF Files (*.gif)|*.gif");
        public static Type Sound = new Type(".bmp", "BMP Files (*.bmp)|*.bmp|GIF Files (*.gif)|*.gif");
    }

    public class Type
    {
        public string defaultExt;
        public string filter;

        public Type(string defaultExt, string filter)
        {
            this.defaultExt = defaultExt;
            this.filter = filter;
        }
    }



}
