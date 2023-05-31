using System;
using System.IO;
using System.Threading.Tasks;

namespace NumberRecognition.ImageFolder {
    public class ImageHelper {
        private const int ImageSize = 784;
        private const int ImageSizeSqrt = 28;
        private static Random rd = new();

        public static async Task<Image[]> LoadImagesAsync(string datapath = "../../../Images Mnist.bytes", string labelpath = "../../../Labels Mnist.bytes") {
            byte[] data = await File.ReadAllBytesAsync(datapath);
            byte[] datalabels = await File.ReadAllBytesAsync(labelpath);
            int imageCount = data.Length / ImageSize;
            Image[] images = new Image[imageCount];

            var range = Enumerable.Range(0, imageCount);

            Parallel.ForEach(range, i => {
                byte[] bytes = new byte[ImageSize];
                Array.Copy(data, i * ImageSize, bytes, 0, ImageSize);

                byte[] countArray = new byte[49]; // New count array of length 49 for 4x4 blocks

                int countIndex = 0;

                for (int c = 0; c    < 28; c += 4) {
                    for (int j = 0; j < 28; j += 4) {
                        byte count = 0;

                        for (int k = c; k < c + 4; k++) {
                            for (int l = j; l < j + 4; l++) {
                                int currentIndex = k * 28 + l;
                                byte value = bytes[currentIndex];

                                if (value < 50) {
                                    count++;
                                }
                            }
                        }

                        countArray[countIndex] = count;
                        countIndex++;
                    }
                }

                images[i] = new(countArray, datalabels[i], new System.Numerics.Vector2(28, 28));
            });

            return images;
        }
        private static void ReverseY180Deg(byte[] bytes) {
            int lastIndex = ImageSizeSqrt - 1;
            byte temp;

            for (int i = 0; i < ImageSize / 2; i++) {
                int x = i % ImageSizeSqrt;
                int y = i / ImageSizeSqrt;
                int targetX = lastIndex - x;
                int targetY = lastIndex - y;

                int targetIndex = targetY * ImageSizeSqrt + targetX;
                temp = bytes[i];
                bytes[i] = bytes[targetIndex];
                bytes[targetIndex] = temp;
            }
        }
        private static void MirrorImage(byte[] bytes) {
            int lastIndex = ImageSizeSqrt - 1;
            byte temp;

            for (int y = 0; y < ImageSizeSqrt; y++) {
                for (int x = 0; x < ImageSizeSqrt / 2; x++) {
                    int index1 = y * ImageSizeSqrt + x;
                    int index2 = y * ImageSizeSqrt + (lastIndex - x);

                    temp = bytes[index1];
                    bytes[index1] = bytes[index2];
                    bytes[index2] = temp;
                }
            }
        }
    }
}