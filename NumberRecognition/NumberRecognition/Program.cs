using NumberRecognition.Image;
using System;
using System.IO;
namespace ReadDataFromByte;
class ZuWild {
    static void Main(string[] args) {
        Image[] images = ImageHelper.LoadImages("../../../Images Mnist.bytes", "../../../Labels Mnist.bytes");
        Console.WriteLine(images.Length);
        Console.ReadKey();
    }

}