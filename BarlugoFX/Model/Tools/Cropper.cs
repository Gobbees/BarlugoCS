using System.Collections.Generic;
using System.Net.WebSockets;
using BarlugoFX.Model.ImageTools;
using BarlugoFX.Model.Tools.Common;

namespace BarlugoFX.Model.Tools
{
    public class Cropper : AbstractImageTool
    {
        private static readonly int MinValue1 = 0;
        private static readonly int MaxValue2 = 1;
        private static readonly int DefaultValue1 = 0;

        private static readonly HashSet<ParameterName> Accepted = new HashSet<ParameterName> {ParameterName.X1, ParameterName.X2, ParameterName.Y1, ParameterName.Y2};

        private Cropper()
        {
        }

        public static Cropper CreateCropper()
        {
            return new Cropper();
        }
        public override Tool ThisTool => Tool.Cropper;
        public override IImage ApplyTool(IImage target)
        {
            var x2 = (int) GetValueFromParameter(ParameterName.X2, MaxValue2, target.Width, target.Width);
            var x1 = (int) GetValueFromParameter(ParameterName.X1, MinValue1, x2, DefaultValue1);
            var y2 = (int) GetValueFromParameter(ParameterName.Y2, MaxValue2, target.Height, target.Height);
            var y1 = (int) GetValueFromParameter(ParameterName.Y1, MinValue1, y2, DefaultValue1);
            var pixels = target.ImageRGB;
            var newPixels = new int[y2 - y1, x2 - x1];
            for (int i = 0; i < newPixels.GetLength(0); i++) {
                for (int j = 0; j < newPixels.GetLength(1); j++) {
                    newPixels[i, j] = pixels[i +y1, j + x1];
                }
            }
            return new Image(newPixels);
        }

        protected override bool IsAccepted(ParameterName name)
        {
            return Accepted.Contains(name);
        }
    }
}