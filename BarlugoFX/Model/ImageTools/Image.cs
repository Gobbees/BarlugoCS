using System;
using System.Drawing;

namespace BarlugoFX.Model.ImageTools
{
    public class Image : IImage
    {
        private readonly int width;
        private readonly int height;
        private const Boolean hasAlphaChannel = true; //Bitmap type always have alpha channel
        private readonly int[,] pixels;

        public Image(Bitmap target)
        {
            width = target.Width;
            height = target.Height;
            pixels = new int[height, width];
            for (int i = 0; i < target.Height; i++)
            {
                for (int j = 0; j < target.Width; j++)
                {
                    pixels[i, j] = target.GetPixel(j, i).ToArgb();
                }
            }
        }

        public Image(int[,] toCopy){
            height = toCopy.GetLength(0);
            width = toCopy.GetLength(1);
            pixels = toCopy;
        }

        public int Width => width;
        public int Height => height;
        public int[,] ImageRGB => pixels;
    }
}
