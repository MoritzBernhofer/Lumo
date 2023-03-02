class NeuralNetwork {
    points = [];
    learnrate = 0.01;
    trainIndex = 0;

    NeuralNetwork(RandomHeightMax, RandomWidthMax) {
        this.w1 = random(-1, 1);
        this.w2 = random(-1, 1);
        for (let i = 0; i < 100; i++) {
            this.points.push(
                new Point(random(RandomHeightMax), random(RandomWidthMax))
            );
        }
    }
    guess(x, y) {
        let value = this.w1 * x + this.w2 * y;
        if (n >= 0) {
            return 1;
        } else {
            return -1;
        }
    }

    trainAllNeurons() {
        for (let i = 0; i < this.points.length; i++) {
            let x = this.points[i].x;
            let y = this.points[i].y;

            let label = this.points[i].label;
            let guess = guess(x, y);
            let error = label - guess;

            this.w1 += error * x * this.learnrate;
            this.w2 += error * y * this.learnrate;
        }
    }
    trainSingleNeuron() {
        let x = this.points[this.trainIndex].x;
        let y = this.points[this.trainIndex].y;

        let label = this.points[this.trainIndex].label;
        let guess = guess(x, y);

        let error = label - guess;
        this.w1 += error * x * this.learnrate;
        this.w2 += error * y * this.learnrate;

        this.trainIndex++;
        if (this.trainIndex == this.points.length) {
            this.trainIndex = 0;
        }
    }
    show() {
        push();
        for (let i = 0; i < this.points.length; i++) {
            stroke(0);
            if (this.points[i].label == 1) {
                fill(0, 0, 255);
            } else {
                fill(0, 255, 0);
            }
            ellipse(this.points[i].x, this.points[i].y, 8, 8);
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
