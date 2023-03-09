positions = [];
trainIndex = 0;

class NeuralNetwork {
    constructor(RandomHeightMax, RandomWidthMax, size) {
        this.w1 = random(-1, 1);
        this.w2 = random(-1, 1);
        this.totallTrainings = 0;
        this.learnrate = 0.01;
        for (let i = 0; i < size; i++) {
            positions.push(
                new Point(random(RandomHeightMax), random(RandomWidthMax))
            );
        }
    }

    guess(x, y) {
        let valuev = this.w1 * x + this.w2 * y;
        if (valuev >= 0) {
            return 1;
        } else {
            return -1;
        }
    }
    correctness() {
        let correct = 0;
        for (let i = 0; i < positions.length; i++) {
            if (positions[i].guess == positions[i].label) {
                correct++;
            }
        }
        return round((correct / positions.length) * 100, 2);
    }
    trainAllNeurons() {
        for (let i = 0; i < positions.length; i++) {
            let x = positions[i].x;
            let y = positions[i].y;

            let label = positions[i].label;
            positions[i].guess = this.guess(x, y);
            let error = label - positions[i].guess;

            this.w1 += error * x * this.learnrate;
            this.w2 += error * y * this.learnrate;

            this.totallTrainings++;
        }
    }
    train10Times() {
        for (let i = 0; i < 10; i++) {
            let x = positions[trainIndex].x;
            let y = positions[trainIndex].y;

            let label = positions[trainIndex].label;
            positions[trainIndex].guess = this.guess(x, y);

            let error = label - positions[trainIndex].guess;
            this.w1 += error * x * this.learnrate;
            this.w2 += error * y * this.learnrate;

            trainIndex++;
            this.totallTrainings++;
            if (trainIndex == positions.length) {
                trainIndex = 0;
            }
        }
    }

    show() {
        push();
        stroke(0);
        for (let i = 0; i < positions.length; i++) {
            if (positions[i].guess == 1) {
                fill(0, 0, 255);
            } else {
                fill(0, 255, 0);
            }
            ellipse(positions[i].x, positions[i].y, 8, 8);
        }
        pop();
    }
}
class Point {
    constructor(x, y) {
        this.x = x;
        this.y = y;
        this.guess;
        if (x > y) {
            this.label = 1;
        } else {
            this.label = -1;
        }
    }
}
