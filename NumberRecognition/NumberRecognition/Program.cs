using System.Numerics;
using System.Xml;
using NumberRecognition.ConsoleFolder;
using NumberRecognition.ImageFolder;
using NumberRecognition.NeuralNetwork;

namespace NumberRecognition;
class ZuWild {

    private static PythonExecutor pythonExecutor = new(@"C:\Python27\python.exe",
        @"C:\Users\flyti\Documents\GitHubONLY\Lumo\NumberRecognition\NumberRecognition\Python\script.py");

    static async Task Main(string[] args) {
        Image[] images = await ImageHelper.LoadImagesAsync();


        Console.WriteLine(pythonExecutor.Execute(10));

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