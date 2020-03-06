using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Data;

namespace MapEditor
{
    /// <summary>
    /// DetailItem.xaml 的交互逻辑
    /// </summary>
    public partial class DetailItem : UserControl
    {
        int index;
        public DetailItem(int Inindex)
        {
            index = Inindex;
            InitializeComponent() ;
            InitUI(this.index);
        }
        public DetailItem()
        {
            index = -1;
            InitializeComponent();
            InitUI(this.index);
        }
        public void InitUI(int lineIndex = 0)
        {
            DataManager manager = DataManager.Get();
            if (index > 0)
            {
                this.tmplID.SetBinding(TextBlock.TextProperty, new Binding("tmplID") { Source = manager.allDetailUIData[index] });
                this.min.SetBinding(TextBlock.TextProperty, new Binding("min") { Source = manager.allDetailUIData[index] });
                this.max.SetBinding(TextBlock.TextProperty, new Binding("max") { Source = manager.allDetailUIData[index] });
                for (int i = manager.staticKeyNum; i < manager.AllKey.Count; i++)
                {
                    ItemDynamicDetail item = new ItemDynamicDetail();
                    //item.Init(manager.AllMapData[manager.currentResourceName][manager.currentMapName][lineIndex][i]);
                    this.panel.Children.Add(item);
                }
            }
            else
            {
                for (int i = manager.staticKeyNum; i < manager.AllKey.Count; i++)
                {
                    ItemDynamicDetail item = new ItemDynamicDetail();
                    //item.Init(manager.AllMapData[manager.currentResourceName][manager.currentMapName][lineIndex][i]);
                    this.panel.Children.Add(item);
                }
            }
        }
    }

}
