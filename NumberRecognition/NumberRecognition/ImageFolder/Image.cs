using System.Numerics;
namespace NumberRecognition.ImageFolder;
public class Image {
    public float[]? ImageData { get; }
    public float Width { get; }
    public float Height { get; }
    public int Value { get; }
    public Image(float[] data, int value, Vector2 ImageScale) {
        ImageData = data;
        Value = value;
        Width = ImageScale.X;
        Height = ImageScale.Y;
    }
}