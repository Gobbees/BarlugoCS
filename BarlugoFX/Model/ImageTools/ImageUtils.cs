using System;
using System.Drawing;
namespace BarlugoFX.Model.ImageTools
{
    public static class ImageUtils
    {
        public static Bitmap ConvertImageToBitmap(IImage target){
            Bitmap output = new Bitmap(target.Width, target.Height);
            int[,] pixels = target.ImageRGB;
            for (int i = 0; i < target.Height; i++)
            {
                for (int j = 0; j < target.Width; j++)
                {
                    output.SetPixel(j, i, Color.FromArgb(pixels[i,j]));
                }
            }
            return output;
        }
    }
}
