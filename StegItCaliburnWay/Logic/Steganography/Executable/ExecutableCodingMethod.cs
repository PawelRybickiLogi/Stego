using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Logic.Steganography.ExecutableFiles
{
    public interface ExecutableCodingMethod
    {
        byte[] CreateHiddenMessage(byte[] container, byte[] message);
        byte[] DecodeHiddenMessage(byte[] hiddenMessageContainer);
    }
}
