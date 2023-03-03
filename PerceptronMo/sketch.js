let pointCreationHeight;
let pointCreationWidth;
let brain;
let learninghistory = [];

function setup() {
    createCanvas(400, 800);
    pointCreationHeight = height - 400;
    pointCreationWidth = width;
    brain = new NeuralNetwork(pointCreationWidth, pointCreationHeight, 500);
}

function draw() {
    background(155);
    push();
    strokeWeight(5);
    stroke(255, 255, 0);
    line(0, 0, 400, 400);
    pop();

    push();
    translate(0, 400);
    textSize(32);
    text("Total Trainings: " + brain.totallTrainings + "\n", 10, 30);
    text("Correctness: " + brain.correctness() + "%", 10, 60);
    text("Learnrate: " + brain.learnrate, 10, 90);
    text("Sample Size: " + positions.length, 10, 120);
    pop();
    push();

    push();
    translate(0, 750);
    fill(255, 0, 0);
    textSize(20);
    text("100%", 40, -105);
    line(0, -100, 400, -100);
    noFill();
    beginShape();
    for (let i = 0; i < learninghistory.length; i++) {
        if (learninghistory[i] === 100) {
            stroke(255, 0, 0);
        } else {
            stroke(255);
        }
        strokeWeight(3);
        vertex(i, learninghistory[i] * -1);
    }
    endShape();
    pop();
    learninghistory.push(brain.correctness());
    if (learninghistory.length > 350) {
        learninghistory.shift();
    }
    brain.show();
}
function mousePressed() {
    brain.trainAllNeurons();
}
