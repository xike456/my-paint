using System;
using System.Collections.Generic;
using System.Data;
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
using Xceed.Wpf.Toolkit;

namespace Paint
{
    public partial class MainWindow : Window
    {
        //public bool selectMode = false; TODO: check and remove

        private bool _justAddedShape = false;
        private BaseShape _shape;
        private int _count = 0;
        private bool _isSelect = false;
        private bool _selected = false;
        private AdornerLayer _myAdornerLayer;
        private UIElement _selectedElement;

        public MainWindow()
        {
            InitializeComponent();
            _shape.localCanvas = DrawCanvas;
        }


        private void Paint_Loaded(object sender, RoutedEventArgs e)
        {
            this.MouseLeftButtonDown += new MouseButtonEventHandler(Window1_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(DragFinishedMouseHandler);
            this.MouseMove += new MouseEventHandler(Paint_MouseMove);
            this.MouseLeave += new MouseEventHandler(Paint_MouseLeave);
            
        }

        private void Paint_MouseLeave(object sender, MouseEventArgs e)
        {
            if (selectMode)
            {
                ResizeAdorners.Instance.StopDragging();
                e.Handled = true;
            }
        }


        void DragFinishedMouseHandler(object sender, MouseButtonEventArgs e)
        {
            if (selectMode)
            {
                ResizeAdorners.Instance.StopDragging();
                e.Handled = true;
            }
        }

        void Paint_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectMode)
            {
                ResizeAdorners.Instance.Dragging(sender, e, DrawCanvas);

            }
        }

        private void Window1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(selectMode)
            {
                ResizeAdorners.Instance.Deselect();
            }
        }

        // Bắt sự kiện nhấn chuột để bắt đầu vẽ hoặc select.
        private void DrawCanvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectMode)
            {
                ResizeAdorners.Instance.Deselect();
                ResizeAdorners.Instance.Select(sender, e, DrawCanvas);
            }
            else
            {
                _count = DrawCanvas.Children.Count;
                _shape.isDrawing = true;
                _justAddedShape = false;
                _shape.startPoint = e.GetPosition(sender as IInputElement);
            }
        }

        // Bắt sự kiện di chuyển chuột trên canvas
        private void DrawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_shape.isDrawing)
            {
                if (_justAddedShape)
                {
                    if (DrawCanvas.Children.Count > 0)
                    {
                        DrawCanvas.Children.RemoveAt(DrawCanvas.Children.Count - 1);
                    }
                }
                _shape.endPoint = e.GetPosition(sender as IInputElement);
                _shape.Draw();
                DrawCanvas.Children.Add(_shape.shape);
                _justAddedShape = true;
                DrawCanvas.CaptureMouse();

            }
        }

        // kết thúc việc vẽ trên canvas
        private void DrawCanvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_shape.isDrawing)
            {
                if (DrawCanvas.Children.Count > 0 && DrawCanvas.Children.Count != _count)
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

        ////thay đổi giữa các hình ảnh.
        //private void radioLine_Checked(object sender, RoutedEventArgs e)
        //{
        //    _shape = new DrawLine();
        //}

        //private void radioRectangle_Checked(object sender, RoutedEventArgs e)
        //{
        //    _shape = new DrawRectangle();
        //}

        //private void radioEllipse_Checked(object sender, RoutedEventArgs e)
        //{
        //    _shape = new DrawEllipse();
        //}


        //thay đổi kích thước nét vẽ.
        //private void SizeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var comboBox = sender as ComboBox;

        //    int value = comboBox.SelectedIndex;
        //    switch (value)
        //    {
        //        case 0:
        //            _shape.SetStroke(3);
        //            break;
        //        case 1:
        //            _shape.SetStroke(5);
        //            break;
        //        case 2:
        //            _shape.SetStroke(7);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        ////thay đổi màu nét vẽ.
        //private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        //{
        //    _shape.SetStrokeColor(ClrPcker_Background.SelectedColor.Value);
        //}

        ////thay đổi màu bên trong hình chữ nhật, hình elip.
        //private void FillColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        //{
        //    _shape.SetFillColor(FillColorPicker.SelectedColor.Value);
        //}


        ////thây đổi nét vẽ.
        //private void dashStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var comboBox = sender as ComboBox;

        //    int value = comboBox.SelectedIndex;

        //    switch (value)
        //    {
        //        case 0:
        //            _shape.SetStrokeStyle(new DoubleCollection() {1,0});
        //            break;
        //        case 1:
        //            _shape.SetStrokeStyle(new DoubleCollection() { 2 });
        //            break;
        //        case 2:
        //            _shape.SetStrokeStyle(new DoubleCollection() { 3,1,1,1 });
        //            break;
        //        default:
        //            break;
        //    }
        //}

        ////chế độ select đang bật
        //private void Select_Checked(object sender, RoutedEventArgs e)
        //{
        //    selectMode = true;
        //}

        ////chế độ select tắt.
        //private void Select_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    selectMode = false;
        //}
    }
}
