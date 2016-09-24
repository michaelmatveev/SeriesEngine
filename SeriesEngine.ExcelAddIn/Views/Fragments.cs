﻿using System;
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
        public event EventHandler<SelectFragmentEventArgs> FragmentSelected;

        public Fragments(IViewEmbedder embedder) : base(embedder, "Фрагменты")
        {
            InitializeComponent();
        }

        public void RefreshFragmentsView(IEnumerable<Fragment> fragments)
        {
            listViewFragments.Items.Clear();
            listViewFragments.Groups.Clear();
            var groups = fragments
                .GroupBy(f => f.Sheet)
                .Select(g => new ListViewGroup(g.Key, g.Key))
                .ToArray();
            listViewFragments.Groups.AddRange(groups);

            listViewFragments.Items.AddRange(
                fragments
                .Select(f => new ListViewItem(new[] { f.Name, f.Cell }, 
                    listViewFragments
                    .Groups
                    .Cast<ListViewGroup>()
                    .Single(g => g.Name == f.Sheet)
                )
                {
                    Tag = f
                })
                .ToArray());
            
        }

        private void listViewFragments_DoubleClick(object sender, EventArgs e)
        {
            FragmentSelected?.Invoke(sender, new SelectFragmentEventArgs
            {
                Fragment = (Fragment)listViewFragments.SelectedItems[0].Tag
            });
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listViewFragments_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
