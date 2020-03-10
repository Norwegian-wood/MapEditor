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
using System.Windows.Shapes;

namespace MapEditor
{
    /// <summary>
    /// WRemindSave.xaml 的交互逻辑
    /// </summary>
    public partial class WRemindSave : Window
    {
        MainWindow window;
        public WRemindSave(MainWindow InWindow)
        {
            InitializeComponent();
            window = InWindow;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            window.SaveCurrentData(window.GetDetailData());
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
