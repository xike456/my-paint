using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Paint
{
    class ResizeAdorners
    {
        public AdornerLayer aLayer;

        public bool isDown;
        public bool isDragging;
        public bool selected = false;
        public UIElement selectedElement = null;

        public Point startPoint;
        public double originalLeft;
        public double originalTop;

        public static ResizeAdorners Instance = new ResizeAdorners();

        public void StopDragging()
        {
            if (isDown)
            {
                isDown = false;
                isDragging = false;
            }
        }

        public void Dragging(object sender, MouseEventArgs e, Canvas myCanvas)
        {
            if (isDown)
            {
                if ((isDragging == false) &&
                    ((Math.Abs(e.GetPosition(myCanvas).X - startPoint.X) > SystemParameters.MinimumHorizontalDragDistance) ||
                    (Math.Abs(e.GetPosition(myCanvas).Y - startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)))
                    isDragging = true;

                if (isDragging)
                {
                    Point position = Mouse.GetPosition(myCanvas);
                    Canvas.SetTop(selectedElement, position.Y - (startPoint.Y - originalTop));
                    Canvas.SetLeft(selectedElement, position.X - (startPoint.X - originalLeft));
                }
            }
        }

        public void Deselect()
        {
            if (selected)
            {
                selected = false;
                if (selectedElement != null)
                {
                    // Remove the adorner from the selected element
                    aLayer.Remove(aLayer.GetAdorners(selectedElement)[0]);
                    selectedElement = null;
                }
            }
        }

        public void Select(object sender, MouseButtonEventArgs e, Canvas myCanvas)
        {
            if (e.Source != myCanvas)
            {
                isDown = true;
                startPoint = e.GetPosition(myCanvas);

                selectedElement = e.Source as UIElement;

                originalLeft = Canvas.GetLeft(selectedElement);
                originalTop = Canvas.GetTop(selectedElement);

                aLayer = AdornerLayer.GetAdornerLayer(selectedElement);
                aLayer.Add(new Adorners(selectedElement));
                selected = true;
                e.Handled = true;
            }
        }
    }
}
