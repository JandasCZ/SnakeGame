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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            createHlava();
        }

        List<Rectangle> Snake = new List<Rectangle>();
        public void createHlava()
        {
            Rectangle head = new Rectangle();
            head.Height = 45;
            head.Width = 45;
            head.Fill = Brushes.Green;
            head.StrokeThickness = 3;
            head.Stroke = Brushes.Black;

            head.RadiusX = 50;
            head.RadiusY = 50;

            mainGrid.Children.Add(head);
            Grid.SetRow(head, 7);
            Grid.SetColumn(head, 7);
            Snake.Add(head);
        }

        public void createBodyPart()
        {
            Rectangle body = new Rectangle();
            body.Height = 45;
            body.Width =45;
            body.Fill = Brushes.Green;
            body.StrokeThickness = 3;
            body.Stroke = Brushes.Black;

            Snake.Add(body);
        }

        public void createTail()
        {
            Rectangle tail = new Rectangle();
            tail.Height = 45;
            tail.Width = 45;
            tail.Fill = Brushes.Green;
            tail.StrokeThickness = 3;
            tail.Stroke = Brushes.Black;

            tail.RadiusX = 50;
            tail.RadiusY = 50;

            Snake.Add(tail);
        }
    }
}
