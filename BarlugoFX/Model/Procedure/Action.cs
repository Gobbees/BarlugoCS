using System.Runtime.ConstrainedExecution;

namespace BarlugoFX.Model.Procedure
{
    public class Action : IAction
    {
        private readonly ActionEnum _type;
        private readonly int _index; 
        private readonly IAdjustment _adjustmentAfter;
        private readonly IAdjustment _adjustmentBefore;

        public Action(ActionEnum type, int index, IAdjustment adjustmentAfter, IAdjustment adjustmentBefore = null)
        {
            this._type = type;
            this._adjustmentAfter = adjustmentAfter;
            this._adjustmentBefore = adjustmentBefore;
            this._index = index;
        }

        public ActionEnum Type => this._type;

        public IAdjustment Adjustment => this._adjustmentAfter;
        
        public IAdjustment AdjustmentBefore => this._adjustmentBefore;

        public int Index => this._index;
    }
}