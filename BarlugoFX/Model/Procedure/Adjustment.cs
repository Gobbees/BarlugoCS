using BarlugoFX.Model.ImageTools;
using BarlugoFX.Model.Tools.Common;

namespace BarlugoFX.Model.Procedure
{
    public class Adjustment : IAdjustment
    {
        private bool _isEnabled;
        private IImageTool _imageTool;
        private IImage _startImage, _endImage;


        public bool IsEnabled
        {
            get { return this._isEnabled; }
            set { this._isEnabled = value; }
        }

        public IImageTool ImageTool => this._imageTool;

        public Tool ToolType => this._imageTool.ToolType;

        public IImage StartImage
        {
            get { return this._startImage; }
            set { this._startImage = value; }
        }

        public IImage EndImage
        {
            get { return this._endImage; }
            set { this._endImage = value; }
        }
    }
}