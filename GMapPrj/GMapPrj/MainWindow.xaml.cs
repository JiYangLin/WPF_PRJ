using GMapNS;
using System.Windows;

namespace GMapPrj
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        GMapUse m_GMapUse = new GMapUse();
        public static MainWindow obj = null;
        public MainWindow()
        {
            InitializeComponent();
            obj = this;
            m_GMapUse.Init(mapControl);
        }
    }
}
