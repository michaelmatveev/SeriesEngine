using System.Collections.Generic;

namespace SeriesEngine.Core.DataAccess
{
    public class TreeHierarchy
    {
        private List<TreeNode> _nodes;
        public TreeHierarchy()
        {
            _nodes = new List<TreeNode>();
        }

        public IEnumerable<TreeNode> AllNodes
        {
            get
            {
                return _nodes;
            }
        }

        public void Add(TreeNode node)
        {
            _nodes.Add(node);
        }

    }
}
