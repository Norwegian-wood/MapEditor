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


namespace MapEditor
{
    /// <summary>
    /// TitleItem.xaml 的交互逻辑
    /// </summary>
    public partial class TitleItem : UserControl
    {
        public TitleItem()
        {
            InitializeComponent();
            InitUI();
        }
        public void InitUI()
        {
            DataManager manager = DataManager.Get();
            //this.stackPanel.Children.Add(new ItemStaticKey());
            for (int i = manager.staticKeyNum; i < manager.AllKey.Count; i++)
            {
                ItemDynamicKey item = new ItemDynamicKey();
                item.Init(manager.AllKey[i]);
                this.stackPanel.Children.Add(item);
            }
        }
    }
}
