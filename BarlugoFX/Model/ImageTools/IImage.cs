namespace BarlugoFX.Model.ImageTools
{
    public interface IImage
    {
        int[,] ImageRGB { get; }
        int Width { get; }
        int Height { get; }
    }
}
