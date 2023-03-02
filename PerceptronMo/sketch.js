let pointCreationHeight;
let pointCreationWidth;
let brain;

function setup() {
    createCanvas(400, 400);
    pointCreationHeight = height - 50;
    pointCreationWidth = width;
    brain = new NeuralNetwork(pointCreationWidth, pointCreationHeight, 500);
}

function draw() {
    line(0, 0, width, height);
    background(155);
    brain.show();
}
function mousePressed() {
    brain.train10Times();
}
