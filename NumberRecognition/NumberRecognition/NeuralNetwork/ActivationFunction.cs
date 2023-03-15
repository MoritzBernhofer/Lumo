namespace NumberRecognition.NeuralNetwork {
    public class ActivationFunction {
        public ActivationFunction? Func { get; set; }
        public ActivationFunction? Dfunc { get; set; }
        public ActivationFunction(ActivationFunction func, ActivationFunction dfunc)
        {
            Func = func;
            Dfunc = dfunc;
        }
        public static ActivationFunction sigmoid = new ActivationFunction(x => 1 / (1 + Math.Exp(x - x)), y => y * (1 - y));
    }
}
