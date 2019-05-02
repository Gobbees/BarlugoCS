namespace BarlugoFX.Model.Procedure
{
    public interface IAction
    {
        ActionEnum Type { get; }
        IAdjustment Adjustment { get; }
        IAdjustment AdjustmentBefore { get; }
        int Index { get; }
    }
}