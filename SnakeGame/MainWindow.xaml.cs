using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
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
        List<Rectangle> foodList = new List<Rectangle>();
        DispatcherTimer timer;
        int score = 0;

        public MainWindow()
        {
            InitializeComponent();

            createBody();

            addFood();
            addFood();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        
                                                                                                        //RUTINA KAŽDÉHO TICKU TIMERU
        private void Timer_Tick(object sender, EventArgs e)
        {
            isColliding();

            isEating();

            bodyMove();

            direction();
            if(direction() == 0)
            {
                moveHeadRight();
            }
            if(direction() == 1)
            {
                moveHeadLeft();
            }
            if(direction() == 2)
            {
                moveHeadUp();
            }
            if(direction() == 3)
            {
                moveHeadDown();
            }
        }
                                                                                                        //VYTVOŘENÍ ELEMENTŮ HRY
                                                                                                                    //SNAKE PARTS
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

        public void createBody()
        {
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
        }
                                                                                                                    //FOOD
        public void createFood()
        {
            Rectangle food = new Rectangle();
            food.Height = 20;
            food.Width = 20;
            food.Fill = Brushes.Red;
            food.StrokeThickness = 3;
            food.Stroke = Brushes.Black;
            food.RadiusX = 50;
            food.RadiusY = 50;

            foodList.Add(food);
        }

        public void addFood()
        {
                createFood();

                Random r = new Random();
                int rColumn = r.Next(0, 14);
                int rRow = r.Next(0, 14);

                mainGrid.Children.Add(foodList.Last());
                Grid.SetColumn(foodList.Last(), rColumn);
                Grid.SetRow(foodList.Last(), rRow);
        }
                                                                                                        //MOVEMENT
                                                                                                                    //HEAD MOVEMENT
        public void moveHeadRight()
        {
            int currentColumn = Grid.GetColumn(snake[0]);
            if(currentColumn == 14)
            {
                gameOver();
            }
            else
            {
                Grid.SetColumn(snake[0], currentColumn + 1);
            }
        }

        public void moveHeadLeft()
        {
            int currentColumn = Grid.GetColumn(snake[0]);
            if (currentColumn == 0)
            {
                gameOver();
            }
            else
            {
                Grid.SetColumn(snake[0], currentColumn - 1);
            }
        }

        public void moveHeadUp()
        {
            int currentRow = Grid.GetRow(snake[0]);
            if (currentRow == 0)
            {
                gameOver();
            }
            else
            {
                Grid.SetRow(snake[0], currentRow - 1);
            }
        }

        public void moveHeadDown()
        {
            int currentRow = Grid.GetRow(snake[0]);
            if (currentRow == 14)
            {
                gameOver();
            }
            else
            {
                Grid.SetRow(snake[0], currentRow + 1);
            }
        }
                                                                                                                    //DIRECTION
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
                                                                                                                    //BODY MOVEMENT
        public void bodyMove()
        {
            for(int i = snake.Count - 1; i >= 1; i--)
            {
                i--;
                int previousColumn = Grid.GetColumn(snake[i]);
                int previousRow = Grid.GetRow(snake[i]);
                i++;
                Grid.SetColumn(snake[i], previousColumn);
                Grid.SetRow(snake[i], previousRow);
            }
        }
                                                                                                        //COLLISION
        private void isEating()
        {
            int snakeColumn = Grid.GetColumn(snake[0]);
            int snakeRow = Grid.GetRow(snake[0]);

            int foodColumn0 = Grid.GetColumn(foodList[0]);
            int foodRow0 = Grid.GetRow(foodList[0]);

            int foodColumn1 = Grid.GetColumn(foodList[1]);
            int foodRow1 = Grid.GetRow(foodList[1]);

            if(snakeColumn == foodColumn0 && snakeRow == foodRow0)
            {
                score++;
                Random r = new Random();
                int rColumn = r.Next(0, 14);
                int rRow = r.Next(0, 14);

                Grid.SetColumn(foodList[0], rColumn);
                Grid.SetRow(foodList[0], rRow);

                int tailColumn = Grid.GetColumn(snake.Last());
                int tailRow = Grid.GetRow(snake.Last());

                createBodyPart();
                Grid.SetColumn(snake.Last(), tailColumn);
                Grid.SetRow(snake.Last(), tailRow);
                mainGrid.Children.Add(snake.Last());
            }

            if (snakeColumn == foodColumn1 && snakeRow == foodRow1)
            {
                score++;
                Random r = new Random();
                int rColumn = r.Next(0, 14);
                int rRow = r.Next(0, 14);

                Grid.SetColumn(foodList[1], rColumn);
                Grid.SetRow(foodList[1], rRow);

                int tailColumn = Grid.GetColumn(snake.Last());
                int tailRow = Grid.GetRow(snake.Last());

                createBodyPart();
                Grid.SetColumn(snake.Last(), tailColumn);
                Grid.SetRow(snake.Last(), tailRow);
                mainGrid.Children.Add(snake.Last());
            }
        }

        public void isColliding()
        {
            for(int i = snake.Count - 1; i >= 1; i--)
            {

                int columnPosition = Grid.GetColumn(snake[i]);
                int rowPosition = Grid.GetRow(snake[i]);

                int headColumnPosition = Grid.GetColumn(snake[0]);
                int headRowPosition = Grid.GetRow(snake[0]);

                if (columnPosition == headColumnPosition && rowPosition == headRowPosition)
                {
                    gameOver();
                }
            }
        }
                                                                                                        //GAME OVER STATUS
        public void gameOver()
        {
            timer.Stop();

            labelScore.Content = "Score: " + score;

            labelGameOver.Visibility = Visibility.Visible;
            labelScore.Visibility = Visibility.Visible;
            buttonRestart.Visibility = Visibility.Visible;
            buttonEnd.Visibility = Visibility.Visible;
        }
                                                                                                                    //BUTTONS
        private void buttonRestart_Click(object sender, RoutedEventArgs e)
        {
            var currentExecutablePath = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(currentExecutablePath);
            Application.Current.Shutdown();
        }

        private void buttonEnd_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
