namespace NumberRecognition.Image {
    public class ImageHelper {
        public static Image[] LoadImages(string datapath, string labelpath) {
            var data = File.ReadAllBytes("../../../Images Mnist.bytes");
            var datalabels = File.ReadAllBytes("../../../Labels Mnist.bytes");
            Image[] images = new Image[(int)data.Length / 1024];

            int counter = 0;
            for (int i = 0; i < data.Length / 1024; i++) {
                byte[] bytes = new byte[1024];

                for(int j = 0; j < bytes.Length; j++) {
                    bytes[j] = data[counter++];
                }
                images[i] = new(bytes, datalabels[i], new System.Numerics.Vector2(32, 32));
            }
            return images;
        }
    }
}
