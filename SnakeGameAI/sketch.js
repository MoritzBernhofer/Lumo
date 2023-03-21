let snakes = [];
let deadsnakes = [];
let rez = 25;
let w;
let h;
let pageRef;
let TOTALL = 500;
let counter = 0;
let slider;

//one food
let foodX;
let foodY;

function setup() {
    canvasRef = createCanvas(600, 600);
    w = floor(width / rez);
    h = floor(height / rez);

    for (let i = 0; i < TOTALL; i++) {
        snakes.push(new Snake());
    }

    slider = createSlider(1, 10, 1);

    pageRef = select("body");
    pageRef.style("background-color", "rgba(11, 127, 171, 1)");

    foodX = floor(random(w));
    foodY = floor(random(h));
}

function draw() {
    scale(rez);
    background(0);

    for (let n = 0; n < slider.value(); n++) {
        for (snake of snakes) {
            snake.update(createVector(foodX, foodY));
        }
        counter++;
    }

    for (snake of snakes) {
        snake.show();
    }

    noStroke();
    fill(255, 0, 0);
    rect(foodX, foodY, 1, 1);
}
