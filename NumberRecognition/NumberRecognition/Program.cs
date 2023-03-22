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

        BigBrain[] bigBrains = new BigBrain[2];
        for (int i = 0; i < bigBrains.Length; i++)
            bigBrains[i] = new BigBrain(784, 16, 10);

        Console.WriteLine("Starting tasks");

        List<Task> tasks = new();

        DateTime now = DateTime.Now;
        for (int i = 0; i < bigBrains.Length; i++) {
            Console.WriteLine(i);
            tasks.Add(new Task(() => { bigBrains[i].Train(images, i); }));
            tasks[i].Start();
        }

        Console.ReadKey();
        Console.WriteLine(DateTime.Now - now);

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
}