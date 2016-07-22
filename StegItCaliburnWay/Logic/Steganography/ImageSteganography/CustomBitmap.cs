﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Logic.Steganography.ImageSteganography
{
    class CustomBitmap
    {
        Bitmap source = null;
        IntPtr Iptr = IntPtr.Zero;
        BitmapData bitmapData = null;
 
    public byte[] Pixels { get; set; }
    public int Depth { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public CustomBitmap(Bitmap source)
    {
        this.source = source;
    }
 
    /// <summary>
    /// Lock bitmap data
    /// </summary>
    public void LockBits()
    {
        try
        {
            Width = source.Width;
            Height = source.Height;
 
            int PixelCount = Width * Height;
 
            Rectangle rect = new Rectangle(0, 0, Width, Height);
 
            Depth = Bitmap.GetPixelFormatSize(source.PixelFormat);
 
            if (Depth != 8 && Depth != 24 && Depth != 32)
            {
                throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
            }
 
            bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite, 
                                         source.PixelFormat);
 
            int step = Depth / 8;
            Pixels = new byte[PixelCount * step];
            Iptr = bitmapData.Scan0;
 
            Marshal.Copy(Iptr, Pixels, 0, Pixels.Length);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
 
    public void UnlockBits()
    {
        try
        {
            Marshal.Copy(Pixels, 0, Iptr, Pixels.Length);
 
            source.UnlockBits(bitmapData);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
 
    public Color GetPixel(int x, int y)
    {
        Color clr = Color.Empty;
 
        int cCount = Depth / 8;
 
        int i = ((y * Width) + x) * cCount;
 
        if (i > Pixels.Length - cCount)
            throw new IndexOutOfRangeException();
 
        if (Depth == 32) 
        {
            byte b = Pixels[i];
            byte g = Pixels[i + 1];
            byte r = Pixels[i + 2];
            byte a = Pixels[i + 3]; // a
            clr = Color.FromArgb(a, r, g, b);
        }
        if (Depth == 24) 
        {
            byte b = Pixels[i];
            byte g = Pixels[i + 1];
            byte r = Pixels[i + 2];
            clr = Color.FromArgb(r, g, b);
        }
        if (Depth == 8)
        {
            byte c = Pixels[i];
            clr = Color.FromArgb(c, c, c);
        }
        return clr;
    }
 
    public void SetPixel(int x, int y, Color color)
    {

        int cCount = Depth / 8;
 
        int i = ((y * Width) + x) * cCount;
 
        if (Depth == 32) 
        {
            Pixels[i] = color.B;
            Pixels[i + 1] = color.G;
            Pixels[i + 2] = color.R;
            Pixels[i + 3] = color.A;
        }
        if (Depth == 24) 
        {
            Pixels[i] = color.B;
            Pixels[i + 1] = color.G;
            Pixels[i + 2] = color.R;
        }
        if (Depth == 8)
        {
            Pixels[i] = color.B;
        }
    }
    }
}
