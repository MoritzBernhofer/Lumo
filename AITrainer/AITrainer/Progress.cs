using System;
using System.Drawing;
using System.Numerics;

namespace AITrainer {
    public class Progress {
        private bool[,] image;
        private int fillratio = 0;
        private RandomFilledArray array;

        public Progress(int x,int y) {
            image = new bool[x,y];
            array = new(x, y, 0);
        }
        public void Update(int addfillratio) {
            if (fillratio + addfillratio > 100)
                return;

            fillratio += addfillratio;
            array.AddFillRatio(addfillratio);
        }
        public Bitmap GetProgress() {
            if (array.X == 0 || array.Y == 0) 
                throw new Exception();

            Bitmap map = new(array.X,array.Y);

            for (int i = 0; i < array.X; i++) {
                for (int j = 0; j < array.Y; j++) {
                    if (image[i, j])
                        map.SetPixel(i, j, Color.FromArgb(32, 194, 14));
                }
            }

            return map;
        }
    }
}