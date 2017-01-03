using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.ExcelAddIn.Helpers;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class DataBlocksControl : PaneControl, IDataBlockView
    {
        public event EventHandler<SelectEntityEventArgs> DataBlockSelected;
        public event EventHandler<SelectEntityEventArgs> NewDataBlockRequested;
        public event EventHandler<SelectEntityEventArgs> DataBlockDeleted;
        public event EventHandler<SelectEntityEventArgs> DataBlockCopied;

        public DataBlocksControl(PanesManager embedder) : base(embedder, "Блоки данных")
        {
            InitializeComponent();
        }

        public BaseDataBlock SelectedBlock
        {
            get
            {
                return (BaseDataBlock)treeViewSheetsAndBlocks.SelectedNode.Tag;
            }
            set
            {
                var nodeToSelect = treeViewSheetsAndBlocks
                    .Nodes
                    .OfType<TreeNode>()
                    .SelectMany(n => GetNodeBranch(n))
                    .SingleOrDefault(t => t.Tag == value);
                if (nodeToSelect != null)
                {
                    treeViewSheetsAndBlocks.SelectedNode = nodeToSelect;
                }
            }
        }

        private IEnumerable<TreeNode> GetNodeBranch(TreeNode node)
        {
            yield return node;

            foreach (TreeNode child in node.Nodes)
                foreach (var childChild in GetNodeBranch(child))
                    yield return childChild;
        }

        private void AddNodes(TreeNode newNode, TreeNode parent)
        {

        }

        public void RefreshDataBlockView(IEnumerable<BaseDataBlock> dataBlocks)
        {
            treeViewSheetsAndBlocks.Nodes.Clear();
            var sheetNodes = new List<TreeNode>();

            foreach(var dcb in dataBlocks.OfType<CollectionDataBlock>())
            {
                var sheetNode = sheetNodes.SingleOrDefault(n => n.Text == dcb.Sheet);
                if(sheetNode == null)
                {
                    sheetNode = new TreeNode(dcb.Sheet);
                    sheetNodes.Add(sheetNode);
                }

                sheetNode.Nodes.Add(GetCollectionTreeNode(dcb));
            }
            var root = treeViewSheetsAndBlocks.Nodes.Add("Эта книга");
            root.Nodes.AddRange(sheetNodes.ToArray());
            foreach(var n in sheetNodes)
            {
                n.Expand();
            }
        }

        public TreeNode GetCollectionTreeNode(CollectionDataBlock dataBlock)
        {
            var newNode = new TreeNode(dataBlock.Name)
            {
                Tag = dataBlock
            };
            newNode.Nodes.AddRange(dataBlock.DataBlocks.Select(db => new TreeNode(db.Name) { Tag = db }).ToArray());
            return newNode;
        }

        //public void RefreshDataBlockView2(IEnumerable<BaseDataBlock> dataBlocks)
        //{
        //    treeViewSheetsAndBlocks.Nodes.Clear();
            
        //    var newNodes = new List<TreeNode>();
        //    var root = treeViewSheetsAndBlocks.Nodes.Add("Эта книга");
        //    newNodes.Add(root);
                                                
        //    foreach(var f in dataBlocks)
        //    {
        //        var newNode = new TreeNode(f.Name)
        //        {
        //            Tag = f
        //        };

        //        if (f.Parent == null)
        //        {
        //            root.Nodes.Add(newNode);                    
        //        }
        //        else
        //        {
        //            var parent = newNodes.SingleOrDefault(n => n.Tag == f.Parent);
        //            if(parent == null)
        //            {
        //                // ?
        //            }
        //            else
        //            {
        //                parent.Nodes.Add(newNode);
        //            }
        //        }
        //        newNodes.Add(newNode);
        //    }

        //    treeViewSheetsAndBlocks.ExpandAll();
        //}

        private void EditNode(TreeNode node)
        {
            if (node.Tag is DataBlock)
            {
                DataBlockSelected?.Invoke(this, new SelectEntityEventArgs
                {
                    Block = (DataBlock)node.Tag
                });
            }
        }

        private void treeViewFragments_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            EditNode(e.Node);
        }

        private void linkLabelAddFragment_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //if (treeViewFragments.SelectedNode?.Tag is NamedCollection)
            //{
            //    NewFragmentRequested?.Invoke(this, new SelectEntityEventArgs
            //    {
            //        SourceCollection = (NamedCollection)treeViewFragments.SelectedNode.Tag
            //    });
            //}
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EditNode(treeViewSheetsAndBlocks.SelectedNode);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (treeViewSheetsAndBlocks.SelectedNode?.Tag is DataBlock)
            {
                DataBlockDeleted?.Invoke(this, new SelectEntityEventArgs
                {
                    Block = (DataBlock)treeViewSheetsAndBlocks.SelectedNode.Tag
                });
            }

        }

        private void linkLabelCopyFragment_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (treeViewSheetsAndBlocks.SelectedNode?.Tag is DataBlock)
            {
                //FragmentCopied?.Invoke(this, new SelectEntityEventArgs
                //{
                //    Fragment = (Fragment)treeViewFragments.SelectedNode.Tag,
                //    SourceCollection = (NamedCollection)treeViewFragments.SelectedNode.Parent.Tag
                //});
            }
        }

    }
}
