using System.Numerics;
using System.Xml;
using NumberRecognition.ConsoleFolder;
using NumberRecognition.ImageFolder;
using NumberRecognition.NeuralNetwork;

namespace NumberRecognition;
class Program {
    static void Main(string[] args) {
        Image[] images = ImageHelper.LoadImages("../../../Images Mnist.bytes", "../../../Labels Mnist.bytes");
        //70k images
        BigBrain[] bigBrains = new BigBrain[200];
        for (int i = 0; i < bigBrains.Length; i++)
            bigBrains[i] = new BigBrain(784, 16, 10);
        Console.WriteLine("Hallo");
        for (int i = 0; i < images.Length; i++) {
            for (int j = 0; j < bigBrains.Length; j++) {
                bigBrains[j].Train(images[i]);
            }
            Console.WriteLine(i);
        }

        
        int counter = 0;
        char input = ' ';
        while (input != 'n') {
            foreach (var bigBrain in bigBrains)
                bigBrain.Train(images[counter]);
            ConsoleHelper.PrintImageToConsole(images[counter++]);
            Console.Clear();
            input = Convert.ToChar(Console.ReadKey());
        }
    }

    public static byte GetValueOfHighest(float[] array) {
        byte highest = 0;
        for (byte i = 0; i < array.Length; i++)
            if (array[i] > array[highest]) 
                highest = i;
        return highest;
    }
}