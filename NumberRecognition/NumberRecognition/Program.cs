using System.Numerics;
using System.Xml;
using NumberRecognition.ConsoleFolder;
using NumberRecognition.ImageFolder;
using NumberRecognition.nn;

namespace NumberRecognition;
class ZuWild {

    private static PythonExecutor pythonExecutor = new(@"C:\Python27\python.exe",
        @"C:\Users\flyti\Documents\GitHubONLY\Lumo\NumberRecognition\NumberRecognition\Python\script.py");

    private static NeuralNetwork[]? networks = new NeuralNetwork[20];
    private static readonly int input_Nodes = 784;
    private static readonly int hidden_layers = 20;
    private static readonly int output_Nodes = 10;

    static async Task Main(string[] args) {
        // Load images asynchronously
        Image[] images = await LoadImagesAsyncWrapper();

        // Generate brains
        for (int i = 0; i < 1; i++) {
            networks[i] = new(input_Nodes, hidden_layers, output_Nodes);
        }

        // Feed brains with data
        foreach(NeuralNetwork network in networks) {
            network.Train(images.Take(1000).ToArray());
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

    private static async Task<Image[]> LoadImagesAsyncWrapper() {
        Console.WriteLine("Loading images...");
        Image[] images = await ImageHelper.LoadImagesAsync();
        Console.WriteLine("Images loaded successfully.");
        return images;
    }

}
