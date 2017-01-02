﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.ExcelAddIn.Helpers;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class FragmentsControl : PaneControl, IFragmentView
    {
        public event EventHandler<SelectEntityEventArgs> FragmentSelected;
        public event EventHandler<SelectEntityEventArgs> NewFragmentRequested;
        public event EventHandler<SelectEntityEventArgs> FragmentDeleted;
        public event EventHandler<SelectEntityEventArgs> FragmentCopied;

        public FragmentsControl(PanesManager embedder) : base(embedder, "Блоки данных")
        {
            InitializeComponent();
        }

        private void AddNodes(TreeNode newNode, TreeNode parent)
        {

        }

        public void RefreshFragmentsView(IEnumerable<BaseDataBlock> fragments)
        {
            treeViewFragments.Nodes.Clear();
            var newNodes = new List<TreeNode>();
            var root = treeViewFragments.Nodes.Add("Эта книга");
            newNodes.Add(root);
            
            foreach(var f in fragments)
            {
                var newNode = new TreeNode(f.Name)
                {
                    Tag = f
                };

                if (f.Parent == null)
                {
                    root.Nodes.Add(newNode);                    
                }
                else
                {
                    var parent = newNodes.SingleOrDefault(n => n.Tag == f.Parent);
                    if(parent == null)
                    {
                        // ?
                    }
                    else
                    {
                        parent.Nodes.Add(newNode);
                    }
                }
                newNodes.Add(newNode);
            }

            treeViewFragments.ExpandAll();
        }

        private void EditNode(TreeNode node)
        {
            if (node.Tag is DataBlock)
            {
                FragmentSelected?.Invoke(this, new SelectEntityEventArgs
                {
                    Fragment = (DataBlock)node.Tag
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
            EditNode(treeViewFragments.SelectedNode);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (treeViewFragments.SelectedNode?.Tag is DataBlock)
            {
                FragmentDeleted?.Invoke(this, new SelectEntityEventArgs
                {
                    Fragment = (DataBlock)treeViewFragments.SelectedNode.Tag
                });
            }

        }

        private void linkLabelCopyFragment_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (treeViewFragments.SelectedNode?.Tag is DataBlock)
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
