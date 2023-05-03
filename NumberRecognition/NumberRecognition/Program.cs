﻿using System.Numerics;
using System.Xml;
using NumberRecognition.ConsoleFolder;
using NumberRecognition.ImageFolder;
using NumberRecognition.NeuralNetwork;

namespace NumberRecognition;
class ZuWild {

    private static PythonExecutor pythonExecutor = new(@"C:\Python27\python.exe",
        @"C:\Users\flyti\Documents\GitHubONLY\Lumo\NumberRecognition\NumberRecognition\Python\script.py");

    private static BigBrain[]? brains = new BigBrain[20];
    private static readonly int input_Nodes = 784;
    private static readonly int hidden_layers = 20;
    private static readonly int output_Nodes = 10;

    static async Task Main(string[] args) {
        // Load images asynchronously
        Image[] images = await LoadImagesAsyncWrapper();

        // Generate brains
        for (int i = 0; i < brains!.Length; i++) {
            brains[i] = new(input_Nodes, hidden_layers, output_Nodes);
        }

        // Feed brains with data
        for(int i = 0; i < 10; i++) {
            Task.Run(() => brains[i].Train(images));
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
