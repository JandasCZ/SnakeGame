using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
                                                                                                        //HLAVNÍ SPUŠTĚNÝ KÓD
        List<Rectangle> snake = new List<Rectangle>();
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 3; i++)
            {
                createBodyPart();
            }
            for (int i = 0; i < snake.Count; i++)
            {
                mainGrid.Children.Add(snake.ElementAt(i));
                Grid.SetColumn(snake.ElementAt(i), 5 - i);
                Grid.SetRow(snake.ElementAt(i), 7);
            }

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        
                                                                                                        //RUTINA KAŽDÉHO TICKU TIMERU
        private void Timer_Tick(object sender, EventArgs e)
        {
            direction();
            if(direction() == 0)
            {
                moveSnakeRight();
            }
            if(direction() == 1)
            {
                moveSnakeLeft();
            }
            if(direction() == 2)
            {
                moveSnakeUp();
            }
            if(direction() == 3)
            {
                moveSnakeDown();
            }
            
        }
                                                                                                        //VYTVOŘENÍ BODY PARTU SNAKA
        public void createBodyPart()
        {
            Rectangle body = new Rectangle();
            body.Height = 45;
            body.Width = 45;
            body.Fill = Brushes.Green;
            body.StrokeThickness = 3;
            body.Stroke = Brushes.Black;

            body.RadiusX = 50;
            body.RadiusY = 50;

            snake.Add(body);
        }
                                                                                                        //MOVEMENT
        public void moveSnakeRight()
        {
            for (int i = 0; i < snake.Count; i++)
            {
                int currentColumn = Grid.GetColumn(snake.ElementAt(i));
                Grid.SetColumn(snake.ElementAt(i), currentColumn + 1);
            }
        }

        public void moveSnakeLeft()
        {
            for (int i = 0; i < snake.Count; i++)
            {
                int currentColumn = Grid.GetColumn(snake.ElementAt(i));
                Grid.SetColumn(snake.ElementAt(i), currentColumn - 1);
            }
        }

        public void moveSnakeUp()
        {
            for (int i = 0; i < snake.Count; i++)
            {
                int currentRow = Grid.GetRow(snake.ElementAt(i));
                Grid.SetRow(snake.ElementAt(i), currentRow - 1);
            }
        }

        public void moveSnakeDown()
        {
            for (int i = 0; i < snake.Count; i++)
            {
                int currentRow = Grid.GetRow(snake.ElementAt(i));
                Grid.SetRow(snake.ElementAt(i), currentRow + 1);
            }
        }

        int direc = 0;
        public int direction()
        {
            if (Keyboard.IsKeyDown(Key.D))
            {
                if (direc == 1)
                {

                }
                else
                {
                    direc = 0;
                }
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                if(direc == 0)
                {

                }
                else
                {
                    direc = 1;
                }
            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                if (direc == 3)
                {

                }
                else
                {
                    direc = 2;
                }
            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                if (direc == 2)
                {

                }
                else
                {
                    direc = 3;
                }
            }

            return direc;
        }
                                                                                                        //GAME OVER STATUS
        public void gameOver()
        {
            timer.Stop();
            labelGameOver.Visibility = Visibility.Visible;


        }
    }
}
