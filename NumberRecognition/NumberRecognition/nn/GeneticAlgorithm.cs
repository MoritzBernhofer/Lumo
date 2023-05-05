using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NumberRecognition.nn {
    public class GeneticAlgorithm {
        public NeuralNetwork[] GenerateNextGeneration(NeuralNetwork[] networks) {
            calculatefitness();
            for (int i = 0; i < TOTAL; i++) {
                birds[i] = pickOne();
            }
            throw new NotFiniteNumberException();
        }
        function pickOne() {
            let index = 0;
            let r = random(1);
            while (r > 0) {
                r = r - savedBirds[index].fitness;
                index++;
            }
            let child = new Bird(savedBirds[index - 1].brain);
            child.mutate();
            return child;
        }
        function calculatefitness() {
            let sum = 0;
            for (let i = 0; i < savedBirds.length; i++) {
                sum += savedBirds[i].score;
            }
            for (let i = 0; i < savedBirds.length; i++) {
                savedBirds[i].fitness = savedBirds[i].score / sum;
            }
        }

    }
}