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
using System.Collections;
using Data;

namespace MapEditor
{
    /// <summary>
    /// WRemove.xaml 的交互逻辑
    /// </summary>
    public partial class WRemove : Window
    {
        private MainWindow window;
        private List<string> KeyRemove = new List<string>();
        public WRemove(MainWindow Inwindow)
        {
            window = Inwindow;
            DataManager manager = DataManager.Get();
            for (int i = 0; i < manager.AllKey.Count; i++)
            {
                if (manager.AllKeyArrtibute[i] == KeyAtrribute.dynamic)
                {
                    KeyRemove.Add(manager.AllKey[i] as string);
                }
            }
            InitializeComponent();
            this.RemovecomboBox.ItemsSource = KeyRemove;
           
        }

        private void RemoveKey_Click(object sender, RoutedEventArgs e)
        {
            DataManager manager = DataManager.Get();
            int index = -1;
            for (int i = 0; i < manager.AllKey.Count; i++)
            {
                if (manager.AllKey[i] == KeyRemove[RemovecomboBox.SelectedIndex])
                {
                    index = i;
                    manager.AllKey.RemoveAt(i);
                    break;
                }
            }
            if (index != -1)
            {
                foreach (var pair1 in manager.AllMapData)
                {
                    foreach (var pair2 in pair1.Value)
                    {
                        foreach (var row in pair2.Value)
                        {
                            row.RemoveAt(index);
                        }
                    }
                }
            }
            window.InitAll();
        }
    }
}
