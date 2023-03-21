class Dino {
    constructor(brain = null) {
        this.y = 0;

        this.lift = 150;

        if (brain) {
            this.brain = brain.copy();
        } else {
            //this.brain = new NeuralNetwork(4, 4, 2);
        }
    }
    update() {
        //ckeck for collision

        //update postion
        if (this.y > 0) {
            this.y -= 4;
            if (this.y < 0) {
                this.y = 0;
            }
        }
        //think
        let inputs = [];
    }
    show() {
        image(
            dinoImg,
            0,
            height - dinoImg.height * scale - this.y,
            dinoImg.width * scale,
            dinoImg.height * scale
        );
    }
    jump() {
        if (this.y == 0) {
            this.y += this.lift;
        }
    }
}
