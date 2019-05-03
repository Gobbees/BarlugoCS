using System;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;

namespace BarlugoFX.Model.Procedure
{
    public class History : IHistory
    {
        public static readonly int MaxSize = 32;
        private readonly IAction[] history = new Action[History.MaxSize];

        private int currentActionIndex;
        private int lastActionIndex;

        public History()
        {
            this.currentActionIndex = -1;
            this.lastActionIndex = -1;
        }

        public void addAction(IAction action)
        {
            if (this.currentActionIndex == History.MaxSize - 1)
            {
                this.shiftLeftHistory();
            }

            for (int i = this.currentActionIndex + 1; i <= this.lastActionIndex; i++)
            {
                this.history[i] = null;
            }
            this.history[this.currentActionIndex + 1] = action;
            this.currentActionIndex++;
            this.lastActionIndex = this.currentActionIndex;
        }

        public IAction undoAction()
        {
            if (this.currentActionIndex < 0)
            {
                throw new Exception("No more actions to undo.");
            }
            IAction action = this.history[this.currentActionIndex];
            this.currentActionIndex--;
            return action;
        }

        public IAction redoAction()
        {
            if (this.currentActionIndex >= this.lastActionIndex)
            {
                throw new Exception("No more actions to undo.");
            }

            this.currentActionIndex++;
            return this.history[this.currentActionIndex];
        }

        public int Size => lastActionIndex + 1;

        private void shiftLeftHistory()
        {
            for (int i = 1; i < History.MaxSize; i++)
            {
                this.history[i - 1] = this.history[i];
            }

            this.currentActionIndex--;
            this.lastActionIndex--;
        }
    }
}