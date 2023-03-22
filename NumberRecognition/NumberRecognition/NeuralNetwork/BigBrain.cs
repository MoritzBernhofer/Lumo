// NumberRecognition

using System.Numerics;
using NumberRecognition.ImageFolder;

namespace NumberRecognition.NeuralNetwork;

public class BigBrain {
    public global::NeuralNetwork nn;
    public int score;
    public float fitness;

    public BigBrain(int a, int b, int c) {
        nn = new(a, b, c);
        score = 0;
    }

    public BigBrain(global::NeuralNetwork nn) {
        this.nn = nn;
    }

    public float[] Train(Image img) {
        float[] result = nn.Predict(img.ImageData);
        float sum = result.Sum();
        float[] certanty = new float[result.Length];
        if (img.Value == Program.GetValueOfHighest(certanty))
            score++;

        for (int i = 0; i < result.Length; i++)
            certanty[i] = result[i] / sum * 100;
        return result;
    }
}