using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.Core.DataAccess
{
    public class TreeNode
    {
        public string Key { get; set; }
        public string ParentKey
        {
            get
            {
                return Parent?.Key;
            }
        }

        public TreeNode Parent { get; set; }
        private List<TreeNode> _children = new List<TreeNode>();

        public void AddChild(TreeNode node)
        {
            _children.Add(node);
        }

        public IEnumerable<TreeNode> Children
        {
            get
            {
                return _children;
            }
        }

        public object Value { get; set; }
    }
}
