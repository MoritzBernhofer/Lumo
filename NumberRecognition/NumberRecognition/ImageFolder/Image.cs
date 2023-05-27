using System.Numerics;
namespace NumberRecognition.ImageFolder;
public class Image {
    public int[]? ImageData { get; }
    public double Width { get; }
    public double Height { get; }
    public int Value { get; }
    public Image(int[] data, int value, Vector2 ImageScale) {
        ImageData = data;
        Value = value;
        Width = ImageScale.X;
        Height = ImageScale.Y;
    }
}