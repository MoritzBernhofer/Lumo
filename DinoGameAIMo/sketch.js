//global varibles
let dinoImg;
let cactus1;
let cactus3;
let scale = 0.6;
let cacti = [];

//not global varibles
let slider;
let dino;
let gamecounter = 0;

function preload() {
    dinoImg = loadImage("dino.png");
    cactus1 = loadImage("cactus1.png");
    cactus3 = loadImage("cactus3.png");
}

function setup() {
    createCanvas(900, 400);
    slider = createSlider(1, 20, 1);

    dino = new Dino();
}
function keyPressed() {
    console.log(keyCode);
    if (keyCode == 32) {
        dino.jump();
    }
}
function draw() {
    drawBackground();

    dino.update();

    //drawing stuff
    //draw dinos
    dino.show();
    //draw cactus
    if (gamecounter % 500 === 0) {
        cacti.push(new Cactus());
    }
    for (cactus of cacti) {
        cactus.show();
        cactus.update();
    }
}
function drawBackground() {
    background(220);
    push();
    strokeWeight(7);
    stroke(255, 0, 0);
    line(0, height, width, height);
    pop();
}
