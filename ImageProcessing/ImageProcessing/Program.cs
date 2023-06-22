using System;
using System.Drawing;
using AForge.Imaging;
using AForge.Imaging.Filters;

class Program {
    static void Main(string[] args) {
        string imagePath = "C:\\Users\\flyti\\Documents\\GitHubONLY\\Lumo\\ImageProcessing\\ImageProcessing\\assets\\Screenshot_3.png"; // Replace with the actual path to your image

        Bitmap image = new Bitmap(imagePath);

        int centerX = 200; // X-coordinate of the number's position
        int centerY = 150; // Y-coordinate of the number's position

        int purpleSquareSize = 50; // Size of the purple square

        // Calculate the coordinates of the purple square
        int startX = Math.Max(0, centerX - purpleSquareSize / 2);
        int endX = Math.Min(image.Width - 1, centerX + purpleSquareSize / 2);
        int startY = Math.Max(0, centerY - purpleSquareSize / 2);
        int endY = Math.Min(image.Height - 1, centerY + purpleSquareSize / 2);

        // Create a new bitmap for the purple square
        Bitmap purpleSquareImage = new Bitmap(purpleSquareSize, purpleSquareSize);

        // Copy the pixels within the purple square to the new bitmap
        for (int x = startX; x <= endX; x++) {
            for (int y = startY; y <= endY; y++) {
                Color pixelColor = image.GetPixel(x, y);
                purpleSquareImage.SetPixel(x - startX, y - startY, pixelColor);
            }
        }

        // Save the purple square image to a file
        string outputImagePath = "path/to/output/image.jpg"; // Replace with the desired output file path
        purpleSquareImage.Save(outputImagePath);
    }
}
