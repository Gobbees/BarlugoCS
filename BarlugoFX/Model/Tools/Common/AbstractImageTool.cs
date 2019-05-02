using System;
using System.Collections.Generic;
using BarlugoFX.Model.ImageTools;

namespace BarlugoFX.Model.Tools.Common
{
    public abstract class AbstractImageTool : IImageTool
    {
        private IDictionary<ParameterName, IParameter<Double>> parameters = new Dictionary<ParameterName, IParameter<Double>>();

        public abstract Tool ToolType { get; }

        public void AddParameter(ParameterName name, IParameter<Double> value)
        {
            if (!IsAccepted(name))
            {
                throw new Exception("Parameter " + name + " is not correct for " + GetType());
            }
            if (!parameters.ContainsKey(name))
            {
                parameters[name] = value;
            }
            else
            {
                throw new Exception("A parameter is already present, please remove it.");
            }

        }

        public abstract IImage ApplyTool(IImage target);

        public IParameter<Double> GetParameter(ParameterName name)
        {
            return parameters[name];
        }


        public void RemoveParameter(ParameterName name)
        {
            parameters.Remove(name);
        }

        protected abstract Boolean IsAccepted(ParameterName name);

        protected double GetValueFromParameter(ParameterName name, double min, double max, double defaultValue){
            IParameter<Double> param = GetParameter(name);
            if(param == null){
                return defaultValue;
            }
            if(param.Value < min || param.Value > max)
            {
                throw new Exception("The " + name + " parameter does not respect the restrition specified by the class");
            }
            return param.Value;
        }
    }
}
