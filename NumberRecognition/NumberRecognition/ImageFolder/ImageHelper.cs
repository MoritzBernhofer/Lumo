using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;

namespace NumberRecognition.ImageFolder {
    public class ImageHelper {
        private static int imageSize = 784;
        private static int imageSizeSqrt = (int)Math.Sqrt(imageSize);
        private static int[] byteNumbers = { 128, 64, 32, 16, 8, 4, 2, 1 };

        public static Image[] LoadImages(string datapath = "../../../Images Mnist.bytes", string labelpath = "../../../Labels Mnist.bytes") {
            byte[] data = File.ReadAllBytes(datapath);
            var datalabels = File.ReadAllBytes(labelpath);
            Image[] images = new Image[data.Length / imageSize];

            int counter = 0;
            for (int i = 0; i < data.Length / imageSize; i++) {
                float[] bytes = new float[imageSize];

                for (int j = 0; j < bytes.Length; j++)
                    bytes[j] = data[counter++];

                //here bytes
                Array.Reverse(bytes);
                bytes = ReversY180Deg(bytes);

                images[i] = new(bytes, datalabels[i], new System.Numerics.Vector2(28, 28));
            }

            return images;
        }
        private static float[] ReversY180Deg(float[] bytes) {
            float[,] array = ConvertTo2DArray(bytes);
            float[,] result = new float[imageSizeSqrt, imageSizeSqrt];

            for (int i = 0; i < array.Length; i++) {
                result[i % imageSizeSqrt, imageSizeSqrt - 1 - i / imageSizeSqrt] = array[i % imageSizeSqrt, i / imageSizeSqrt];
            }

            return ConvertTo1DArray(result);
        }

        private static float[] ConvertTo1DArray(float[,] bytes)
            => bytes.Cast<float>().ToArray();

        private static float[,] ConvertTo2DArray(float[] bytes, int height = 28, int width = 28) {
            float[,] output = new float[height, width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    output[i, j] = bytes[i * width + j];
            return output;
        }
    }
}