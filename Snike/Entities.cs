using System.Windows.Media;
using System.Windows.Shapes;

namespace Snike
{
    public class Entities
    {
        public Rectangle rect = new Rectangle
        {
            Width = 20,
            Height = 20,
            Fill = Brushes.Black,
            StrokeThickness = 0.5,
            Stroke = Brushes.Black
        };
        public bool LiveBool { get; set; } = true;
        public int X { get; set; }
        public int Y { get; set; }
    }
}
