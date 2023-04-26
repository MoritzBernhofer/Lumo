using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AITrainer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private Progress progress;
        private int percent = 5;
        public MainWindow() {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Minimize_Window(object sender, RoutedEventArgs e) {
            MainTab.WindowState = WindowState.Minimized;
        }

        private void Close_Window(object sender, RoutedEventArgs e) {
            MainTab.Close();
        }
        private void Maximize_Window(object sender, RoutedEventArgs e) {
            if (MainTab.WindowState == WindowState.Maximized)
                MainTab.WindowState = WindowState.Normal;
            else
                MainTab.WindowState = WindowState.Maximized;
        }

        private void ProgressThing(object sender, RoutedEventArgs e) {
            if (progress == null)
                progress = new((int)Math.Round(Progress_Display.ActualWidth), (int)Math.Round(Progress_Display.ActualHeight));

            progress.Update(percent++);

            Bitmap? map = progress.GetProgress();
            if (map != null) {
                Progress.Source = ToBitmapImage(map);
            }
        }
        public BitmapImage ToBitmapImage(Bitmap bitmap) {
            using (var memory = new MemoryStream()) {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
