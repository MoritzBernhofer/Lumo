using NumberRecognition.ConsoleFolder;
using NumberRecognition.ImageFolder;

namespace NumberRecognition;
class ZuWild {
    static void Main(string[] args) {
        Image[] images = ImageHelper.LoadImages("../../../Images Mnist.bytes", "../../../Labels Mnist.bytes");
        //70k images
        int counter = 0;
        while (true) {
            ConsoleHelper.PrintImageToConsole(images[counter++]);
            Console.ReadKey();
            Console.Clear();

        }


    }

}