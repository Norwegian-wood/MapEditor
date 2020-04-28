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
namespace MapEditor
{
    /// <summary>
    /// WModify.xaml 的交互逻辑
    /// </summary>
    public partial class WModify : Window
    {
        MainWindow window;
        List<string> KeyModify =new List<string>();
        public WModify(MainWindow inMainWindow)
        {
            InitializeComponent();
            window = inMainWindow;
            DataManager manager = DataManager.Get();
            for (int i = 0; i < manager.AllKey.Count; i++)
            {
                if (manager.AllKeyArrtibute[i] != KeyAtrribute.staticReadOnly)
                {
                    KeyModify.Add(manager.AllKey[i] as string);
                }
            }
            comboBox.ItemsSource = KeyModify;
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            DataManager manager = DataManager.Get();
            int index = -1;
            for (int i = 0; i < manager.AllKey.Count; i++)
            {
                if (manager.AllKey[i] == KeyModify[comboBox.SelectedIndex])
                {
                    index = i;
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
                            row[index] = modifyValue.Text;
                        }
                    }
                }
            }
            window.InitAll();
            manager.WriteAll();
        }

        private void Modify_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
