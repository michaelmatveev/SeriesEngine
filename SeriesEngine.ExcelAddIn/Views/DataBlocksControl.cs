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
        public event EventHandler<SelectEntityEventArgs> CollectionDataBlockSelected;
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
                    nodeToSelect.EnsureVisible();
                    treeViewSheetsAndBlocks.SelectedNode = nodeToSelect;
                    treeViewSheetsAndBlocks.Focus();
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
            var state = treeViewSheetsAndBlocks.Nodes.GetExpansionState();
            treeViewSheetsAndBlocks.BeginUpdate();
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
                //sheetNode.Expand();
                sheetNode.Nodes.Add(GetCollectionTreeNode(dcb));
            }
            var root = treeViewSheetsAndBlocks.Nodes.Add("Эта книга");
            root.Nodes.AddRange(sheetNodes.ToArray());
            treeViewSheetsAndBlocks.Nodes.SetExpansionState(state);

            treeViewSheetsAndBlocks.EndUpdate();
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
            DataBlockSelected?.Invoke(this, new SelectEntityEventArgs
            {
                Block = (BaseDataBlock)node.Tag
            });

            //if (node.Tag is DataBlock)
            //{
            //    DataBlockSelected?.Invoke(this, new SelectEntityEventArgs
            //    {
            //        Block = (DataBlock)node.Tag
            //    });
            //}
            //else if(node.Tag is CollectionDataBlock)
            //{
            //    CollectionDataBlockSelected?.Invoke(this, new SelectEntityEventArgs
            //    {
            //        SourceCollection = (CollectionDataBlock)node.Tag
            //    });
            //}
        }

        private void treeViewFragments_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            EditNode(e.Node);
        }

        private void linkLabelDeleteDataCollection_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(treeViewSheetsAndBlocks.SelectedNode?.Tag is CollectionDataBlock)
            {
                DataBlockDeleted?.Invoke(this, new SelectEntityEventArgs
                {
                    SourceCollection = (CollectionDataBlock)treeViewSheetsAndBlocks.SelectedNode.Tag
                });
            }
        }

        private void linkLabelAddDataBlock_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(treeViewSheetsAndBlocks.SelectedNode?.Tag is CollectionDataBlock)
            {
                NewDataBlockRequested?.Invoke(this, new SelectEntityEventArgs
                {
                    SourceCollection = (CollectionDataBlock)treeViewSheetsAndBlocks.SelectedNode.Tag
                });
            }
        }

    }
}
