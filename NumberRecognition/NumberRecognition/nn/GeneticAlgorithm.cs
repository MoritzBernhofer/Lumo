using System.Text;

namespace NumberRecognition.nn {
    public class GeneticAlgorithm {
        private NeuralNetwork[] nextGeneration;
        public NeuralNetwork[] GenerateNextGeneration(NeuralNetwork[] networks) {
            //calculatefitness(networks);
            double multiplier = 1.09;

            calculateNextGeneration(networks, multiplier);

            for (int i = 0; i < networks.Length; i++) {
                networks[i] = pickOne(nextGeneration.ToArray());
            }

            networks.ToList().ForEach(network => network.MutateNN(0.2));

            return networks;
        }

        private NeuralNetwork pickOne(NeuralNetwork[] topnetworks) {
            int index = new Random().Next(0, topnetworks.Length);
            //SerializeToCSV(topnetworks);

            return new(topnetworks[index]);
        }

        public void SerializeToCSV(NeuralNetwork[] networks) {
            StringBuilder sb = new();
            foreach(NeuralNetwork network in networks) {
                sb.AppendLine("---------------");
                //for (int i = 0; i < network.hidden_Weights.Length; i++) {
                //    sb.AppendLine($"{network.hidden_Weights[i]} {network.hidden_Bias[i]}");
                //}
                //for (int i = 0; i < network.output_Weights.Length; i++) {
                //    sb.AppendLine($"{network.output_Weights[i]} {network.output_Bias[i]}");
                //}
                foreach (var value in network.gueses) {
                    sb.Append($"{value};");
                }
                sb.AppendLine($"Score {network.Score}");
            }
            string filePath = "../../../output.csv";

            // Open the file and write the content using StreamWriter
            using (StreamWriter writer = new StreamWriter(filePath)) {
                writer.Write(sb.ToString());
            }
        }

        private void calculateNextGeneration(NeuralNetwork[] networks, double multiplier) {

            //int sum = networks.Sum(value => value.Score);
            //int avg = sum / networks.Length;

            //foreach (NeuralNetwork network in networks) {
            //    if (network.Score > avg * multiplier) {
            //        nextGeneration.Add(network);
            //    }
            //}
            nextGeneration = networks.OrderByDescending(obj => obj.Score).Take(3).ToArray();
        }

        //private NeuralNetwork pickOne(NeuralNetwork[] networks) {
        //    int index = 0;
        //    float r = (float)new Random().NextDouble();

        //    while (r > 0 && index < networks.Length) {
        //        r -= networks[index++].Fitness;
        //    }

        //    NeuralNetwork child = new NeuralNetwork(networks[index - 1].Brain);
        //    child.MutateNN(0.1); //0.1 => mutate rate
        //    return child;
        //}


        private void calculatefitness(NeuralNetwork[] networks) {
            float sum = 0;

            foreach (var network in networks) {
                sum += network.Score;
            }

            for (int i = 0; i < networks.Length; i++) {
                networks[i].Fitness = networks[i].Score / sum;
            }
        }
    }
}