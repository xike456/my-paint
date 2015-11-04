using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paint.Shape
{
    public class ShapeProperties
    {
        public int strokeThickness = 1;
        public SolidColorBrush fillColor;
        public SolidColorBrush strokeColor;
        public DoubleCollection strokeStyle;
        public static ShapeProperties instance = null;

        private ShapeProperties()
        {
           
        }

        public ShapeProperties GetInstance()
        {
            if (instance == null)
            {
                instance = new ShapeProperties();
            }
            return instance;
        }

        // set size cho nét vẽ.
        public void SetStroke(int value)
        {
            strokeThickness = value;
        }

        // set màu cho nét vẽ.
        public void SetStrokeColor(Color value)
        {
            strokeColor = new SolidColorBrush(value);
        }

        // set màu bên trong.
        public void SetFillColor(Color value)
        {
            fillColor = new SolidColorBrush(value);
        }
        // set kiểu nét vẽ.
        public void SetStrokeStyle(DoubleCollection style)
        {
            strokeStyle = style;
        }
    }
}
