using MathNet.Numerics.Distributions;
using NumberRecognition.ImageFolder;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NumberRecognition.nn;
public class NeuralNetwork {
    #region fields
    private Random random = new(DateTime.Now.Microsecond);
    private volatile int score;

    private int input_Nodes;
    private int hidden_Nodes;
    private int output_Nodes;

    public float[] hidden_Weights;
    public float[] output_Weights;

    public float[] hidden_Bias;
    public float[] output_Bias;
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

        hidden_Weights = (float[])network.hidden_Weights.Clone();
        output_Weights = (float[])network.output_Weights.Clone();

        hidden_Bias = (float[])network.hidden_Bias.Clone();
        output_Bias = (float[])network.output_Bias.Clone();
    }

    public void Train(Image[] images) {
        foreach (Image image in images) {
            float[] results = Predict(image);
            int index = Array.IndexOf(results, results.Max());

            if (index == image.Value)
                score++;
        }
    }
    public async Task TrainAsync(Image[] images) {
        await Task.Run(() => {
            foreach (Image image in images) {
                float[] results = Predict(image);
                int index = Array.IndexOf(results, results.Max());

                if (index == image.Value)
                    Interlocked.Increment(ref score);
            }
        });
    }
    public float[] Predict(Image image) {
        if (image == null && image.ImageData.Length != input_Nodes)
            throw new Exception("not valid");

        float[] inputs = Enumerable.Range(0, image.ImageData.Length)
            .Select(i => (float)i).ToArray();

        float[] hidden = Multiply(inputs, hidden_Weights);
        hidden = hidden.Zip(hidden_Bias, (a, b) => a + b).ToArray();
        hidden = Sigmoid(hidden);

        float[] output = Multiply(hidden, output_Weights);
        output = hidden.Zip(output_Bias, (a, b) => a + b).ToArray();
        //output = Sigmoid(output);

        return output;
    }
    #region Array Functions
    private float[] Multiply(float[] arr1, float[] arr2) {
        return arr1.Zip(arr2, (a, b) => a * b).ToArray();
    }
    private float[] NewRandomizedArray(int length) => Enumerable.Range(0, length)
                         .Select(_ => (float)random.NextDouble())
                         .ToArray();
    public static float[] Sigmoid(float[] array) {
        float[] result = new float[array.Length];
        for (int i = 0; i < array.Length; i++) {
            result[i] = Sigmoid(array[i]);
        }
        return result;
    }
    public static float Sigmoid(float x) =>
        (float)(1 / (1 + Math.Exp(-x)));

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

    private float Mutate(float value, double rate) {
        value += RandomGaussian(0, rate);
        return value;
    }
    private float RandomGaussian(float mean, double standardDeviation) {
        Normal normalDistribution = new Normal(mean, standardDeviation);
        return (float)normalDistribution.Sample();
    }
}