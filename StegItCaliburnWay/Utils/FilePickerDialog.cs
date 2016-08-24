using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using StegItCaliburnWay.Logic.Steganography.AudioSteganography.Methods.Wave;
using StegItCaliburnWay.Utils;
using StegItCaliburnWay.Utils.ExtensionsMethods;
using StegItCaliburnWay.Utils.Video;
using Type = StegItCaliburnWay.Utils.Type;

namespace StegItCaliburnWay
{
    public class FilePickerDialog
    {
        private readonly AviFileReading _aviFileReading;

        public FilePickerDialog(AviFileReading aviFileReading)
        {
            _aviFileReading = aviFileReading;
        }

        public ImageFile OpenReadImageDialog(Type fileType)
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = fileType.defaultExt,
                Filter = fileType.filter
            };

            dlg.ShowDialog();

            if (dlg.FileName == "")
            {
                throw new ArgumentException();
            }

            var bitmap = (Bitmap) Image.FromFile(dlg.FileName, true);
            var bytes = FileReader.ReadFile(dlg.FileName);
 
            return new ImageFile(bitmap, bytes);
        }

        public void OpenSaveImageDialog(Bitmap bitmapImage, Type fileType)
        {
            var dlg = new SaveFileDialog
            {
                DefaultExt = fileType.defaultExt,
                Filter = fileType.filter
            };

            dlg.ShowDialog();

            if (dlg.FileName == "")
                return;

            if (bitmapImage.HasAlphaChannel() || bitmapImage.IsIndextedImage())
                bitmapImage.Save(dlg.FileName);
            else
                bitmapImage.SaveAsNot32BitImage(dlg.FileName);
        }

        public byte[] OpenReadDialog(Type dialogType)
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = dialogType.defaultExt,
                Filter = dialogType.filter
            };

            dlg.ShowDialog();

            if (dlg.FileName == "")
                return null;

            var bytes = FileReader.ReadFile(dlg.FileName);

            return bytes;
        }

        public void OpenSaveDialog(Type dialogType, byte[] hiddenMessage)
        {
            var dlg = new SaveFileDialog
            {
                DefaultExt = dialogType.defaultExt,
                Filter = dialogType.filter
            };
            
            dlg.ShowDialog();

            if (dlg.FileName == "") 
                return;

            FileWriter.WriteToFile(dlg.FileName, hiddenMessage);
        }

        public AudioFile OpenReadAudioDialog(Type fileType)
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = fileType.defaultExt,
                Filter = fileType.filter
            };

            dlg.ShowDialog();

            if (dlg.FileName == "")
            {
                throw new ArgumentException();
            }

            var bytes = FileReader.ReadFile(dlg.FileName);

            return new AudioFile(bytes);
        }

        public VideoFile OpenReadVideoDialog(Type dialogType)
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = dialogType.defaultExt,
                Filter = dialogType.filter
            };

            dlg.ShowDialog();

            if (dlg.FileName == "")
            {
                throw new ArgumentException();
            }

            _aviFileReading.Open(dlg.FileName);

            var videoFile = new VideoFile(dlg.FileName)
            {
                FrameHeight = _aviFileReading.Bih.biHeight,
                FrameWidth = _aviFileReading.Bih.biWidth,
                FrameCount = _aviFileReading.CountFrames,
                FrameRate = _aviFileReading.FrameRate
            };

            _aviFileReading.Close();

            return videoFile;
        }
    }
}
