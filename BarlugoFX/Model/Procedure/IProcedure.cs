using BarlugoFX.Model.ImageTools;
using BarlugoFX.Model.Tools.Common;

namespace BarlugoFX.Model.Procedure
{
    public interface IProcedure
    {
        IImage Add(IAdjustment adjustment);

        IImage Remove(Tool toolType);

        IImage Edit(Tool toolType, IAdjustment adjustment);

        IImage Disable(int index);

        IImage Enable(int index);

        IImage Undo();

        IImage Redo();

        bool IsAdjustmentEnabled(int index);

        bool CanAdd(Tool toolType);
    }
}