using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Shapes;
using Paint.Shape;

namespace Paint
{
    class DrawRectangle : BaseShape
    {
        public override void Draw()
        {
            base.Draw();
            Rectangle rectangle = new Rectangle();
            rectangle.Fill = fillColor;
            rectangle.Stroke = strokeColor;
            rectangle.StrokeThickness = strokeThickness;

            rectangle.Width = Math.Abs(startPoint.X - endPoint.X);
            rectangle.Height =Math.Abs( startPoint.Y - endPoint.Y);

            Canvas.SetTop(rectangle, Math.Min(startPoint.Y, endPoint.Y));
            Canvas.SetLeft(rectangle, Math.Min(startPoint.X, endPoint.X));

            shape = rectangle;

        }
    }
}
