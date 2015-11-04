using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace Paint
{
    public class Adorners : Adorner
    {
        public Thumb topLeft, topRight, btmLeft, btmRight;

        public VisualCollection visualChilren;

        public Adorners(UIElement adornedElement) : base(adornedElement)
        {
            visualChilren = new VisualCollection(this);

            BuildAdornersThumb(ref topLeft, Cursors.SizeNWSE);
            BuildAdornersThumb(ref topRight, Cursors.SizeNESW);
            BuildAdornersThumb(ref btmLeft, Cursors.SizeNESW);
            BuildAdornersThumb(ref btmRight, Cursors.SizeNWSE);

            topLeft.DragDelta += new DragDeltaEventHandler(HandleTopLeft);
            topRight.DragDelta += new DragDeltaEventHandler(HandleTopRight);
            btmLeft.DragDelta += new DragDeltaEventHandler(HandleBtmLeft);
            btmRight.DragDelta += new DragDeltaEventHandler(HandleBtmRight);

        }

        private void HandleBtmRight(object sender, DragDeltaEventArgs e)
        {
            FrameworkElement adornedElement = this.AdornedElement as FrameworkElement;
            Thumb hitThumb = sender as Thumb;

            if (adornedElement == null || hitThumb == null) return;
            FrameworkElement parentElement = adornedElement.Parent as FrameworkElement;

            EnforceSize(adornedElement);

            adornedElement.Width = Math.Max(adornedElement.Width + e.HorizontalChange, hitThumb.DesiredSize.Width);
            adornedElement.Height = Math.Max(e.VerticalChange + adornedElement.Height, hitThumb.DesiredSize.Height);
        }

        private void HandleBtmLeft(object sender, DragDeltaEventArgs e)
        {
            FrameworkElement adornedElement = AdornedElement as FrameworkElement;
            Thumb hitThumb = sender as Thumb;

            if (adornedElement == null || hitThumb == null) return;

            EnforceSize(adornedElement);

            adornedElement.Height = Math.Max(e.VerticalChange + adornedElement.Height, hitThumb.DesiredSize.Height);

            double width_old = adornedElement.Width;
            double width_new = Math.Max(adornedElement.Width - e.HorizontalChange, hitThumb.DesiredSize.Width);
            double left_old = Canvas.GetLeft(adornedElement);
            adornedElement.Width = width_new;
            Canvas.SetLeft(adornedElement, left_old - (width_new - width_old));
        }

        

        private void HandleTopRight(object sender, DragDeltaEventArgs e)
        {
            FrameworkElement adornedElement = this.AdornedElement as FrameworkElement;
            Thumb hitThumb = sender as Thumb;

            if (adornedElement == null || hitThumb == null) return;
            FrameworkElement parentElement = adornedElement.Parent as FrameworkElement;

            EnforceSize(adornedElement);

            adornedElement.Width = Math.Max(adornedElement.Width + e.HorizontalChange, hitThumb.DesiredSize.Width);

            double height_old = adornedElement.Height;
            double height_new = Math.Max(adornedElement.Height - e.VerticalChange, hitThumb.DesiredSize.Height);
            double top_old = Canvas.GetTop(adornedElement);
            adornedElement.Height = height_new;
            Canvas.SetTop(adornedElement, top_old - (height_new - height_old));
        }

        public void BuildAdornersThumb(ref Thumb cornerThumb, Cursor customCursor)
        {
            if (cornerThumb != null)
            {
                return;
            }
            else
            {
                cornerThumb = new Thumb();
                cornerThumb.Cursor = customCursor;
                cornerThumb.Height = cornerThumb.Width = 10;
                cornerThumb.Opacity = 0.8f;
                cornerThumb.Background = new SolidColorBrush(Colors.BlueViolet);

                visualChilren.Add(cornerThumb);
            }
        }

        private void HandleTopLeft(object sender, DragDeltaEventArgs e)
        {
            FrameworkElement adornedElement = AdornedElement as FrameworkElement;
            Thumb hitThumb = sender as Thumb;

            if (adornedElement == null || hitThumb == null) return;

            EnforceSize(adornedElement);

            double width_old = adornedElement.Width;
            double width_new = Math.Max(adornedElement.Width - e.HorizontalChange, hitThumb.DesiredSize.Width);
            double left_old = Canvas.GetLeft(adornedElement);
            adornedElement.Width = width_new;
            Canvas.SetLeft(adornedElement, left_old - (width_new - width_old));

            double height_old = adornedElement.Height;
            double height_new = Math.Max(adornedElement.Height - e.VerticalChange, hitThumb.DesiredSize.Height);
            double top_old = Canvas.GetTop(adornedElement);
            adornedElement.Height = height_new;
            Canvas.SetTop(adornedElement, top_old - (height_new - height_old));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double desiredWidth = AdornedElement.DesiredSize.Width;
            double desiredHeight = AdornedElement.DesiredSize.Height;
            double adornerWidth = this.DesiredSize.Width;
            double adornerHeight = this.DesiredSize.Height;

            topLeft.Arrange(new Rect(-adornerWidth / 2, -adornerHeight / 2, adornerWidth, adornerHeight));
            topRight.Arrange(new Rect(desiredWidth - adornerWidth / 2, -adornerHeight / 2, adornerWidth, adornerHeight));
            btmLeft.Arrange(new Rect(-adornerWidth / 2, desiredHeight - adornerHeight / 2, adornerWidth, adornerHeight));
            btmRight.Arrange(new Rect(desiredWidth - adornerWidth / 2, desiredHeight - adornerHeight / 2, adornerWidth, adornerHeight));

            return finalSize;
        }

        private void EnforceSize(FrameworkElement adornedElement)
        {
            if (adornedElement.Width.Equals(Double.NaN))
                adornedElement.Width = adornedElement.DesiredSize.Width;
            if (adornedElement.Height.Equals(Double.NaN))
                adornedElement.Height = adornedElement.DesiredSize.Height;

            FrameworkElement parent = adornedElement.Parent as FrameworkElement;
            if (parent != null)
            {
                adornedElement.MaxHeight = parent.ActualHeight;
                adornedElement.MaxWidth = parent.ActualWidth;
            }
        }

        protected override int VisualChildrenCount { get { return visualChilren.Count; } }
        protected override Visual GetVisualChild(int index) { return visualChilren[index]; }
    }
}
