using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paint.Shape
{
    class DrawEllipse : BaseShape
    {
        public override void Draw()
        {
            base.Draw();
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = fillColor;
            ellipse.Stroke = strokeColor;
            ellipse.StrokeThickness = strokeThickness;

            ellipse.Width = Math.Abs(startPoint.X - endPoint.X);
            ellipse.Height = Math.Abs(startPoint.Y - endPoint.Y);

            Canvas.SetTop(ellipse, Math.Min(startPoint.Y, endPoint.Y));
            Canvas.SetLeft(ellipse, Math.Min(startPoint.X, endPoint.X));

            shape = ellipse;
            
        }
    }
}
