using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Paint.Shape
{
    class BaseShape
    {
        public Point startPoint;
        public Point endPoint;
        public System.Windows.Shapes.Shape shape;
        public Canvas localCanvas;
        public int strokeThickness = 3;
        public SolidColorBrush fillColor = new SolidColorBrush(Colors.Transparent);
        public SolidColorBrush strokeColor = Brushes.Black;
        public bool isDrawing = false;

        public virtual void Draw()
        {
            
        }
    }
}
