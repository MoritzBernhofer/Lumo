using System.Numerics;
using System.Xml;
using NumberRecognition.ConsoleFolder;
using NumberRecognition.ImageFolder;
using NumberRecognition.NeuralNetwork;

namespace NumberRecognition;
class ZuWild {

    private static PythonExecutor pythonExecutor = new(@"C:\Python27\python.exe",
        @"C:\Users\flyti\Documents\GitHubONLY\Lumo\NumberRecognition\NumberRecognition\Python\script.py");

    private static BigBrain[]? Brains = new BigBrain[20];
    private static readonly int input_Nodes = 784;
    private static readonly int hidden_layers = 20;
    private static readonly int output_Nodes = 10;


    static async Task Main(string[] args) {
        Image[] images = await ImageHelper.LoadImagesAsync();

        //generate brains
        for (int i = 0; i < Brains!.Length; i++) {
            Brains[i] = new(input_Nodes, hidden_layers, output_Nodes);
        }

        //feed brains with data

        foreach(BigBrain brain in Brains) {
            await Task.Run(() => brain.Train(images));
        }


        Console.ReadKey();

        int counter = 0;
        while (counter < images.Length) {
            Console.Clear();
            ConsoleHelper.PrintImageToConsole(images[counter]);
            Console.WriteLine("\nPress any key to display the next image...");
            Console.ReadKey();
            counter++;


        }
    }
}