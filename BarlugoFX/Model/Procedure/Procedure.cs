using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Numerics;
using System.Text;
using BarlugoFX.Model.ImageTools;
using BarlugoFX.Model.Tools.Common;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using Image = BarlugoFX.Model.ImageTools.Image;

namespace BarlugoFX.Model.Procedure
{
    public class Procedure : IProcedure
    {
        private static readonly int _MaxToolNumber = Enum.GetValues(typeof(Tool)).Length;
        private readonly IAdjustment[] _procedure = new Adjustment[_MaxToolNumber];
        private int _nextIndex;
        private readonly Dictionary<Tool, int> _toolMap = new Dictionary<Tool, int>();
        private readonly IHistory _history = new History();
        private readonly IImage _baseImage;
        
        public Procedure(IImage baseImage)
        {
            _nextIndex = 0;
            _baseImage = baseImage;
        }

        public IImage Add(IAdjustment adjustment)
        {
            if (_toolMap.ContainsKey(adjustment.ToolType))
            {
                throw new Exception("Tool already in use.");
            }

            int index = _nextIndex;
            
            IImage image = Insert(index, adjustment);

            _history.addAction(new Action(ActionEnum.Add, index, adjustment));

            return image;
        }

        private IImage Insert(int index, IAdjustment adjustment)
        {
            if (adjustment == null)
            {
                throw new Exception("Adjustment reference is null.");
            }

            if (index < 0 || index > _nextIndex)
            {
                throw new Exception("Invalid index.");
            }

            for (int i = _nextIndex; i > index; i--)
            {
                _procedure[i] = _procedure[i - 1];
            }

            _procedure[index] = adjustment;
            _toolMap.Add(adjustment.ToolType, index);
            _nextIndex++;
            _procedure[index].StartImage = (index > 0) ? _procedure[index - 1].EndImage : _baseImage;
            return ProcessImage(index);
        } 

        public IImage Remove(Tool toolType)
        {
            int index = FindByType(toolType);

            if (index == -1)
            {
                throw new Exception("Tool not present.");
            }

            _history.addAction(new Action(ActionEnum.Remove, index, _procedure[index]));
            return Delete(index);
        }

        private IImage Delete(int index)
        {
            if (index < 0 || index >= _nextIndex)
            {
                throw new Exception("Invalid index (either negative or too big.");
            }

            _toolMap.Remove(_procedure[index].ToolType);
            _procedure[index] = null;

            for (int i = index; i < _nextIndex - 1; i++)
            {
                _procedure[i] = _procedure[i + 1];
                _toolMap[_procedure[i].ToolType] = i;
            }

            _nextIndex--;
            return ProcessImage(index);
        }

        public IImage Edit(Tool toolType, IAdjustment adjustment)
        {
            int index = FindByType(toolType);

            if (index == -1)
            {
                throw new Exception("Tool not present.");
            }
            
            _history.addAction(new Action(ActionEnum.Edit, index, adjustment, _procedure[index]));
            return Replace(index, adjustment);
        }

        private IImage Replace(int index, IAdjustment adjustment)
        {
            if (index < 0 || index > _nextIndex)
            {
                throw new Exception("Invalid index");
            }

            if (adjustment == null)
            {
                throw new Exception("Adjustment reference is null.");
            }

            if (_procedure[index].ToolType != adjustment.ToolType)
            {
                throw new Exception("Adjustments tool type isn't the same.");
            }

            adjustment.StartImage = _procedure[index].StartImage;
            _procedure[index] = adjustment;
            return ProcessImage(index);
        }

        public IImage Disable(int index)
        {
            if (index < 0 || index >= _nextIndex)
            {
                throw new Exception("Invalid index.");
            }

            _procedure[index].IsEnabled = true;
            return ProcessImage(index);
        }

        public IImage Enable(int index) 
        {
            if (index < 0 || index >= _nextIndex)
            {
                throw new Exception("Invalid index.");
            }

            _procedure[index].IsEnabled = false;
            return ProcessImage(index);
        }

        public IImage Undo()
        {
            IAction action = _history.undoAction();

            switch (action.Type)
            {
                case ActionEnum.Add:
                    return Delete(action.Index);
                case ActionEnum.Edit:
                    return Replace(action.Index, action.AdjustmentBefore);
                case ActionEnum.Remove:
                    return Insert(action.Index, action.Adjustment);
                default:
                    throw new Exception("Action type not valid.");
            }
            
        }

        public IImage Redo()
        {
            IAction action = _history.undoAction();
            
            switch (action.Type)
            {
                case ActionEnum.Add:
                    return Insert(action.Index, action.Adjustment);
                case ActionEnum.Edit:
                    return Replace(action.Index, action.Adjustment);
                case ActionEnum.Remove:
                    return Delete(action.Index);
                default:
                    throw new Exception("Action type not valid.");
            }
        }

        public bool IsAdjustmentEnabled(int index)
        {
            if (index < 0 || index > _nextIndex)
            {
                throw new Exception("Invalid index.");
            }

            return _procedure[index].IsEnabled;
        }

        public bool CanAdd(Tool toolType)
        {
            return _toolMap.ContainsKey(toolType);
        }

        private IImage ProcessImage(int index)
        {
            for (int i = index; i < _nextIndex; i++)
            {
                _procedure[i].StartImage = (i > 0) ? _procedure[i - 1].EndImage : _baseImage;
                _procedure[i].EndImage = _procedure[i].ImageTool.ApplyTool(_procedure[i].StartImage);
            }

            return (_nextIndex > 0) ? _procedure[_nextIndex - 1].EndImage : _baseImage;
        }

        private int FindByType(Tool toolType)
        {
            if (_toolMap.ContainsKey(toolType))
            {
                return _toolMap[toolType];
            }

            return -1;
        }
    }
}