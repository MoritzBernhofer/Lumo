class NeuralNetwork {
    totalltrainings = 0;
    positions = [];
    trainIndex = 0;
    constructor(size) {
        this.w1 = random(-2, 2);
        this.w2 = random(-2, 2);
        this.w3 = random(-2, 2);
        this.w4 = random(-2, 2);
        this.learnrate = 0.01;
        for (let i = 0; i < size; i++) {
            positions.push(new Point(random(800), random(800)));
        }
    }
    trainAllNeurons() {
        for (let i = 0; i < positions.length; i++) {}
    }
    guess(x, y) {
        let sum = this.w1 * x + this.w2 * y;
        return this.activate(sum);
    }
    activate(sum) {
        return 1 / (1 + Math.exp(-sum / 2)); //Sigmund function
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
        this.guess1;
        this.guess2;
        if (x > y) {
            this.label = 1;
        } else {
            this.label = -1;
        }
    }
}
