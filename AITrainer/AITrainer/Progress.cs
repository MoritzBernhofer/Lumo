using System;
using System.Numerics;

namespace AITrainer {
    public class Progress {
        private bool[,] image;
        private int currentProgress = 0;
        private Vector2 dimensions;
        private Random rd = new();

        public Progress(Vector2 dimensions) {
            this.dimensions.X = (int)Math.Round(dimensions.X);
            this.dimensions.Y = (int)Math.Round(dimensions.Y);
            image = new bool[(int)this.dimensions.X, (int)this.dimensions.Y];

        }
        public void Update(int progressInPercent) {
            if (progressInPercent < currentProgress) return;

            int addProgress = currentProgress - progressInPercent;
            int addPixel = (int)Math.Round(dimensions.X * dimensions.Y) * (addProgress / 100);

            while(addPixel > 0 ) {
                int x = rd.Next(0, (int)dimensions.X + 1);
                int y = rd.Next(0, (int)dimensions.Y + 1);

                if (image[x, y] == false) {
                    image[x, y] = true;
                    addPixel--;
                }
            }
        }
        public bool[,] GetProgress() => image;
    }
}