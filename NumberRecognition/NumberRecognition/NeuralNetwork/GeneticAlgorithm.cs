// NumberRecognition

namespace NumberRecognition.NeuralNetwork;

public static class GeneticAlgorithm {
    public static Random random = new Random(DateTime.Now.Microsecond);
    
    public static BigBrain PickOne(BigBrain[] networks) {
        int index = 0;
        float r = (float)random.NextDouble();
        while (r > 0)
            r -= networks[index++].fitness;
        BigBrain broHeGotThatSkillllllllllll = new BigBrain(networks[index - 1].nn);
        broHeGotThatSkillllllllllll.nn.Mutate(global::NeuralNetwork.LearningRate);
        return broHeGotThatSkillllllllllll;
    }

    public static void GenerateNewGeneration(BigBrain[] networks) {
        CalculateFitness(networks);
        for (int i = 0; i < networks.Length; i++) {
            networks[i] = PickOne(networks);
        }
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