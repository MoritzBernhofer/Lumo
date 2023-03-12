class Bird {
    constructor(brain) {
        this.y = height / 2;
        this.x = 64;

        this.gravity = 0.8;
        this.lift = -12;
        this.velocity = 0;

        this.score = 0;
        this.fitness = 0;

        this.decision = [];
        if (brain) {
            this.brain = brain.copy();
        } else {
            this.brain = new NeuralNetwork(4, 8, 2);
        }
    }
    think(pipes) {
        let closest = null;
        let closestD = Infinity;
        for (let i = 0; i < pipes.length; i++) {
            let d = pipes[i].x + pipes[i].w - this.x;
            if (d < closestD && d > 0) {
                closest = pipes[i];
                closestD = d;
            }
        }
        let nnInputs = [];
        nnInputs[0] = this.y / height;
        nnInputs[1] = closest.top / height;
        nnInputs[2] = closest.bottom / height;
        nnInputs[3] = closest.x / width;
        //nnInputs[4] = this.velocity / 10;

        let nnOutput = this.brain.predict(nnInputs);
        this.decision = nnOutput;

        if (nnOutput[0] > nnOutput[1]) {
            this.up();
        }
    }
    getLastChoice() {
        return this.decision;
    }
    show() {
        stroke(255);
        fill(255, 100);
        ellipse(this.x, this.y, 32, 32);
    }

    up() {
        this.velocity = 0;
        this.velocity += this.lift;
    }

    mutate() {
        this.brain.mutate(0.1);
    }

    offScreen() {
        return this.y > height || this.y < 0;
    }

    update() {
        this.score++;

        this.velocity += this.gravity;
        //this.velocity *= 0.9;
        this.y += this.velocity;
    }
}
