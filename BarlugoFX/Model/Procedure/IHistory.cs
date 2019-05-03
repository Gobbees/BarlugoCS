using NUnit.Framework;

namespace BarlugoFX.Model.Procedure
{
    public interface IHistory
    {
        void addAction(IAction action);

        IAction undoAction();

        IAction redoAction();
        
        int Size { get; }
    }
}