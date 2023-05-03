// NumberRecognition

namespace NumberRecognition.NeuralNetwork;

public static class GeneticAlgorithm {
    public static Random random = new Random(DateTime.Now.Microsecond);

    private static BigBrain PickOne(BigBrain[] networks) {
        int index = 0;
        float r = (float)random.NextDouble();
        while (r > 0)
            r -= networks[index++].fitness;
        BigBrain best = new(networks[index - 1].nn);
        best.nn.Mutate(global::NeuralNetwork.LearningRate);
        return best;
    }

    public static BigBrain[] GenerateNewGeneration(BigBrain[] networks) {
        BigBrain[] results = new BigBrain[networks.Length];

        CalculateFitness(networks);
        for (int i = 0; i < networks.Length; i++) {
            results[i] = new(PickOne(networks).nn);
        }

        return results;
    }

    private static void CalculateFitness(BigBrain[] networks) {
        float sum = 0;
        for (int i = 0; i < networks.Length; i++) {
            sum += networks[i].score;
        }

        for (int i = 0; i < networks.Length; i++) {
            networks[i].fitness = networks[i].score / sum;
        }
    }
}