using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeriesEngine.ExcelAddIn.Models;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class Fragments : PaneControl, IFragmentView
    {
        public event EventHandler<SelectEntityEventArgs> FragmentSelected;
        public event EventHandler<SelectEntityEventArgs> NewFragmentRequested;
        public event EventHandler<SelectEntityEventArgs> FragmentDeleted;
        public event EventHandler<SelectEntityEventArgs> FragmentCopied;

        public Fragments(IViewEmbedder embedder) : base(embedder, "Фрагменты")
        {
            InitializeComponent();
        }

        public void RefreshFragmentsView(IEnumerable<Fragment> fragments)
        {
            treeViewFragments.Nodes.Clear();
            var root = treeViewFragments.Nodes.Add("Эта книга");

            var namedCollections = fragments
                .GroupBy(f => f.SourceCollection)
                .Select(g => new TreeNode(g.Key.Name)
                {
                    Tag = g.Key
                })
                .ToArray();

            root.Nodes.AddRange(namedCollections);

            foreach (var collection in namedCollections)
            {
                collection.Nodes.AddRange(fragments
                    .Where(f => f.SourceCollection == collection.Tag)
                    .Select(f => new TreeNode($"{f.Name} ({f.Sheet}!{f.Cell})")
                    {
                        Tag = f
                    })
                    .ToArray());
            }

            treeViewFragments.ExpandAll();
        }

        private void EditNode(TreeNode node)
        {
            if (node.Tag is Fragment)
            {
                FragmentSelected?.Invoke(this, new SelectEntityEventArgs
                {
                    Fragment = (Fragment)node.Tag
                });
            }
        }

        private void treeViewFragments_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            EditNode(e.Node);
        }

        private void linkLabelAddFragment_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (treeViewFragments.SelectedNode?.Tag is NamedCollection)
            {
                NewFragmentRequested?.Invoke(this, new SelectEntityEventArgs
                {
                    SourceCollection = (NamedCollection)treeViewFragments.SelectedNode.Tag
                });
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EditNode(treeViewFragments.SelectedNode);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (treeViewFragments.SelectedNode?.Tag is Fragment)
            {
                FragmentDeleted?.Invoke(this, new SelectEntityEventArgs
                {
                    Fragment = (Fragment)treeViewFragments.SelectedNode.Tag
                });
            }

        }

        private void linkLabelCopyFragment_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (treeViewFragments.SelectedNode?.Tag is Fragment)
            {
                FragmentCopied?.Invoke(this, new SelectEntityEventArgs
                {
                    Fragment = (Fragment)treeViewFragments.SelectedNode.Tag,
                    SourceCollection = (NamedCollection)treeViewFragments.SelectedNode.Parent.Tag
                });
            }
        }

    }
}
