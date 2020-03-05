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
using Data;
using System.Collections;

namespace MapEditor
{
    /// <summary>
    /// WAdd.xaml 的交互逻辑
    /// </summary>
    public partial class WAdd : Window
    {
        MainWindow window;
        public WAdd(MainWindow InWindow)
        {
            window = InWindow;
            InitializeComponent();
        }

        private void AddKey_Click(object sender, RoutedEventArgs e)
        {
            DataManager manager = DataManager.Get();
            foreach (string key in manager.AllKey)
            {
                if (key == NewKey.Text)
                {
                    MessageBox.Show("字段已经存在");
                    return;
                }
            }
            manager.AllKey.Add(NewKey.Text);
            manager.AllKeyArrtibute.Add(KeyAtrribute.dynamic);
            foreach (var pair1 in manager.AllMapData)
            {
                foreach (var pair2 in pair1.Value)
                {
                    foreach (var row in pair2.Value)
                    {
                        row.Add(NewValue.Text);
                    }
                }
            }
            //TODO:是否需要保存按钮
            manager.WriteAll();
            window.InitAll();
        }
    }
}
