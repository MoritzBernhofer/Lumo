let pointCreationHeight;
let pointCreationWidth;
let brain;

function setup() {
    createCanvas(400, 400);
    pointCreationHeight = height - 50;
    pointCreationWidth = width;
    brain = new NeuralNetwork(pointCreationHeight, pointCreationWidth);
}

function draw() {
    background(155);
    brain.show();
}
function mousePressed() {
    brain.trainAllNeurons();
}
