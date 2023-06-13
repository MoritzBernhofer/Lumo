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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Threading;

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
                            CreateBitmap(imageBytes);
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
        private void CreateBitmap(byte[] array) {
            Bitmap bitmap = ConvertByteArrayToBitmap(array);

            // Display the Bitmap
            ImageControl.Source = ImageSourceFromBitmap(bitmap);
        }
        static Bitmap ConvertByteArrayToBitmap(byte[] imageBytes) {
            MemoryStream stream = new MemoryStream(imageBytes);

            return new Bitmap(stream);
        }
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public ImageSource ImageSourceFromBitmap(Bitmap bmp) {
            var handle = bmp.GetHbitmap();
            try {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }
    }
}
