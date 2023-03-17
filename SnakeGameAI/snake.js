class Snake {
    constructor(brain = null) {
        this.x = floor(random(w));
        this.y = floor(random(h));
        this.xdir = 1;
        this.ydir = 0;
        this.total = 0;
        this.body = [];
        this.body[0] = createVector(this.x, this.y);

        this.oldheadposition = createVector(this.x, this.y);

        this.score = 0;
        this.fitness = 0;

        if (brain !== null) {
            this.brain = brain.copy();
        } else {
            this.brain = new NeuralNetwork(7, 14, 4);
        }
    }
    stDirection(x, y) {
        this.xdir = x;
        this.ydir = y;
    }
    update(foodvector) {
        //check if dead
        if (this.IsDead()) {
            deadsnakes.push(this);
            deadsnakes.splice(deadsnakes.indexOf(this), 1);
        }
        //check if eat
        let x = this.body[this.body.length - 1].x;
        let y = this.body[this.body.length - 1].y;
        if (x == foodX && y == foodY) {
            this.score += 1000;
            this.grow();
        }
        //mode head forward
        let head = this.body[this.body.length - 1].copy();
        this.body.shift();

        head.x += this.xdir;
        head.y += this.ydir;
        this.body.push(head);

        //ask brain what to do
        this.think(foodvector);

        this.score++;
    }

    IsDead() {
        let x = this.body[this.body.length - 1].x;
        let y = this.body[this.body.length - 1].y;
        //check if out of bounds
        if (x > w - 1 || x < 0 || y > h - 1 || y < 0) {
            console.log("out of bounds");
            return true;
        }
        //check if it hits itself in the head
        if (this.oldheadposition.x == x && this.oldheadposition.y == y) {
            console.log("hit itself in the head");
            return true;
        }

        //check if hit itself
        for (let i = 0; i < this.body.length - 1; i++) {
            let part = this.body[i];
            if (part.x == x && part.y == y) {
                console.log("hit itself");
                return true;
            }
        }
        return false;
    }
    grow() {
        let head = this.body[this.body.length - 1].copy();
        this.total++;
        this.body.push(head);
    }
    think(foodvector) {
        let inputs = [];
        //head x,y
        inputs[0] = this.body[this.body.length - 1].x / w;
        inputs[1] = this.body[this.body.length - 1].y / h;
        //food x,y
        inputs[2] = foodvector.x / w;
        inputs[3] = foodvector.x / h;
        //length
        inputs[4] = this.total;
        //direction
        inputs[5] = this.xdir;
        inputs[6] = this.ydir;

        let outputs = this.brain.predict(inputs);

        //forward
        if (
            outputs[0] > outputs[1] &&
            outputs[0] > outputs[2] &&
            outputs[0] > outputs[3]
        ) {
            this.stDirection(0, 1);
        } else if (
            outputs[1] > outputs[0] &&
            outputs[1] > outputs[2] &&
            outputs[1] > outputs[3]
        ) {
            this.stDirection(0, -1);
        } else if (
            outputs[2] > outputs[0] &&
            outputs[2] > outputs[1] &&
            outputs[2] > outputs[3]
        ) {
            this.stDirection(-1, 0);
        } else if (
            outputs[3] > outputs[0] &&
            outputs[3] > outputs[1] &&
            outputs[3] > outputs[2]
        ) {
            this.stDirection(1, 0);
        }
    }
    show() {
        //draw food
        noStroke();
        fill(255, 0, 0);
        rect(this.foodX, this.foodY, 1, 1);

        //draw snake
        for (let i = 0; i < this.body.length; i++) {
            fill(255);
            noStroke();
            rect(this.body[i].x, this.body[i].y, 1, 1);
        }
    }
}
