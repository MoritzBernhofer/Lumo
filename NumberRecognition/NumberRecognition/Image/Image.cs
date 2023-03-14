using System.Numerics;

namespace NumberRecognition.Image;
public class Image
{
    public byte[]? ImageData { get;}
    public float Width { get; }
    public float Height { get;}
    public int Value { get; }
    public Image(byte[] data, int value, Vector2 ImageScale)
    {
        ImageData = data;
        Value = value;
        Width = ImageScale.X;
        Height = ImageScale.Y;
    }
}