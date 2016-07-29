using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegItCaliburnWay.Utils.Audio;

namespace StegItCaliburnWay.Logic.Steganography.AudioSteganography.Methods.Wave
{
    public class AudioFile
    {
        public byte[] bytes;
        public WaveFile waveFile;
        public byte[] hiddenMessageBytes;

        public AudioFile(byte[] bytes)
        {
            this.bytes = bytes;
            this.waveFile = new WaveFile(bytes);
        }

        public AudioFile(byte[] bytes, byte[] hiddenMessageBytes)
        {
            this.bytes = bytes;
            this.waveFile = new WaveFile(bytes);
            this.hiddenMessageBytes = hiddenMessageBytes;
        }
    }
}
