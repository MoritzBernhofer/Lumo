using NumberRecognition.NeuralNetwork;

public class ActivationFunction {
    public Func<float, float> Func;
    public Func<float, float> Dfunc;

    public ActivationFunction(Func<float, float> func, Func<float, float> dfunc) {
        this.Func = func;
        this.Dfunc = dfunc;
    }
}

public class NeuralNetwork {
    private int input_nodes;
    private int hidden_nodes;
    private int output_nodes;
    private Matrix weights_ih;
    private Matrix weights_ho;
    private Matrix bias_h;
    private Matrix bias_o;
    private ActivationFunction activation_function;
    private float learning_rate;
    private float[] data;

    public NeuralNetwork(int a, int b, int c) {
        this.input_nodes = a;
        this.hidden_nodes = b;
        this.output_nodes = c;

        this.weights_ih = new Matrix(this.hidden_nodes, this.input_nodes);
        this.weights_ho = new Matrix(this.output_nodes, this.hidden_nodes);
        this.weights_ih.Randomize();
        this.weights_ho.Randomize();

        this.bias_h = new Matrix(this.hidden_nodes, 1);
        this.bias_o = new Matrix(this.output_nodes, 1);
        this.bias_h.Randomize();
        this.bias_o.Randomize();

        this.data = new float[0];

        // TODO: copy these as well
        this.learning_rate = 0.1f;
        this.activation_function = new ActivationFunction((x) => (float)(1 / (1 + Math.Exp(-x))), (y) => y * (1 - y));
    }

    public NeuralNetwork(NeuralNetwork nn) {
        this.input_nodes = nn.input_nodes;
        this.hidden_nodes = nn.hidden_nodes;
        this.output_nodes = nn.output_nodes;

        this.weights_ih = nn.weights_ih;
        this.weights_ho = nn.weights_ho;

        this.bias_h = nn.bias_h;
        this.bias_o = nn.bias_o;

        this.data = new float[0];

        this.learning_rate = 0.1f;
        this.activation_function = new ActivationFunction((x) => (float)(1 / (1 + Math.Exp(-x))), (y) => y * (1 - y));
    }
    private float GetRandomfloat(float minimum, float maximum) {
        Random random = new Random();
        return (float)random.NextDouble() * (maximum - minimum) + minimum;
    }
    public float[] Predict(float[] input_array) {
        this.data = input_array;

        Matrix inputs = Matrix.FromArray(input_array);
        Matrix hidden = Matrix.Multiply(this.weights_ih, inputs);
        hidden.Add(this.bias_h);

        hidden.Map(this.activation_function.Func);

        Matrix output = Matrix.Multiply(this.weights_ho, hidden);
        output.Add(this.bias_o);
        output.Map(this.activation_function.Func);
        
        return output.ToArray();
    }
    public void Train(float[] input_array, float[] target_array) {
        // Generating the Hidden Outputs
        Matrix inputs = Matrix.FromArray(input_array);
        Matrix hidden = Matrix.Multiply(this.weights_ih, inputs);
        hidden.Add(this.bias_h);
        // activation function!
        hidden.Map(this.activation_function.Func);

        // Generating the output's output!
        Matrix outputs = Matrix.Multiply(this.weights_ho, hidden);
        outputs.Add(this.bias_o);
        outputs.Map(this.activation_function.Func);

        // Convert array to matrix object
        Matrix targets = Matrix.FromArray(target_array);

        // Calculate the error
        // ERROR = TARGETS - OUTPUTS
        Matrix output_errors = Matrix.Subtract(targets, outputs);

        // Calculate gradient
        Matrix gradients = Matrix.Map(outputs, this.activation_function.Dfunc);
        gradients.Multiply(output_errors);
        gradients.Multiply(this.learning_rate);

        // Calculate deltas
        Matrix hidden_T = Matrix.Transpose(hidden);
        Matrix weight_ho_deltas = Matrix.Multiply(gradients, hidden_T);

        // Adjust the weights by deltas
        this.weights_ho.Add(weight_ho_deltas);
        // Adjust the bias by its deltas (which is just the gradients)
        this.bias_o.Add(gradients);

        // Calculate the hidden layer errors
        Matrix who_t = Matrix.Transpose(this.weights_ho);
        Matrix hidden_errors = Matrix.Multiply(who_t, output_errors);

        // Calculate hidden gradient
        Matrix hidden_gradient = Matrix.Map(
            hidden,
            this.activation_function.Dfunc
        );
        hidden_gradient.Multiply(hidden_errors);
        hidden_gradient.Multiply(this.learning_rate);

        // Calcuate input->hidden deltas
        Matrix inputs_T = Matrix.Transpose(hidden_errors);
        Matrix weight_ih_deltas = Matrix.Multiply(hidden_gradient, inputs_T);

        this.weights_ih.Add(weight_ih_deltas);

        this.bias_h.Add(hidden_gradient);
    }
    public NeuralNetwork Copy() {
        return new NeuralNetwork(this);
    }

    public void Mutate(float rate) {
        float Mutate(float val) {
            if ((float)new Random().NextDouble() < rate) {
                // return 2 * new Random().Nextfloat() - 1;
                return val + randomGaussian(0, 0.1f); //Assuming randomGaussian is defined elsewhere
            }
            else {
                return val;
            }
        }
        Array.ForEach(this.weights_ih, x => Mutate(x));
        Array.ForEach(this.weights_ho, x => Mutate(x));
        Array.ForEach(this.bias_h, x => Mutate(x));
        Array.ForEach(this.bias_o, x => Mutate(x));
    }
    private static Random random = new Random();
    private static float randomGaussian(float mean, float stdDev) {
        float u1 = 1.0 - random.Nextfloat(); //uniform(0,1] random floats
        float u2 = 1.0 - random.Nextfloat();
        float normal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
        return mean + stdDev * normal; //random normal(mean,stdDev^2)
    }
}