function NewGeneration() {
    CalculateFitness();

    console.log("new generation");

    for (let i = 0; i < deadsnakes.length; i++) {
        snakes.push(pickOne());
    }

    deadsnakes = [];
}
function CalculateFitness() {
    let sum = 0;
    for (snake of deadsnakes) {
        sum += snake.score;
    }

    for (snake of deadsnakes) {
        snake.fitness = snake.score / sum;
    }
}
function pickOne() {
    let index = 0;
    let r = random(1);

    while (r > 0) {
        r -= deadsnakes[index++].fitness;
    }

    let child = new Snake(deadsnakes[index - 1].brain);
    child.brain.mutate(0.1);
    return child.brain;
}
