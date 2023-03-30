using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
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

namespace AITrainer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private Progress progress;
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
            progress = new(new Vector2((int)Math.Round(Progress_Display.Width), (int)Math.Round(Progress_Display.Height)));
            progress.Update(30);
            bool[,] array = progress.GetProgress();

            Bitmap newImage = new(array.GetLength(0), array.GetLength(1));


        }
    }
}
