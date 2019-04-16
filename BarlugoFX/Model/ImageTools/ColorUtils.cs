using System;
namespace BarlugoFX.Model.ImageTools
{
    public static class ColorUtils
    {
        private const int CORRECTOR = 0x000000FF;
        private const int REDSHIFT = 16;
        private const int GREENSHIFT = 8;
        private const int ALPHASHIFT = 24;
        private const int SHIFT = 8;
        private const int RESET_BLUE = unchecked((int)0xFFFFFF00);
        private const int RESET_GREEN = unchecked((int)0xFFFF00FF);
        private const int RESET_RED = unchecked((int)0xFF00FFFF);
        private const int RESET_ALPHA = 0x00FFFFFF;
        private const int MAX_CAP = 255;
        private const int MIN_CAP = 0;

        //Note that I do not use properties because I want it to be fast
        public static int GetRed(int pixel){
            return pixel >> REDSHIFT & CORRECTOR;
        }

        public static int GetBlue(int pixel)
        {
            return pixel & CORRECTOR;
        }

        public static int GetGreen(int pixel)
        {
            return pixel >> GREENSHIFT & CORRECTOR;
        }

        public static int GetAlpha(int pixel)
        {
            return pixel >> ALPHASHIFT & CORRECTOR;
        }

        public static int PixelsToInt(int red, int blue, int green, int alpha)
        {
            int rgb = alpha;
            rgb = (rgb << SHIFT) + red;
            rgb = (rgb << SHIFT) + green;
            rgb = (rgb << SHIFT) + blue;
            return rgb;
        }

        public static int SetBlue(int pixel,  int newBlueValue) 
        {
            return (pixel & RESET_BLUE) + Truncate(newBlueValue);
        }

        public static int SetGreen( int pixel,  int newGreenValue)
        {
            return (pixel & RESET_GREEN) + (Truncate(newGreenValue) << GREENSHIFT);
        }

        public static int SetRed( int pixel,  int newRedValue)
        {
            return (pixel & RESET_RED) + (Truncate(newRedValue) << REDSHIFT);
        }

        public static int SetAlpha( int pixel,  int newAlphaValue)
        {
            return (pixel & RESET_ALPHA) + (Truncate(newAlphaValue) << ALPHASHIFT);
        }

        public static int UpdateRed( int pixel,  int valueToAdd)
        {
            return SetRed(pixel, GetRed(pixel) + valueToAdd);
        }

        public static int UpdateGreen( int pixel,  int valueToAdd)
        {
            return SetGreen(pixel, GetGreen(pixel) + valueToAdd);
        }

        public static int UpdateBlue( int pixel,  int valueToAdd)
        {
            return SetBlue(pixel, GetBlue(pixel) + valueToAdd);
        }

        public static int UpdateAlpha( int pixel,  int valueToAdd)
        {
            return SetAlpha(pixel, GetAlpha(pixel) + valueToAdd);
        }

        public static int Truncate( int rgbValue)
        {
            if (rgbValue > MAX_CAP)
            {
                return MAX_CAP;
            }
            if (rgbValue < MIN_CAP)
            {
                return MIN_CAP;
            }
            return rgbValue;
        }
    }
}
