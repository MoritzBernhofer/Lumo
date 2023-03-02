class NeuralNetwork {
    points = [];
    learnrate = 0.01;
    trainIndex = 0;

    constructor(RandomHeightMax, RandomWidthMax) {
        w1 = random(-1, 1);
        w2 = random(-1, 1);
        for (let i = 0; i < 100; i++) {
            points.push(
                new Point(random(RandomHeightMax), random(RandomWidthMax))
            );
        }
    }
    guess(x, y) {
        let value = w1 * x + w2 * y;
        if (value >= 0) {
            return 1;
        } else {
            return -1;
        }
    }

    trainAllNeurons() {
        for (let i = 0; i < points.length; i++) {
            let x = points[i].x;
            let y = points[i].y;

            let label = points[i].label;
            let guess = guess(x, y);
            let error = label - guess;

            w1 += error * x * learnrate;
            w2 += error * y * learnrate;
        }
    }
    trainSingleNeuron() {
        console.log("training");
        let x = points[trainIndex].x;
        let y = points[trainIndex].y;

        let label = points[trainIndex].label;
        let guess = guess(x, y);

        let error = label - guess;
        w1 += error * x * learnrate;
        w2 += error * y * learnrate;

        trainIndex++;
        if (trainIndex == points.length) {
            trainIndex = 0;
        }
    }
    show() {
        push();
        stroke(0);
        for (let i = 0; i < points.length; i++) {
            if (points[i].label == 1) {
                fill(0, 0, 255);
            } else {
                fill(0, 255, 0);
            }
            ellipse(points[i].x, points[i].y, 8, 8);
        }
        pop();
    }
}
class Point {
    Point(x, y) {
        this.x = x;
        this.y = y;
        if (x > y) {
            this.label = 1;
        } else {
            this.label = -1;
        }
    }
}
