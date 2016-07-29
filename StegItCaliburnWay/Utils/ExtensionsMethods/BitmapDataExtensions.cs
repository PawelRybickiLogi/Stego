using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Utils.ExtensionsMethods
{
    public static class BitmapDataExtensions
    {
        public static List<byte> GetAllPixelsIndexes(this BitmapData bmd)
        {
            var references = new List<byte>();

            for (int i = 0; i < bmd.Width; i++)
            {
                for (int j = 0; j < bmd.Height; j++)
                {
                    references.Add(bmd.GetPixel(i, j));
                }
            }

            return references;
        }

        public static unsafe void SetPixel(this BitmapData bmd, int x, int y, byte c)
        {
            byte* p = (byte*)bmd.Scan0.ToPointer();
            int offset = y * bmd.Stride + (x);
            p[offset] = c;
        }

        public static unsafe Byte GetPixel(this BitmapData bmd, int x, int y)
        {
            byte* p = (byte*)bmd.Scan0.ToPointer();
            int offset = y * bmd.Stride + x;
            return p[offset];
        }

        public static void MovePixelsToFirstHalfOfThePalette(this BitmapData bitmapData, Dictionary<byte, byte> oldToNewIndexMap, List<Color> first64EntriesFromPalette, Dictionary<byte, Color> originalPalleteColorForIndex)
        {
            for (int i = 0; i < bitmapData.Width; i++)
            {
                for (int j = 0; j < bitmapData.Height; j++)
                {
                    var oldPixelIndex = bitmapData.GetPixel(i, j);

                    if (oldToNewIndexMap.ContainsKey(oldPixelIndex))
                    {
                        bitmapData.SetPixel(i, j, oldToNewIndexMap[oldPixelIndex]);
                    }
                    else
                    {
                        var closestColorIndexInNewPalette = GetClosestColorIndex(first64EntriesFromPalette, originalPalleteColorForIndex[oldPixelIndex]);
                        bitmapData.SetPixel(i, j, (byte)closestColorIndexInNewPalette);
                    }
                }
            }
        }

        private static int GetClosestColorIndex(IEnumerable<Color> colorArray, Color baseColor)
        {
            var colors = colorArray.Select(x => new { Value = x, Diff = GetDiff(x, baseColor) }).ToList();
            var min = colors.Min(x => x.Diff);
            return colors.FindIndex(x => x.Diff == min);
        }

        private static int GetDiff(Color color, Color baseColor)
        {
            int a = color.A - baseColor.A,
                r = color.R - baseColor.R,
                g = color.G - baseColor.G,
                b = color.B - baseColor.B;
            return a * a + r * r + g * g + b * b;
        }
    }
}
