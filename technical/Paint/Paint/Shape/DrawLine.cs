using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using Paint.Shape;

namespace Paint
{
    class DrawLine : BaseShape
    {
        public override void Draw()
        {
            base.Draw();
            Line line = new Line();
            line.Stroke = new SolidColorBrush(Colors.Black);

            line.X1 = startPoint.X;
            line.X2 = endPoint.X;
            line.Y1 = startPoint.Y;
            line.Y2 = endPoint.Y;
            shape = line;
        }
    }
}
