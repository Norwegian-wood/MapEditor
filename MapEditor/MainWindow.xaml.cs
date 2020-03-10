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

            //manager.PreInitMainUIData();
            //manager.InitMainUIData();
            for (int i = 0; i < manager.AllMapData[manager.currentResourceName][manager.currentMapName].Count;i++) 
            {
                DetailItem item = new DetailItem(i);
                DetailPanel.Children.Add(item);
            }
            SetDetailData(manager.AllMapData[manager.currentResourceName][manager.currentMapName]);

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
            List<List<string>> data;
            if (CheckData(out data))
            {

            }
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

        private void SaveData_Click(object sender, RoutedEventArgs e)
        {
            List<List<string>> data;
            if (CheckData(out data))
            {
                SaveCurrentData(data);
            }
        }
        public void SaveCurrentData(List<List<string>> data)
        {
            manager.AllMapData[manager.currentResourceName][manager.currentMapName] = data;
            manager.WriteAMap(manager.currentResourceName, manager.currentMapName);
        }
        private bool CheckData(out List<List<string>> data)
        {
            data  = GetDetailData();
            bool ifChange = false;
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[i].Count; j++)
                {
                    if (data[i][j] != manager.AllMapData[manager.currentResourceName][manager.currentMapName][i][j])
                    {
                        ifChange = true;
                        break;
                    }
                }
            }
            return ifChange;
        }
        public List<List<string>> GetDetailData()
        {
            List<List<string>> data = new List<List<string>>();
            for (int i = 1; i < DetailPanel.Children.Count; i++)
            {
                List<string> row = new List<string>();
                DetailItem item = DetailPanel.Children[i] as DetailItem;

                foreach(var iter in item.panel.Children)
                {
                    TextBox box = null;
                    ComboBox combo = null;
                    ItemDynamicDetail DyDetail = null;
                    box = iter as TextBox;
                    combo = iter as ComboBox;
                    DyDetail = iter as ItemDynamicDetail;
                    if (box != null)
                    {
                        row.Add(box.Text);
                    }
                    else if (combo != null)
                    {
                        row.Add(combo.Text);
                    }
                    else if (DyDetail != null)
                    {
                        row.Add(DyDetail.content.Text);
                    }
                }
                data.Add(row);
            }
            return data;
        }
        private void SetDetailData(List<List<string>> data)
        {
            for (int i = 1; i < DetailPanel.Children.Count; i++)
            {
                DetailItem row = DetailPanel.Children[i] as DetailItem;
                int index = 0;
                foreach (var item in row.panel.Children)
                {
                    TextBox box = null;
                    ComboBox combo = null;
                    ItemDynamicDetail DyDetail = null;
                    string text = data[i - 1][index];
                    box = item as TextBox;
                    combo = item as ComboBox;
                    DyDetail = item as ItemDynamicDetail;
                    if (box != null)
                    {
                        box.Text = text;
                        index++;
                    }
                    else if (combo != null)
                    {
                        combo.Text = text;
                        index++;
                    }
                    else if (DyDetail != null)
                    {
                        DyDetail.content.Text = text;
                        index++;
                    }
                }
            }
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
