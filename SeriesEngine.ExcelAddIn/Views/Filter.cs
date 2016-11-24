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
using SeriesEngine.ExcelAddIn.Helpers;
using SeriesEngine.Msk1;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class Filter : PaneControl, IFilterView
    {
        public Filter(PanesManager embedder) : base(embedder, "Фильтр")
        {
            InitializeComponent();
            breadCrumbs.BreadCrumbClick += (s, e) =>
            {
                var sb = new StringBuilder();
                breadCrumbs.SelectedItems.Aggregate(sb, (ac, si) => ac.AppendLine(si.Text));
                FilterUpdated?.Invoke(this, new FilterUpdatedArgs
                {
                    FilterString = sb.ToString()
                });
            };
        }

        public event EventHandler<FilterUpdatedArgs> FilterUpdated;

        public void RefreshFilter(NetworkTree selectedNetwork, string currentRoute)
        {
            var items = new List<BreadCrumbs.BreadCrumbItem>();
            //items.AddRange(selectedNetwork.MainHierarchyNodes.Select(n => new BreadCrumbs.BreadCrumbItem(n.NodeName, 0, 0, 0, n)));
            foreach(var item in items)
            {
                var parent = ((NetworkTreeNode)item.Tag).Parent;
                item.Parent = items.FirstOrDefault(it => it.Tag == parent);
            }
            breadCrumbs.BreadCrumbItems = items;
        }

    }
}
