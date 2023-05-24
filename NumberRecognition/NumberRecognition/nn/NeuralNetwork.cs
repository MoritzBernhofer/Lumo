﻿using MathNet.Numerics.Distributions;
using NumberRecognition.ImageFolder;

namespace NumberRecognition.nn;
public class NeuralNetwork {
    #region fields
    private Random random = new(DateTime.Now.Microsecond);
    private volatile int score;

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

        double[] inputs = Enumerable.Range(0, image.ImageData!.Length)
            .Select(i => (double)i).ToArray();

        double[] hidden = Multiply(inputs, hidden_Weights);
        hidden = hidden.Zip(hidden_Bias, (a, b) => a + b).ToArray();
        hidden = Sigmoid(hidden);

        double[] output = Multiply(hidden, output_Weights);
        output = hidden.Zip(output_Bias, (a, b) => a + b).ToArray();
        output = Sigmoid(output);

        return output;
    }
    #region Array Functions
    private double[] Multiply(double[] arr1, double[] arr2) {
        return arr1.Zip(arr2, (a, b) => a * b).ToArray();
    }
    private double[] NewRandomizedArray(int length) => Enumerable.Range(0, length)
                         .Select(_ => random.NextDouble())
                         .ToArray();

    public static double[] Sigmoid(double[] array) {
        double[] result = new double[array.Length];
        for (int i = 0; i < array.Length; i++) {
            result[i] = Sigmoid(array[i]);
        }
        return result;
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