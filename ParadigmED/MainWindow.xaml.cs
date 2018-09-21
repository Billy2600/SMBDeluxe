using System;
using System.Collections.Generic;
using System.Linq;
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
using System.IO;
using Microsoft.Win32;

namespace ParadigmED
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum Mode
        {
            Draw,
            Select
        }
        public Mode CurrentMode { get; set; }

        Rectangle selectedRect = null;
        public Rectangle SelectedRect
        {
            get { return selectedRect; }
            set { selectedRect = value; }
        }
        public List<Rectangle> Rectangles { get; set; }

        private Brush oldFill = null; // Store fill of rectangle, to return it to normal after moving it
        private Random r = new Random();

        public MainWindow()
        {
            CurrentMode = Mode.Select;
            Rectangles = new List<Rectangle>();
            InitializeComponent();

            // TEMPORARY add rectangles
            for (int i = 0; i < 5; i++)
            {
                AddRectangle("Rect" + i.ToString(), i * 10, i * 10, 50, 50);
            }
        }

        private void AddRectangle(string name, int x, int y, int width, int height)
        {
            Rectangle newRect = new Rectangle()
            {
                Name = name,
                Width = width,
                Height = height,
                // Generate random color
                Fill = new SolidColorBrush(Color.FromRgb((byte)r.Next(0, 255), (byte)r.Next(255), (byte)r.Next(255))),
            };
            newRect.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            newRect.MouseLeftButtonUp += Rectangle_MouseLeftButtonUp;

            DrawingCanvas.Children.Add(newRect);
            Canvas.SetTop(newRect, x);
            Canvas.SetLeft(newRect, y);
            Rectangles.Add(newRect);
        }

        // Drag rectangles once they've been selected
        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedRect == null)
                return;

            Canvas.SetTop(selectedRect, e.GetPosition(DrawingCanvas).Y - selectedRect.Height / 2);
            Canvas.SetLeft(selectedRect, e.GetPosition(DrawingCanvas).X - selectedRect.Width / 2);
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CurrentMode == Mode.Draw)
            {
                return;
            }

            Rectangle rect = sender as Rectangle;
            selectedRect = rect;

            oldFill = rect.Fill;
            rect.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CurrentMode == Mode.Draw)
            {
                return;
            }

            Rectangle rect = sender as Rectangle;
            selectedRect = null;
            rect.Fill = oldFill;
        }

        private void SaveFile_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, "Test");
            }
        }

        private void DrawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(CurrentMode == Mode.Draw)
            {
                AddRectangle("Rect" + (Rectangles.Count + 1).ToString(),
                    (int)e.GetPosition(DrawingCanvas).X,
                    (int)e.GetPosition(DrawingCanvas).Y,
                    50, 50);
            }

            //CurrentMode = Mode.Select;
            //selectedRect = Rectangles[Rectangles.Count - 1];
        }

        private void DrawingCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void DrawMode_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DrawMode_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentMode = Mode.Draw;
        }
    }

    // Added after MainWindow class so we can reference it
    public static class Command
    {
        public static readonly RoutedUICommand DrawMode = new RoutedUICommand("Draw Mode", "Draw Mode", typeof(MainWindow));
    }
}
