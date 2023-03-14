namespace NumberRecognition.ImageFolder {
    public class ImageHelper {
        private static int imageSize = 784;
        public static Image[] LoadImages(string datapath, string labelpath) {
            var data = File.ReadAllBytes("../../../Images Mnist.bytes");
            var datalabels = File.ReadAllBytes("../../../Labels Mnist.bytes");
            Image[] images = new Image[data.Length / imageSize];

            int counter = 0;
            for (int i = 0; i < data.Length / imageSize; i++) {
                byte[] bytes = new byte[imageSize];

                for (int j = 0; j < bytes.Length; j++) 
                    bytes[j] = data[counter++];


                images[i] = new(bytes, datalabels[i], new System.Numerics.Vector2(28, 28));
            }
            return images;
        }
    }
}
