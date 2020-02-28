using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;

namespace Data
{
    public enum KeyAtrribute { staticReadOnly,staticKey,dynamic}
    public class DataManager
    {
        static private DataManager _Manager = new DataManager();
        static public DataManager Get() { return _Manager; }


        public string currentResourceName = "dz_Map";
        public string currentMapName = "dz_Map";


        public int staticKeyNum = 8;
        public Dictionary<string, Dictionary<string, List<List<string>>>> AllMapData = new Dictionary<string, Dictionary<string, List<List<string>>>>();
        public List<string> AllKey = new List<string>();
        public List<KeyAtrribute> AllKeyArrtibute = new List<KeyAtrribute>();
        public List<DetaiUIlData> allDetailUIData = new List<DetaiUIlData>();
        private Dictionary<string, string> TreeAtributes = new Dictionary<string, string>();
        // UIData uiData;
        public DataManager()
        {
            for (int i = 0; i < staticKeyNum; i++)
            {
                AllKey.Add("StaticKey");
                AllKeyArrtibute.Add(KeyAtrribute.staticKey);
                
            }

            //uiData = new UIData();
        }
        //保存与初始化
        public void Init()
        {
            
            TreeAtributes.Add("dz_Map", "101");

            AllMapData.Add("dz_Map", new Dictionary<string, List<List<string>>>());
            //MapAtrributes["dz_Map"].Add("dz_Map", new Dictionary<string, string>());
            AllMapData["dz_Map"].Add("dz_Map", new List<List<string>>());
            AllMapData["dz_Map"].Add("101", new List<List<string>>());
            // MapAtrributes["dz_Map"]["dz_Map"].Add("TestKey1", "TestValue1");

            //MapAtrributes["dz_Map"]["101"].Add("TestKey1", "TestValue2");
            AllMapData.Add("dz_Map_Main2", new Dictionary<string, List<List<string>>>());
            //MapAtrributes["dz_Map_Main2"].Add("dz_Map_Main2", new Dictionary<string, string>());
            AllMapData["dz_Map_Main2"].Add("201", new List<List<string>>());
            AllMapData["dz_Map_Main2"].Add("dz_Map_Main2", new List<List<string>>());
            //MapAtrributes["dz_Map_Main2"]["dz_Map_Main2"].Add("TestKey1", "TestValue3");
            //MapAtrributes["dz_Map_Main2"]["201"].Add("TestKey1", "TestValue4");
            for (int j = 0; j < 4; j++)
            {
                List<string> temp1 = new List<string>();
                List<string> temp2 = new List<string>();
                for (int i = 0; i < 9; i++)
                {
                    temp1.Add(string.Format("dz_MapValue:{0:D},{0:D}", j, i));
                    temp2.Add(string.Format("dz_Map_Main2:{0:D},{0:D}", j, i));
                }
                AllMapData["dz_Map"]["dz_Map"].Add(temp1);
                AllMapData["dz_Map_Main2"]["dz_Map_Main2"].Add(temp2);
            }

            //treeMap.ItemsSource;
        }
        public void PreInitMainUIData()
        {
            allDetailUIData.Clear();
            for (int i = 0; i < AllMapData[currentResourceName][currentMapName].Count; i++)
            {
                allDetailUIData.Add(new DetaiUIlData());
            }
        }
        public void InitMainUIData()
        {
            for (int i = 0; i < AllMapData[currentResourceName][currentMapName].Count; i++)
            {
                allDetailUIData[i].Init(AllMapData[currentResourceName][currentMapName][i]);
            }
        }
        public void SaveAMap(string MapName)
        {

        }
        public void SaveAll()
        {

        }
        //增删字段
    }
    public class DetaiUIlData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string blockNum ="1";
        public string min ="1";
        public string max = "1";
        public string file= "1";
        public string locationX = "1";
        public string locationY = "1";
        public ArrayList dynamicValue;
        public DetaiUIlData()
        {

        }
        public void Init(List<string> InData)
        {
            blockNum = InData[1] as string;
            min = InData[2] as string;
            max = InData[3] as string;
        }
        public string BlockNum
        {
            get { return blockNum; }
            set
            {
                blockNum = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("blockNum"));
                }
            }

        }
        public string Min
        {
            get { return min; }
            set
            {
                min = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Min"));
                }
            }
        }

        public string Max
        {
            get { return Max; }
            set
            {
                min = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Max"));
                }
            }
        }

    }
}