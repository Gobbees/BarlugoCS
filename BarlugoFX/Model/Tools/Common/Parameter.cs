using System;
namespace BarlugoFX.Model.Tools.Common
{
    public class Parameter<T> : IParameter<T>
    {
        readonly T value;
        public Parameter(T value)
        {
            this.value = value;
        }
        public T Value => value;
    }
}
