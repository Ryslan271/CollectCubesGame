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

namespace Snike
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int SnakeSquareSize = 20;

        private List<Entities> Entities = new List<Entities>();
        private List<SnakePart> Snakes = new List<SnakePart>();

        private List<SnakePart> SnakesTemporary = new List<SnakePart>();
        private List<Entities> EntitiesTemporary = new List<Entities>();

        private Random rnd = new Random();

        private SnakePart Snake = new SnakePart();

        public MainWindow()
        {
            Snakes.Add(Snake);

            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawGameArea();

            DrawEssences();

            GameArea.Children.Add(Snake.UiElement);
            Canvas.SetTop(Snake.UiElement, Snake.Y);
            Canvas.SetLeft(Snake.UiElement, Snake.X);
        }

        private void DrawGameArea()
        {
            bool doneDrawingBackground = false;
            int nextX = 0, nextY = 0;
            int rowCounter = 0;
            bool nextIsOdd = false;

            while (doneDrawingBackground == false)
            {
                Rectangle rect = new Rectangle
                {
                    Width = SnakeSquareSize,
                    Height = SnakeSquareSize,
                    Fill = Brushes.White,
                    StrokeThickness = 0.5,
                    Stroke = Brushes.Black
                };

                GameArea.Children.Add(rect);
                Canvas.SetTop(rect, nextY);
                Canvas.SetLeft(rect, nextX);

                nextIsOdd = !nextIsOdd;
                nextX += SnakeSquareSize;
                if (nextX >= GameArea.ActualWidth)
                {
                    nextX = 0;
                    nextY += SnakeSquareSize;
                    rowCounter++;
                    nextIsOdd = (rowCounter % 2 != 0);
                }

                if (nextY >= GameArea.ActualHeight)
                    doneDrawingBackground = true;
            }
        }

        private void DrawEssences()
        {
            int countEssences = rnd.Next(10, 15);

            while (countEssences >= 0)
            {
                Entities essence = new Entities()
                {
                    Y = rnd.Next(1, 20) * 20,
                    X = rnd.Next(1, 20) * 20
                };

                Entities.Add(essence);

                GameArea.Children.Add(essence.rect);
                Canvas.SetTop(essence.rect, essence.Y);
                Canvas.SetLeft(essence.rect, essence.X);
                countEssences--;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (SnakePart snakeItem in Snakes)
            {
                switch (e.Key)
                {
                    case Key.D:
                        snakeItem.X += SnakeSquareSize;
                        break;

                    case Key.A:
                        snakeItem.X -= SnakeSquareSize;
                        break;

                    case Key.W:
                        snakeItem.Y -= SnakeSquareSize;
                        break;

                    case Key.S:
                        snakeItem.Y += SnakeSquareSize;
                        break;
                }
            }

            SnakesTemporary = new List<SnakePart>();
            EntitiesTemporary = new List<Entities>();

            ValidateEntitiesSnakes();

            Snakes.AddRange(SnakesTemporary);
            
            foreach (var item in EntitiesTemporary)
                Entities.Remove(item);

            AddGameArenaSnakes();
        }

        private void AddGameArenaSnakes()
        {
            foreach (var snakeItem in Snakes)
            {
                GameArea.Children.Remove(snakeItem.UiElement);
                GameArea.Children.Add(snakeItem.UiElement);
                Canvas.SetTop(snakeItem.UiElement, snakeItem.Y);
                Canvas.SetLeft(snakeItem.UiElement, snakeItem.X);
            }
        }

        private void ValidateEntitiesSnakes()
        {
            foreach (Entities item in Entities)
            {
                foreach (SnakePart snakeItem in Snakes)
                {
                    if ((item.X + SnakeSquareSize == snakeItem.X &&
                        item.Y == snakeItem.Y) ||
                        (item.X == snakeItem.X &&
                        item.Y - SnakeSquareSize == snakeItem.Y) ||
                        (item.X - SnakeSquareSize == snakeItem.X &&
                        item.Y == snakeItem.Y) ||
                        (item.X == snakeItem.X &&
                        item.Y + SnakeSquareSize == snakeItem.Y))
                    {
                        SnakesTemporary.Add(new SnakePart
                        {
                            X = item.X,
                            Y = item.Y
                        });

                        EntitiesTemporary.Add(item);
                        GameArea.Children.Remove(item.rect);
                    }
                }
            }
        }
    }
}
