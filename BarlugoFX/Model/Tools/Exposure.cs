using System;
using System.Drawing;
using BarlugoFX.Model.ImageTools;
using BarlugoFX.Model.Tools.Common;

namespace BarlugoFX.Model.Tools
{
    public class Exposure : AbstractImageTool
    {
        private const int Max = 1;
        private const int Min = -1;
        private const float DefaultValue = 0f;
        private double _value;

        private Exposure()
        {
            _value = DefaultValue;
        }

        /// <summary>
        /// Creates a new Exposure.
        /// </summary>
        /// <returns>the instantiated modifier</returns>
        public static Exposure CreateExposure()
        {
            return new Exposure();
        }

        public override Tool ThisTool => Tool.Exposure;
        public override IImage ApplyTool(IImage target)
        {
            _value = GetValueFromParameter(ParameterName.Exposure, Min, Max, DefaultValue);
            var pixels = target.ImageRGB;
            var newPixels = new int[target.Height, target.Width];
            for (var i = 0; i < target.Height; i++) 
            {
                for (var j = 0; j < target.Width; j++) 
                {
                    var hsb = Color.FromArgb(ColorUtils.GetAlpha(pixels[i, j]), ColorUtils.GetRed(pixels[i, j]),
                        ColorUtils.GetGreen(pixels[i, j]),
                        ColorUtils.GetBlue(pixels[i, j]));
                    Color res = FromHSV(hsb.GetHue(), hsb.GetSaturation(), Math.Abs(_value) < Double.Epsilon ? hsb.GetBrightness() : TruncateSum(hsb.GetBrightness(), _value));
                    newPixels[i, j] = res.ToArgb();
                    newPixels[i, j] = ColorUtils.SetAlpha(newPixels[i, j], ColorUtils.GetAlpha(pixels[i, j]));
                }
                
            }

            return new ImageTools.Image(newPixels);
        }
        
        private static double TruncateSum(double hsv, double hue) 
        {
            var result = hsv + hue;
            if (result > Max) {
                return Max;
            } else {
                if (result < 0) {
                    return 0;
                } else {
                    return result;
                }
            }
        }
        
        private Color FromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        protected override bool IsAccepted(ParameterName name)
        {
            return name == ParameterName.Exposure;
        }
    }
}