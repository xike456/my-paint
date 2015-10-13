using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Paint.Shape;

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point startPoint;
        private Point endPoint;
        private bool _justAddedShape = false;
        private BaseShape _shape;
        public MainWindow()
        {
            InitializeComponent();
            _shape = new DrawRectangle();
            _shape.localCanvas = DrawCanvas;
        }
        private void DrawCanvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _shape.isDrawing = true;
            _justAddedShape = false;
            _shape.startPoint = e.GetPosition(sender as IInputElement);
        }

        private void DrawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_shape.isDrawing)
            {
                if (_justAddedShape)
                {
                    if (DrawCanvas.Children.Count > 0)
                    {
                        DrawCanvas.Children.RemoveAt(DrawCanvas.Children.Count-1);
                    }
                }
                _shape.endPoint = e.GetPosition(sender as IInputElement);
                _shape.Draw();
                DrawCanvas.Children.Add(_shape.shape);
                _justAddedShape = true;
                DrawCanvas.CaptureMouse();
            }
        }

        private void DrawCanvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_shape.isDrawing)
            {
                if (DrawCanvas.Children.Count > 0)
                {
                    DrawCanvas.Children.RemoveAt(DrawCanvas.Children.Count -1);
                }

                _shape.endPoint = e.GetPosition(sender as IInputElement);
                _shape.isDrawing = false;
                _shape.Draw();
                DrawCanvas.Children.Add(_shape.shape);
                _justAddedShape = true;
                DrawCanvas.ReleaseMouseCapture();
            }
        }
    }
}
