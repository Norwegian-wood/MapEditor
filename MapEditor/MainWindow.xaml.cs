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
        //UI;
        DataManager manager;
        //UI数据

        public ArrayList allDetailData = new ArrayList();
        public MainWindow()
        {
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
            treeMap.Items.Clear();
            int i = 0;
            foreach (var pair in manager.AllMapData)
            {
                treeMap.Items.Add(pair.Key);
                foreach (var pair2 in pair.Value)
                {
                    //(treeMap.Items[i] as TreeViewItem).Items.Add("test2");
                    //(treeMap.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem).Items.Add(pair2.Key);
                }
                i++;
            }
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
        private void treeMap_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = this.treeMap.SelectedItem as TreeViewItem;
            manager.currentResourceName = "dz_Map";
            manager.currentMapName = "dz_Map";
            //if (item != null)
            //{
            //    InitMainUI("dz_Map");
            //}
            //else
            //{
            //    InitMainUI("dz_Map_Main2");
            //}
            
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
