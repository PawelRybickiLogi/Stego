using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegIt.Annotations;

namespace StegIt.Text.StegoTools
{
    interface ITextCodingMethod
    {
        char[] CreateHiddenMessage(char[] openedFile, char[] message);
        char[] DecodeHiddenMessage(char[] openedFile);
    }
}
