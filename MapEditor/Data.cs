using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace Data
{
    public enum KeyAtrribute { staticReadOnly,staticKey,dynamic}
    public class DataManager
    {
        static private DataManager _Manager = new DataManager();
        static public DataManager Get() { return _Manager; }

        private string GameDir = "";
        private string MapDir = "";
        public string currentResourceName = "dz_Map";
        public string currentMapName = "dz_Map";


        public int staticKeyNum = 8;
        public Dictionary<string, Dictionary<string, List<List<string>>>> AllMapData = new Dictionary<string, Dictionary<string, List<List<string>>>>();
        public List<string> AllKey = new List<string>();
        public List<KeyAtrribute> AllKeyArrtibute = new List<KeyAtrribute>();
        public List<DetaiUIlData> allDetailUIData = new List<DetaiUIlData>();
        //private Dictionary<string, string> TreeAtributes = new Dictionary<string, string>();
        // UIData uiData;
        public DataManager()
        {
            for (int i = 0; i < staticKeyNum; i++)
            {
                AllKey.Add("StaticKey");
                AllKeyArrtibute.Add(KeyAtrribute.staticKey);
                
            }
            InitConfig();
            //uiData = new UIData();
        }
        //保存与初始化
        public void Init()
        {
            
            //AllMapData.Add("dz_Map", new Dictionary<string, List<List<string>>>());
            ////MapAtrributes["dz_Map"].Add("dz_Map", new Dictionary<string, string>());
            //AllMapData["dz_Map"].Add("dz_Map", new List<List<string>>());
            //AllMapData["dz_Map"].Add("101", new List<List<string>>());
            //// MapAtrributes["dz_Map"]["dz_Map"].Add("TestKey1", "TestValue1");

            ////MapAtrributes["dz_Map"]["101"].Add("TestKey1", "TestValue2");
            //AllMapData.Add("dz_Map_Main2", new Dictionary<string, List<List<string>>>());
            ////MapAtrributes["dz_Map_Main2"].Add("dz_Map_Main2", new Dictionary<string, string>());
            //AllMapData["dz_Map_Main2"].Add("201", new List<List<string>>());
            //AllMapData["dz_Map_Main2"].Add("dz_Map_Main2", new List<List<string>>());
            ////MapAtrributes["dz_Map_Main2"]["dz_Map_Main2"].Add("TestKey1", "TestValue3");
            ////MapAtrributes["dz_Map_Main2"]["201"].Add("TestKey1", "TestValue4");
            //for (int j = 0; j < 4; j++)
            //{
            //    List<string> temp1 = new List<string>();
            //    List<string> temp2 = new List<string>();
            //    for (int i = 0; i < 9; i++)
            //    {
            //        temp1.Add(string.Format("dz_MapValue:{0:D},{0:D}", j, i));
            //        temp2.Add(string.Format("dz_Map_Main2:{0:D},{0:D}", j, i));
            //    }
            //    AllMapData["dz_Map"]["dz_Map"].Add(temp1);
            //    AllMapData["dz_Map_Main2"]["dz_Map_Main2"].Add(temp2);
            //}
            ReadAll();
            //treeMap.ItemsSource;
        }
        private void InitConfig()
        {
            string workDir = Directory.GetCurrentDirectory();
            string configPath = workDir + "\\config.txt";
            if (!File.Exists(configPath))
            {
                MessageBox.Show("配置文件损坏");
                Environment.Exit(0);
                //Application.Current.Shutdown();
            }
            StreamReader sr = File.OpenText(configPath);
            string dirLine = sr.ReadLine();
            if (!Directory.Exists(dirLine) ||!Directory.Exists(dirLine + "\\ZhuxianClient"))
            {
                MessageBox.Show("游戏目录设置错误");
                Environment.Exit(0);
            }
            this.GameDir = dirLine;
            this.MapDir = dirLine + "\\ZhuxianClient\\gamedata\\mapdata";
            string keyLine = sr.ReadLine();
           
            string[] keys = keyLine.Split(' ');
            AllKey.Clear();
            foreach (string str in keys)
            {
                AllKey.Add(str);
            }
            staticKeyNum = AllKey.Count;
        }

        public void ReadAll()
        {
            ReadKey();
            foreach (string Resourcedir in Directory.EnumerateDirectories(this.MapDir))
            {
                string ResourceName = Path.GetFileName(Resourcedir);
                ReadInternal(ResourceName, ResourceName);
                foreach (string mapDir in Directory.EnumerateDirectories(Resourcedir))
                {
                    string mapName = Path.GetFileName(mapDir);
                    ReadInternal(ResourceName, mapName);
                }
            }
        }
        public void ReadKey()
        {
            for (int i = AllKey.Count -1; i >=staticKeyNum ; i--)
            {
                AllKey.RemoveAt(i);
            }
            bool readSuccess = false;
            foreach (string Resourcedir in Directory.EnumerateDirectories(this.MapDir))
            {
                string mapsetting = Resourcedir + "\\mapsetting.json";
                if (File.Exists(mapsetting))
                {
                    string jsStr = File.ReadAllText(mapsetting);
                    JArray job = JArray.Parse(jsStr);
                    foreach (var pair in (job[0] as JObject))
                    {
                        if (!AllKey.Contains(pair.Key))
                        {
                            AllKey.Add(pair.Key);
                        }
                    }
                    readSuccess = true;
                    break;
                }
            }
            if (readSuccess == false)
            {
                MessageBox.Show(string.Format("读取地图数据完全失败"));
                return;
            }
        }
        public void ReadCurrent()
        {
            ReadKey();
            ReadInternal(currentResourceName, currentMapName);
        }
        private void ReadInternal(string ResourceName,string MapName)
        {
            string path = "";
            if (ResourceName == MapName)
            {
                path = MapDir + "\\"+ ResourceName + "\\mapsetting.json";
            }
            else
            {
                path = MapDir + "\\" + ResourceName + "\\" + MapName+ "\\mapsetting.json";
            }
            if (!AllMapData.ContainsKey(ResourceName))
            {
                AllMapData.Add(ResourceName, new Dictionary<string, List<List<string>>>());
            }
            AllMapData[ResourceName].Remove(MapName);
            AllMapData[ResourceName].Add(MapName, new List<List<string>>());
            if (File.Exists(path))
            {
                string jsStr = File.ReadAllText(path);
                JArray jar = JArray.Parse(jsStr);
                foreach (JObject iter in jar)
                {
                    AllMapData[ResourceName][MapName].Add(new List<string>());
                    foreach (string key in AllKey)
                    {
                        if (!iter.ContainsKey(key))
                        {
                            MessageBox.Show(string.Format("键值不存在:" + key));
                            Environment.Exit(0);
                        }
                        AllMapData[ResourceName][MapName][AllMapData[ResourceName][MapName].Count - 1].Add(iter[key].ToString());
                    }

                }
            }
            else if (ResourceName == MapName)
            {
            }
            else
            {

            }
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
        private void WriteAMap(string resourceName,string MapName)
        {
            string path = "";
            if (resourceName == MapName)
            {
                path = MapDir + "\\" + resourceName + "\\mapsetting.json";
            }
            else
            {
                path = MapDir + "\\" + resourceName + "\\" + MapName + "\\mapsetting.json";
            }
            JArray jar = new JArray();
            foreach (var iter in AllMapData[resourceName][MapName])
            {
                JObject job = new JObject();
                for (int i = 0; i < AllKey.Count; i++)
                {
                    job.Add(AllKey[i], iter[i]);
                }
                jar.Add(job);
            }
            //TODO:以覆盖的方式打开文件
            if (jar.Count != 0)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                StreamWriter sw = File.CreateText(path);
                sw.Write(jar.ToString());
                sw.Flush();
            }
        }
        public void WriteAll()
        {
            foreach (var pair in AllMapData)
            {
                foreach (var pair2 in pair.Value)
                {
                    WriteAMap(pair.Key, pair2.Key);
                }
            }
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