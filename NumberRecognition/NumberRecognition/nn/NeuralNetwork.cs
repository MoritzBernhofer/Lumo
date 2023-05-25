using MathNet.Numerics.Distributions;
using NumberRecognition.ImageFolder;
using System.IO.IsolatedStorage;

namespace NumberRecognition.nn;
public class NeuralNetwork {
    #region fields
    private Random random = new(DateTime.Now.Microsecond);
    private volatile int score;
    private double[] last;

    private int input_Nodes;
    private int hidden_Nodes;
    private int output_Nodes;

    public double[] hidden_Weights;
    public double[] output_Weights;

    public double[] hidden_Bias;
    public double[] output_Bias;

    public List<int> gueses { get; set; } = new();
    #endregion fields

    public int Score => score;
    public float Fitness { get; set; }
    public NeuralNetwork Brain => this;

    public NeuralNetwork(int input_Nodes, int hidden_Nodes, int output_Nodes) {
        this.input_Nodes = input_Nodes;
        this.hidden_Nodes = hidden_Nodes;
        this.output_Nodes = output_Nodes;

        hidden_Weights = NewRandomizedArray(input_Nodes);
        output_Weights = NewRandomizedArray(output_Nodes);

        hidden_Bias = NewRandomizedArray(hidden_Nodes);
        output_Bias = NewRandomizedArray(output_Nodes);
    }

    public NeuralNetwork(NeuralNetwork network) {
        input_Nodes = network.input_Nodes;
        hidden_Nodes = network.hidden_Nodes;
        output_Nodes = network.output_Nodes;

        hidden_Weights = (double[])network.hidden_Weights.Clone();
        output_Weights = (double[])network.output_Weights.Clone();

        hidden_Bias = (double[])network.hidden_Bias.Clone();
        output_Bias = (double[])network.output_Bias.Clone();
    }

    public void Train(Image[] images) {
        foreach (Image image in images) {
            double[] results = Predict(image);
            int index = Array.IndexOf(results, results.Max());

            if (index == image.Value)
                score++;
        }
    }
    public async Task TrainAsync(Image[] images) {
        await Task.Run(() => {
            foreach (Image image in images) {
                double[] results = Predict(image);
                int index = Array.IndexOf(results, results.Max());

                if (index == image.Value)
                    Interlocked.Increment(ref score);

                gueses.Add(index);
            }
        });
    }
    public double[] Predict(Image image) {
        if (image == null && image!.ImageData!.Length != input_Nodes)
            throw new Exception("not valid");

        double[] inputs = CloneToDoubleArray(image.ImageData);
        inputs = Preprocessing(inputs);

        double[] hidden = Multiply(hidden_Weights, inputs);
        hidden = hidden.Zip(hidden_Bias, (a, b) => a + b).ToArray();
        hidden = ProcessArrays(hidden);

        double[] output = Multiply(output_Weights, hidden);
        output = hidden.Zip(output_Bias, (a, b) => a + b).ToArray();
        output = ProcessArrays(output);

        return output;
    }

    private double[] CloneToDoubleArray(byte[]? imageData) {
        double[] data = new double[imageData!.Length];

        for (int i = 0; i < imageData.Length; i++) {
            data[i] = imageData[i];
        }
        return data;
    }
    #region Array Functions
    public double[] Preprocessing(double[] data) {
        //goal  0 => 0.8
        //everything else 0.8 value / 255 
        for (int i = 0; i < data.Length; i++) {
            if (data[i] == 0) {
                data[i] = 0.3;
            }
            else {
                data[i] = 0.5 + data[i] * 0.00392156;
            }
        }
        return data;
    }

    private double[] Multiply(double[] arr1, double[] arr2) {
        for (int i = 0; i < arr1.Length; i++) {
            for (int j = 0; j < arr2.Length; j++) {
                arr1[i] *= arr2[j];
            }
        }
        return arr1;
    }
    private double[] NewRandomizedArray(int length, int addon = 0) => Enumerable.Range(0, length)
                         .Select(_ => addon + random.NextDouble())
                         .ToArray();

    public static double[] ProcessArrays(double[] array) {
        return NormalizeData(array, 0, 1);
    }
    private static double[] NormalizeData(IEnumerable<double> data, int min, int max) {
        double dataMax = data.Max();
        double dataMin = data.Min();
        double range = dataMax - dataMin;

        return data
            .Select(d => (d - dataMin) / range)
            .Select(n => (double)((1 - n) * min + n * max))
            .ToArray();
    }
    public static double Sigmoid(double x) =>
        (1 / (1 + Math.Exp(-x)));

    #endregion Array Functions
    public void Save() {
        //write to file
    }
    public void MutateNN(double rate) {
        hidden_Weights = hidden_Weights.Select(num => Mutate(num, rate)).ToArray();
        output_Weights = output_Weights.Select(num => Mutate(num, rate)).ToArray();
        hidden_Bias = hidden_Bias.Select(num => Mutate(num, rate)).ToArray();
        output_Bias = output_Bias.Select(num => Mutate(num, rate)).ToArray();
    }

    private double Mutate(double value, double rate) {
        value += RandomGaussian(0, rate);
        return value;
    }
    private float RandomGaussian(float mean, double standardDeviation) {
        Normal normalDistribution = new Normal(mean, standardDeviation);
        return (float)normalDistribution.Sample();
    }
}