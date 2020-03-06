using System;
using System.Collections;
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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DataManager manager;
        //UI;
        public List<TreeViewItem> root { get; set; }

        //UI数据

        public ArrayList allDetailData = new ArrayList();
        public MainWindow()
        {
            this.DataContext = this;
            root = new List<TreeViewItem>();
            InitializeComponent();
            manager = DataManager.Get();
            manager.Init();
            InitAll();
        }
        public void InitAll()
        {
            InitTree();
            InitMainUI();
        }
        public void InitTree()
        {
            root.Clear();
            int i = 0;
            foreach (var pair in manager.AllMapData)
            {
                TreeViewItem father = new TreeViewItem();
                father.DisplayMapName = pair.Key;
                foreach (var pair2 in pair.Value)
                {
                    if (pair2.Key != pair.Key)
                    {
                        TreeViewItem child = new TreeViewItem();
                        child.DisplayMapName = pair2.Key;
                        child.parent = father;
                        father.Children.Add(child);
                    }
                }
                root.Add(father);
                i++;
            }
            manager.currentMapName = manager.currentResourceName = manager.AllMapData.First().Key;
        }
        public void InitMainUI()
        {
            //上部
            mapName.Text = manager.currentMapName;
            //下部
            DetailPanel.Children.Clear();
            allDetailData.Clear();

          
            DetailPanel.Children.Add(new TitleItem());

            manager.PreInitMainUIData();
            //manager.InitMainUIData();
            for (int i = 0; i < manager.AllMapData[manager.currentResourceName][manager.currentMapName].Count;i++) 
            {
                DetailItem item = new DetailItem(i);
                DetailPanel.Children.Add(item);
            }
            manager.InitMainUIData();

        }
        private void InputAll_Click(object sender, RoutedEventArgs e)
        {
            InitAll();
        }

        private void AddKey_Click(object sender, RoutedEventArgs e)
        {
            WAdd windowAdd = new WAdd(this);
            windowAdd.ShowDialog();
        }
        private void RemoveKey_Click(object sender, RoutedEventArgs e)
        {
            WRemove windowRemove = new WRemove(this);
            windowRemove.ShowDialog();
        }
        private void ModifyData_Click(object sender, RoutedEventArgs e)
        {
            WModify windowModify = new WModify(this);
            windowModify.ShowDialog();
        }
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = this.treeView.SelectedItem as TreeViewItem;
            if (item.parent == null)
            {
                manager.currentResourceName = item.DisplayMapName;
            }
            else
            {
                manager.currentResourceName = item.parent.DisplayMapName;
            }
            manager.currentMapName = item.DisplayMapName;
            InitMainUI();
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < manager.AllKey.Count; i++)
            {
                list.Add("0");
            }
            manager.AllMapData[manager.currentResourceName][manager.currentMapName].Add(list);
            manager.WriteAll();
            this.InitMainUI();
        }

        private void inputCurrent_Click(object sender, RoutedEventArgs e)
        {
            manager.ReadCurrent();
            this.InitMainUI();
        }
    }
    //bindingData
    //public class MapName:INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler Handler;
    //    private string mapname;
    //    public string Name
    //    {
    //        get { return Name; }
    //        set {
    //            Name = mapname;
    //            Handler.Invoke(this, new PropertyChangedEventArgs("Name"));
    //        }
    //    }
    //}

}
