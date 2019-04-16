using System;
namespace BarlugoFX.Model.Tools.Common
{
    public interface IParameter<T> //Passato a Double perche' non esiste un tipo generico Number che acc
    {
        T Value { get; }
    }
}
