using NumberRecognition.ImageFolder;

namespace NumberRecognition.NeuralNetwork;
public class NeuralNetwork {
    #region fields
    private Random random = new(DateTime.Now.Millisecond);

    private int input_Nodes;
    private int hidden_Nodes;
    private int output_Nodes;

    private float[] hidden_Weights;
    private float[] output_Weights;
    #endregion fields

    public NeuralNetwork(int input_Nodes, int hidden_Nodes, int output_Nodes) {
        this.input_Nodes = input_Nodes;
        this.hidden_Nodes = hidden_Nodes;
        this.output_Nodes = output_Nodes;

        this.hidden_Weights = NewRandomizedArray(input_Nodes);
        this.output_Weights = NewRandomizedArray(output_Nodes);


    }
    public float[] Predict(Image image) {
        if (image == null && image.ImageData.Length != input_Nodes)
            throw new Exception("Get fucked");

        float[] results = new float[image.ImageData.Length];

        float[] inputs = Enumerable.Range(0, image.ImageData.Length)
            .Select(i => (float)i).ToArray();


        float[] hidden = Multiply(inputs, hidden_Weights);
        hidden = hidden.Zip(hidden_Weights, (a, b) => a + b).ToArray();


        return results;
    }
    #region Array Functions
    private float[] Multiply(float[] arr1, float[] arr2) {
        if (arr1 == null || arr2 == null || arr1.Length != arr2.Length) {
            throw new ArgumentException("Both arrays must be non-null and have the same length.");
        }

        return arr1.Zip(arr2, (x, y) => x * y).ToArray();
    }
    private float[] NewRandomizedArray(int length) => Enumerable.Range(0, length)
                         .Select(_ => (float)random.NextDouble())
                         .ToArray();
    #endregion Array Functions
    public void Save() {
        //write to file
    }
}