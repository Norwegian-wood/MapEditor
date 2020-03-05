using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    public class TreeViewItem
    {
        public string Icon { get; set; }
        public string DisplayMapName { set; get; }
        public string mapName { set; get; }
        public List<TreeViewItem> Children { set; get; }
        public TreeViewItem parent { set; get; }
        public TreeViewItem()
        {
            Children = new List<TreeViewItem>();
        }
    }
}
