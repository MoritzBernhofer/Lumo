using System;
using System.Drawing;
using System.Numerics;

namespace AITrainer {
    public class Progress {
        private bool[,] image;
        private int currentProgress = 0;
        private int x;
        private int y;
        private Random rd = new();

        public Progress(int x,int y) {
            this.x = x;
            this.y = y;
            image = new bool[x,y];

        }
        public void Update(int progressInPercent) {
            if (progressInPercent < currentProgress) return;

            int addProgress =  progressInPercent - currentProgress;

            if(addProgress + currentProgress > 100)
                addProgress = 100 - addProgress;

            int addPixel = (int)Math.Round((double)(x*y) * ((double)addProgress / (double)100));

            while(addPixel > 0 ) {
                int xcord = rd.Next(0, x);
                int ycord = rd.Next(0, y);

                if (image[xcord, ycord] == false) {
                    image[xcord, ycord] = true;
                    addPixel--;
                }
            }
        }
        public Bitmap GetProgress() {
            if (x == 0 || y == 0) throw new Exception();

            Bitmap map = new(x,y);

            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++) {
                    if (image[i, j])
                        map.SetPixel(i, j, Color.FromArgb(32, 194, 14));
                }
            }

            return map;
        }
    }
}