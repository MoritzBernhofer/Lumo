namespace NumberRecognition.ImageFolder {
    public class ImageHelper {
        private static int imageSize = 784;
        private static int imageSizeSqrt = (int)Math.Sqrt(imageSize);
        private static int[] byteNumbers = { 128, 64, 32, 16, 8, 4, 2, 1 };

        public static Image[] LoadImages(string datapath, string labelpath) {
            var data = File.ReadAllBytes("../../../Images Mnist.bytes");
            var datalabels = File.ReadAllBytes("../../../Labels Mnist.bytes");
            Image[] images = new Image[data.Length / imageSize];


            int counter = 0;
            for (int i = 0; i < data.Length / imageSize; i++) {
                byte[] bytes = new byte[imageSize];

                for (int j = 0; j < bytes.Length; j++)
                    bytes[j] = data[counter++];


                //here bytes
                Array.Reverse(bytes);
                bytes = ReversY180Deg(bytes);

                images[i] = new(bytes, datalabels[i], new System.Numerics.Vector2(28, 28));
            }

            return images;
        }
        private static byte[] ReversY180Deg(byte[] bytes) {
            byte[,] array = ConvertTo2DArray(bytes);
            byte[,] result = new byte[imageSizeSqrt, imageSizeSqrt];

            for (int i = 0; i < array.Length; i++) {
                result[i % imageSizeSqrt, imageSizeSqrt - 1 - i / imageSizeSqrt] = array[i % imageSizeSqrt, i / imageSizeSqrt];
            }

            return ConvertTo1DArray(result);
        }

        private static byte[] ConvertTo1DArray(byte[,] bytes)
            => bytes.Cast<byte>().ToArray();

        private static byte[,] ConvertTo2DArray(byte[] bytes, int height = 28, int width = 28) {
            byte[,] output = new byte[height, width];
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    output[i, j] = bytes[i * width + j];
                }
            }
            return output;
        }
    }
}