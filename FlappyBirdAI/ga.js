function GenerateNextGeneration() {
    calculatefitness();
    console.log("new Generation");
    for (let i = 0; i < TOTAL; i++) {
        birds[i] = pickOne();
    }
}
function pickOne() {
    let index = 0;
    let r = random(1);
    while (r > 0) {
        r = r - savedBirds[index].fitness;
        index++;
    }
    let child = new Bird(savedBirds[index - 1].brain);
    child.mutate();
    return child;
}
function calculatefitness() {
    let sum = 0;
    for (let i = 0; i < savedBirds.length; i++) {
        sum += savedBirds[i].score;
    }
    for (let i = 0; i < savedBirds.length; i++) {
        savedBirds[i].fitness = savedBirds[i].score / sum;
    }
}
