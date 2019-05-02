using System;
using BarlugoFX.Model.ImageTools;
using BarlugoFX.Model.Tools.Common;
using System.Drawing;

namespace BarlugoFX.Model.Tools
{
    public class Saturation : AbstractImageTool
    {
        private const double Max = 1;
        private const double Min = -1;
        private const double DefaultValue = 0f;

        private double _value;
        public Saturation()
        {
            _value = DefaultValue;
        }

        /// <summary>
        /// Creates a new Saturation.
        /// </summary>
        /// <returns>the instantiated modifier</returns>
        public static Saturation CreateSaturation()
        {
            return new Saturation();
        }

        public override Tool ThisTool => Tool.Saturation;
        public override IImage ApplyTool(IImage target)
        {
            _value = GetValueFromParameter(ParameterName.Saturation, Min, Max, DefaultValue);
            var pixels = target.ImageRGB;
            var newPixels = new int[target.Height, target.Width];
            for (int i = 0; (i < target.Height); i++) {
                for (int j = 0; (j < target.Width); j++) {
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
            return name == ParameterName.Saturation;
        }
        
    }
}