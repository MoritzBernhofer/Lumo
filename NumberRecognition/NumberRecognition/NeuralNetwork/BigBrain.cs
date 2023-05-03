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

    public void Train(Image[]? images) {
        for (int i = 0; i < images!.Length; i++) {
            float[] result = nn.Predict(images[i].ImageData);
            score += (GetValueOfHighest(result) == images[i].Value) ? 1 : 0;

            if(i % 1000  == 0) {
                Console.WriteLine($"at {i}");
            }
        }
        Console.WriteLine($"done {score}");
    }

    public void Train(Image image) {
        float[] result = nn.Predict(image.ImageData);
        if (GetValueOfHighest(result) ==  image.Value) {
            score++;
        }
    }

    private byte GetValueOfHighest(float[] array) {
        byte highest = 0;
        for (byte i = 0; i < array.Length; i++)
            if (array[i] > array[highest])
                highest = i;
        return highest;
    }
}