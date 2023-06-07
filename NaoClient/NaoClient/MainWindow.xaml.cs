using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NaoClient {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private TcpListener? listener = null;
        private TcpClient? client = null;

        List<byte[]> packets = new();

        public MainWindow() {
            InitializeComponent();
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e) {
            // Define the TCP server address and port
            string serverAddress = "10.0.0.154";
            int serverPort = 12345;

            // Create a TCP listener
            listener = new TcpListener(IPAddress.Parse(serverAddress), serverPort);
            listener.Start();

            // Accept a client connection
            StatusTextBlock.Text = "Waiting";

            client = await listener.AcceptTcpClientAsync();
            StatusTextBlock.Text = "Connected to client: " + ((IPEndPoint)client.Client.RemoteEndPoint).Address;

            // Receive and display images
            using (NetworkStream stream = client.GetStream()) {
                while (true) {
                    try {
                        // Receive the image data
                        byte[] data = new byte[4096];
                        using (MemoryStream ms = new MemoryStream()) {
                            int bytesRead;
                            do {
                                bytesRead = await stream.ReadAsync(data, 0, data.Length);
                                ms.Write(data, 0, bytesRead);
                            }
                            while (bytesRead == data.Length);

                            // Convert the received bytes to an image
                            byte[] imageBytes = ms.ToArray();
                            packets.Add(imageBytes);
                            //using (MemoryStream imageStream = new MemoryStream(imageBytes)) {
                            //    // Load the image from the stream
                            //    BitmapImage bitmapImage = new BitmapImage();
                            //    bitmapImage.BeginInit();
                            //    bitmapImage.StreamSource = imageStream;
                            //    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            //    bitmapImage.EndInit();
                            //    bitmapImage.Freeze();

                            //    // Display the image in the Image control
                            //    ImageControl.Source = bitmapImage;
                            //}
                        }
                    }
                    catch (IOException) {
                        // Connection closed by the client
                        break;
                    }
                }
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e) {
            // Close the client and listener
            client?.Close();
            listener?.Stop();
            StatusTextBlock.Text = "Server stopped";
        }
    }
}
