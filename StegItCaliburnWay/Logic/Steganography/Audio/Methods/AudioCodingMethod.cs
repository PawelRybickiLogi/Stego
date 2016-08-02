using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegItCaliburnWay.Logic.Steganography.AudioSteganography.Methods.Wave;

namespace StegItCaliburnWay.Logic.Steganography.AudioSteganography.Methods
{
    interface AudioCodingMethod
    {
        AudioFile CreateHiddenMessage(AudioFile containerAudioFile, byte[] messageToHide);
        AudioFile DecodeHiddenMessage(AudioFile containerAudioFile);
    }
}
