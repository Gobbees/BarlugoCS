using BarlugoFX.Model.ImageTools;
using BarlugoFX.Model.Tools.Common;

namespace BarlugoFX.Model.Tools
{
    /// <summary>
    ///  This class allows changes of an {@link Image} contrast. It only accepts one
    ///  parameter, Contrast, which must be between -255 and 255. Eventual other value
    ///  will result in an {@link IllegalStateException}.
    /// </summary>
    public class Contrast : AbstractImageTool
    {
        private const int MaxValue = 255;
        private const int Translation = 128;
        private const int DefaultValue = 0;

        private double _contrastCorrectionFactor;

        private Contrast()
        {
        }
        /// <summary>
        /// Creates a new Contrast class.
        /// </summary>
        /// <return>a new Contrast element</return>
        public static Contrast CreateContrast()
        {
            return new Contrast();
        }

        public override Tool ThisTool => Tool.Brightness;
        public override IImage ApplyTool(IImage target)
        {
            var value = GetValueFromParameter(ParameterName.Contrast, -MaxValue, MaxValue, DefaultValue);
            _contrastCorrectionFactor = (MaxValue + 4) * (value + MaxValue) / (MaxValue * (MaxValue + 4 - value));
            var pixels = target.ImageRGB;
            var newPixels = new int[target.Width,target.Height];
            for (var i = 0; i < target.Width; i++) 
            {
                for (var j = 0; j < target.Height; j++) 
                {
                    newPixels[i,j] = pixels[i,j];
                    newPixels[i,j] = ColorUtils.SetBlue(newPixels[i,j],
                        (int) (_contrastCorrectionFactor * (ColorUtils.GetBlue(pixels[i,j]) - Translation) + Translation));
                    newPixels[i,j] = ColorUtils.SetGreen(newPixels[i,j],
                        (int) (_contrastCorrectionFactor * (ColorUtils.GetGreen(pixels[i,j]) - Translation) + Translation));
                    newPixels[i,j] = ColorUtils.SetRed(newPixels[i,j],
                        (int) (_contrastCorrectionFactor * (ColorUtils.GetRed(pixels[i,j]) - Translation) + Translation));
                }
            }

            return new Image(newPixels);
        }

        protected override bool IsAccepted(ParameterName name)
        {
            return name == ParameterName.Contrast;
        }
    }
}