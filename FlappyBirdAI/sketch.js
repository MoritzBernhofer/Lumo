const TOTAL = 500;
let pipes = [];
let birds = [];
let savedBirds = [];
let counter = 0;
let slider;
let generation = 0;

function setup() {
    createCanvas(1040, 480);
    slider = createSlider(1, 10, 1);
    for (let i = 0; i < TOTAL; i++) {
        birds[i] = new Bird();
    }
}

function draw() {
    for (let n = 0; n < slider.value(); n++) {
        if (counter % 75 == 0) {
            pipes.push(new Pipe());
        }
        counter++;

        for (let i = pipes.length - 1; i >= 0; i--) {
            pipes[i].update();

            for (let j = birds.length - 1; j >= 0; j--) {
                if (pipes[i].hits(birds[j])) {
                    console.log("splice1");
                    savedBirds.push(birds.splice(j, 1)[0]);
                }
            }

            if (pipes[i].offscreen()) {
                pipes.splice(i, 1);
            }
        }

        for (let i = birds.length - 1; i >= 0; i--) {
            if (birds[i].offScreen()) {
                console.log("splice2");
                savedBirds.push(birds.splice(i, 1)[0]);
            }
        }

        for (let bird of birds) {
            bird.update();
            bird.think(pipes);
        }
        if (birds.length === 0) {
            generation++;
            counter = 0;
            pipes = [];
            GenerateNextGeneration();
        }
    }

    background(0);
    let bestBird = birds[0];
    for (let i = 0; i < birds.length; i++) {
        if (bestBird == null || birds[i].score > bestBird.score) {
            bestBird = birds[i];
        }
    }
    bestBird.brain.showNN(bestBird.getLastChoice());
    noStroke();
    text("Generation: " + generation, 10, 20);
    text("Best Score: " + bestBird.score, 10, 40);
    text("remaining: " + birds.length, 10, 60);

    for (let bird of birds) {
        bird.show();
    }

    for (let pipe of pipes) {
        pipe.show();
    }
}
function keyPressed() {
    if (key == " ") {
        birds[0].up();
    }
}