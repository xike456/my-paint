using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Paint.Shape;

namespace Paint
{
    class Control
    {
        public bool selectMode = false;

        //chế độ select đang bật
        private void Select_Checked(object sender, RoutedEventArgs e)
        {
            selectMode = true;
        }

        //chế độ select tắt.
        private void Select_Unchecked(object sender, RoutedEventArgs e)
        {
            selectMode = false;
        }

        //thây đổi nét vẽ.
        private void dashStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            int value = comboBox.SelectedIndex;

            switch (value)
            {
                case 0:
                    _shape.SetStrokeStyle(new DoubleCollection() { 1, 0 });
                    break;
                case 1:
                    _shape.SetStrokeStyle(new DoubleCollection() { 2 });
                    break;
                case 2:
                    _shape.SetStrokeStyle(new DoubleCollection() { 3, 1, 1, 1 });
                    break;
                default:
                    break;
            }
        }

        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            _shape.SetStrokeColor(ClrPcker_Background.SelectedColor.Value);
        }

        //thay đổi màu bên trong hình chữ nhật, hình elip.
        private void FillColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            _shape.SetFillColor(FillColorPicker.SelectedColor.Value);
        }


        private void SizeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            int value = comboBox.SelectedIndex;
            switch (value)
            {
                case 0:
                    _shape.SetStroke(3);
                    break;
                case 1:
                    _shape.SetStroke(5);
                    break;
                case 2:
                    _shape.SetStroke(7);
                    break;
                default:
                    break;
            }
        }


        //thay đổi giữa các hình ảnh.
        private void radioLine_Checked(object sender, RoutedEventArgs e)
        {
            _shape = new DrawLine();
        }

        private void radioRectangle_Checked(object sender, RoutedEventArgs e)
        {
            _shape = new DrawRectangle();
        }

        private void radioEllipse_Checked(object sender, RoutedEventArgs e)
        {
            _shape = new DrawEllipse();
        }
    }
}
