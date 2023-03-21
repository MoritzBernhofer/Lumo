class Cactus {
    constructor() {
        this.y = 0;
        this.x = width;
        this.cactusImg = random([cactus1, cactus3]);
    }

    show() {
        image(
            this.cactusImg,
            this.x,
            height * scale - this.y,
            cactus.width * scale,
            cactus.height * scale
        );
    }

    update() {
        this.x -= 5;
    }
}
