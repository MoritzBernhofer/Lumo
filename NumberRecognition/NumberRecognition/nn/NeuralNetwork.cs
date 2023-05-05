using NumberRecognition.ImageFolder;

namespace NumberRecognition.nn;
public class NeuralNetwork {
    #region fields
    private Random random = new(DateTime.Now.Microsecond);

    private int input_Nodes;
    private int hidden_Nodes;
    private int output_Nodes;
    private volatile int score;

    private float[] hidden_Weights;
    private float[] output_Weights;
    #endregion fields

    public int Score => score;

    public NeuralNetwork(int input_Nodes, int hidden_Nodes, int output_Nodes) {
        this.input_Nodes = input_Nodes;
        this.hidden_Nodes = hidden_Nodes;
        this.output_Nodes = output_Nodes;

        hidden_Weights = NewRandomizedArray(input_Nodes);
        output_Weights = NewRandomizedArray(output_Nodes);
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
            throw new Exception("Get fucked");


        float[] inputs = Enumerable.Range(0, image.ImageData.Length)
            .Select(i => (float)i).ToArray();

        float[] hidden = Multiply(inputs, hidden_Weights);
        hidden = hidden.Zip(hidden_Weights, (a, b) => a + b).ToArray();
        hidden = Sigmoid(hidden);

        float[] output = Multiply(hidden, output_Weights);

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
}