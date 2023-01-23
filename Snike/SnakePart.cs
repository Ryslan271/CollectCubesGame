using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snike
{
    public class SnakePart
    {
        public UIElement UiElement { get; set; } = new Rectangle
        {
            Width = 20,
            Height = 20,
            Fill = Brushes.Green,
            StrokeThickness = 0.5,
            Stroke = Brushes.Black
        };

        public int X { get; set; } = 200;
        public int Y { get; set; } = 200;

        public bool IsHead { get; set; }
    }
}
