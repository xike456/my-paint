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
        //public static int strokeThickness = 1;
        //public static SolidColorBrush fillColor;
        //public static SolidColorBrush strokeColor;
        //public static DoubleCollection strokeStyle;
        public ShapeProperties shapeProperties = new ShapeProperties();
        public bool isDrawing = false;


        public virtual void Draw()
        {
            
        }
    }
}
