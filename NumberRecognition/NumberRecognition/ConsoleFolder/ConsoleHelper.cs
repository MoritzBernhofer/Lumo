using NumberRecognition.ImageFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberRecognition.ConsoleFolder;
public class ConsoleHelper {
    private static readonly string density =
    "@#W$8976543210?!abc+;:=-,._°^`    ";
    private static int imageSize = 784;
    public static void PrintImageToConsole(Image img) {
        int counter = 0;
        while (counter < imageSize) {
            double charindex = Math.Floor(map(img.ImageData![counter++], 0, 255, density.Length - 1, 0));
            Console.Write(density[(int)charindex]);
            if (counter % img.Width == 0) {
                Console.WriteLine();
            }
        }
        Console.WriteLine();
        Console.WriteLine(img.Value);


    }
    private static float map(float value, float leftMin, float leftMax, float rightMin, float rightMax) {
        return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
    }
}
