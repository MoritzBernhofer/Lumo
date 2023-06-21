using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;

namespace TCPServer {
    public partial class MainWindow : Window {
        private TcpListener? tcpListener;
        private Thread? listenThread;

        public MainWindow() {
            InitializeComponent();
        }

        public void StartButton_Click(object sender, RoutedEventArgs e) {
            // Start the TCP server on a separate thread
            listenThread = new Thread(new ThreadStart(ListenForClients));
            listenThread.Start();
            Console.WriteLine("Server started");
        }

        private void ListenForClients() {
            // Define the server address and port
            IPAddress ipAddress = IPAddress.Parse("172.17.210.91");  // Replace with the IP address of your server
            int port = 12345;

            // Create a TCP listener
            tcpListener = new TcpListener(ipAddress, port);
            tcpListener.Start();

            while (true) {
                // Accept incoming client connections
                TcpClient client = tcpListener.AcceptTcpClient();

                // Start a new thread to handle the client connection
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientConnection));
                clientThread.Start(client);
            }
        }

        private void HandleClientConnection(object clientObj) {
            TcpClient client = (TcpClient)clientObj;
            Console.WriteLine("Client connected: " + client.Client.RemoteEndPoint);

            byte[] buffer = new byte[1024];
            int bytesRead;
            int totalBytesRead = 0;
            DateTime startTime = DateTime.Now;
            int imageSize = 921600; // Size of a single image in bytes


            using (MemoryStream ms = new MemoryStream()) {
                while (true) {
                    // Read the image data from the client
                    bytesRead = client.GetStream().Read(buffer, 0, buffer.Length);
                    ms.Write(buffer, 0, bytesRead);
                    totalBytesRead += bytesRead;

                    // Check if a complete image has been received
                    if (totalBytesRead >= imageSize) {
                        // Process the received image data (e.g., save it to a file)

                        byte[] imageBytes = ms.ToArray();

                        byte[] pattern = { 255, 216 };

                        int index = FindPattern(imageBytes, pattern);

                        if (index == -1) {
                            break;
                        }

                        byte[] save = imageBytes;

                        imageBytes = new byte[imageSize];
                        for (int i = 0; i < 921595; i++) { //921595 because minus 5 from the pattern
                            imageBytes[i] = save[index++];
                            if (index >= imageSize) {
                                break;
                            }
                        }


                        // Update the transfer rate text block on the UI thread
                        Dispatcher.Invoke(() => {
                            TransferRateTextBlock.Text = totalBytesRead.ToString();
                        });

                        // Display the image in a WPF Image control
                        Dispatcher.Invoke(() => {
                            BitmapImage imageSource = new BitmapImage();
                            imageSource.BeginInit();
                            imageSource.StreamSource = new MemoryStream(imageBytes);
                            imageSource.EndInit();
                            YourImageControl.Source = imageSource;
                        });

                        // Reset the stream and counters for the next image
                        ms.SetLength(0);
                        totalBytesRead = 0;
                    }
                }
            }
        }


        public void StopButton_Click(object sender, RoutedEventArgs e) {
            // Stop the TCP listener and join the listen thread
            tcpListener.Stop();
            listenThread.Join();
            Console.WriteLine("Server stopped");
        }
        public static int FindPattern(byte[] byteArray, byte[] pattern) {
            for (int i = 0; i < byteArray.Length - pattern.Length + 1; i++) {
                bool found = true;
                for (int j = 0; j < pattern.Length; j++) {
                    if (byteArray[i + j] != pattern[j]) {
                        found = false;
                        break;
                    }
                }

                if (found) {
                    return i;
                }
            }

            return -1; // Pattern not found
        }
    }
}
