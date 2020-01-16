using GMap.NET.WindowsPresentation;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace GMAPNS
{
    /// <summary>
    /// UserMarker.xaml 的交互逻辑
    /// </summary>
    public partial class UserMarker : UserControl
    {
        Popup Popup;
        Label Label;
        GMapMarker Marker;
        GMapControl MainWindow;

        public UserMarker(GMapControl window, GMapMarker marker, string picPathName,string title)
        {
            this.InitializeComponent();

            this.MainWindow = window;
            this.Marker = marker;
            this.icon.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(picPathName, UriKind.RelativeOrAbsolute));


            Popup = new Popup();
            Label = new Label();

            this.Unloaded += CustomMarkerDemo_Unloaded;
            this.Loaded += CustomMarkerDemo_Loaded;
            this.SizeChanged += CustomMarkerDemo_SizeChanged;
            this.MouseEnter += MarkerControl_MouseEnter;
            this.MouseLeave += MarkerControl_MouseLeave;
            this.MouseDoubleClick += UserMarker_MouseDoubleClick;

            Popup.Placement = PlacementMode.Mouse;
            {
                Label.Background = Brushes.WhiteSmoke;
                Label.Foreground = Brushes.Black;
                Label.BorderBrush = Brushes.Gray;
                Label.BorderThickness = new Thickness(1);
                Label.Padding = new Thickness(6);
                Label.FontSize = 10;
                Label.Content = title;
            }
            Popup.Child = Label;
        }
        private void UserMarker_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //HidePopup();
        }




        void CustomMarkerDemo_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == icon.Source) return;

            if (icon.Source.CanFreeze)
            {
                icon.Source.Freeze();
            }
        }

        void CustomMarkerDemo_Unloaded(object sender, RoutedEventArgs e)
        {
            Marker.Shape = null;
            icon.Source = null;
            icon = null;
            Popup = null;
            Label = null;
        }

        void CustomMarkerDemo_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Marker.Offset = new Point(-e.NewSize.Width / 2, -e.NewSize.Height);
        }

        void CustomMarkerDemo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured)
            {
                Point p = e.GetPosition(MainWindow);
                Marker.Position = MainWindow.FromLocalToLatLng((int)(p.X), (int)(p.Y));
            }
        }

        void MarkerControl_MouseLeave(object sender, MouseEventArgs e)
        {
            HidePopup();
        }
        void HidePopup()
        {
            Marker.ZIndex -= 10000;
            Popup.IsOpen = false;
        }
        void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Marker.ZIndex += 10000;
            Popup.IsOpen = true;
        }
    }
}
