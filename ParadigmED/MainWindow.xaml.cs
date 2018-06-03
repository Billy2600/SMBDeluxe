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

namespace ParadigmED
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle selectedRect = null;
        Brush oldFill = null; // Store fill of rectangle, to return it to normal after moving it

        public MainWindow()
        {
            InitializeComponent();
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
            Rectangle rect = sender as Rectangle;

            oldFill = rect.Fill;
            rect.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            selectedRect = rect;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = sender as Rectangle;

            selectedRect = null;
            rect.Fill = oldFill;
        }
    }
}
