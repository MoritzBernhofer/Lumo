namespace NumberRecognition.ImageFolder
{
    public class ImageHelper
    {
        private static int imageSize = 784;
        private static int imageSizeSqrt = (int)Math.Sqrt(imageSize);
        private static int[] byteNumbers = { 128, 64, 32, 16, 8, 4, 2, 1 };

        public static Image[] LoadImages(string datapath, string labelpath)
        {
            var data = File.ReadAllBytes("../../../Images Mnist.bytes");
            var datalabels = File.ReadAllBytes("../../../Labels Mnist.bytes");
            Image[] images = new Image[data.Length / imageSize];


            int counter = 0;
            for (int i = 0; i < data.Length / imageSize; i++)
            {
                byte[] bytes = new byte[imageSize];

                for (int j = 0; j < bytes.Length; j++)
                    bytes[j] = data[counter++];


                //here bytes
                Array.Reverse(bytes);  
                bytes = Reverse(bytes);

                images[i] = new(bytes, datalabels[i], new System.Numerics.Vector2(28, 28));
            }

            return images;
        }
        private static byte[] Reverse(byte[] bytes)
        {
            byte[,] array = ConvertTo2D(bytes);
            byte[,] result = new byte[imageSizeSqrt, imageSizeSqrt];

            for (int i = 0; i < array.Length; i++)
            {
                result[i % imageSizeSqrt, imageSizeSqrt - 1 - i / imageSizeSqrt] = array[i % imageSizeSqrt, i / imageSizeSqrt];
            }

            return ConvertTo1D(result);
        }
        private static byte[] ReverseRows(byte[] bytes)
        {
            byte[,] array = ConvertTo2D(bytes);
            byte[,] result = new byte[imageSizeSqrt, imageSizeSqrt];

            for (int i = 0; i < array.Length; i++)
            {
                result[i % imageSizeSqrt, imageSizeSqrt - 1 - i / imageSizeSqrt] = array[i / imageSizeSqrt, i % imageSizeSqrt];
            }

            return ConvertTo1D(result);
        }

        private static byte[] ConvertTo1D(byte[,] bytes)
        {
            byte[] result = new byte[bytes.Length];

            for (int i = 0; i < bytes.Length; i++)
            {
                result[i] = bytes[i / imageSizeSqrt, i % imageSizeSqrt];
            }

            return result;
        }

        private static byte[,] ConvertTo2D(byte[] bytes)
        {
            byte[,] result = new byte[imageSizeSqrt, imageSizeSqrt];

            for (int i = 0; i < bytes.Length; i++)
            {
                result[i / imageSizeSqrt, i% imageSizeSqrt] = bytes[i];
            }

            return result;
        }
    }
}