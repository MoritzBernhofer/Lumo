using NumberRecognition.ConsoleFolder;
using NumberRecognition.ImageFolder;
using NumberRecognition.nn;

namespace NumberRecognition;
class ZuWild {

    private static PythonExecutor pythonExecutor = new(@"C:\Python27\python.exe",
        @"C:\Users\flyti\Documents\GitHubONLY\Lumo\NumberRecognition\NumberRecognition\Python\script.py");

    private static int generationsCount = 1000;
    private static int sampleSize = 200;
    private static int imageInputCount = 200;

    private static NeuralNetwork[]? networks = new NeuralNetwork[sampleSize];
    private static GeneticAlgorithm geneticAlgorithm = new();
    private static readonly int input_Nodes = 784;
    private static readonly int hidden_layers = 1000;
    private static readonly int output_Nodes = 10;
    private static double highest = double.MinValue; 

    static async Task Main(string[] args) {
        // Load images asynchronously
        Image[] images = await LoadImagesAsyncWrapper();

        // Generate brains
        for (int i = 0; i < networks.Length; i++) {
            networks[i] = new(input_Nodes, hidden_layers, output_Nodes);
        }

        // Feed brains with data

        var firstrun = new List<Task>();
        for (int i = 0; i < networks.Length; i++) {
            firstrun.Add(networks[i].TrainAsync(GetRandomImages(images, imageInputCount)));
        }
        await Console.Out.WriteLineAsync("started succesfully");
        await Task.WhenAll(firstrun);

        DisplayNetworkScores(networks);

        await Console.Out.WriteLineAsync("First run Complete");
        geneticAlgorithm.SerializeToCSV(networks);
        networks = geneticAlgorithm.GenerateNextGeneration(networks);
        await Console.Out.WriteLineAsync($"Running {generationsCount} Generations");
        Console.ReadKey();

        for (int i = 0; i < generationsCount; i++) {
            var tasks = new List<Task>();

            for (int a = 0; a < networks.Length; a++) {
                tasks.Add(networks[a].TrainAsync(GetRandomImages(images, imageInputCount)));
            }

            await Task.WhenAll(tasks);
            await Console.Out.WriteLineAsync($"Run: {i}");

            //int sum = networks.Sum(value => value.Score);
            //double percent = (((double)sum / (double)networks.Length) / imageInputCount) * 100;

            //if (percent > highest)
            //    highest = percent;

            //await Console.Out.WriteLineAsync($"top: {highest:f2}");

            DisplayNetworkScores(networks);


            if (i != generationsCount - 1) {
                networks = geneticAlgorithm.GenerateNextGeneration(networks);
            }
        }

        DisplayNetworkScores(networks);

        Console.ReadKey();
        Console.ReadKey();
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

    private static Image[] GetRandomImages(Image[] images, int length) {
        //int value = new Random()
        //   .Next(0, Math.Min((int)Math.Round(images.Length / 1000.0) * 1000, length));
        ////value from 77k to 0

        //return images.Skip(images.Length - value).Take(length).ToArray();
        return images.Take(length).ToArray();
    }

    private static async Task<Image[]> LoadImagesAsyncWrapper() {
        Console.WriteLine("Loading images...");
        Image[] images = await ImageHelper.LoadImagesAsync();
        Console.WriteLine("Images loaded successfully.");
        return images;
    }
    private static void DisplayNetworkScores(NeuralNetwork[] networks) {
        int sum = 0;
        int highest = Int32.MinValue;

        foreach (NeuralNetwork network in networks) {
            Console.Write(network.Score + "**");
            sum += network.Score;

            if (network.Score > highest) {
                highest = network.Score;
            }
        }
        Console.WriteLine();
        Console.WriteLine($"Highest Score: {highest}, Avg. Score: {(double)sum / (double)networks.Length:f3}");
        Console.WriteLine($"% in Score: {(((double)sum / (double)networks.Length) / imageInputCount) * 100:f2} %");
    }

}