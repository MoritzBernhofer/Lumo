using System;
using System.IO;
using System.Threading.Tasks;

namespace NumberRecognition.ImageFolder {
    public class ImageHelper {
        private const int ImageSize = 784;
        private const int ImageSizeSqrt = 28;

        public static async Task<Image[]> LoadImagesAsync(string datapath = "../../../Images Mnist.bytes", string labelpath = "../../../Labels Mnist.bytes") {
            byte[] data = await File.ReadAllBytesAsync(datapath);
            byte[] datalabels = await File.ReadAllBytesAsync(labelpath);
            int imageCount = data.Length / ImageSize;
            Image[] images = new Image[imageCount];

            var range = Enumerable.Range(0, imageCount);

            Parallel.ForEach(range, i => {
                byte[] bytes = new byte[ImageSize];
                Array.Copy(data, i * ImageSize, bytes, 0, ImageSize);
                MirrorImage(bytes);
                ReverseY180Deg(bytes);

                for (int y = 0; y < bytes.Length; y++) {
                    if(bytes[y] == 0 ) {
                        bytes[y] += 2;
                    }
                }

                images[i] = new(bytes, datalabels[i], new System.Numerics.Vector2(28, 28));
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