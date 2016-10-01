using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class BreadCrumbs : ToolStrip
    {
        //private ToolStripDropDownItem StartButton;
        private ToolStripSplitButton StartButton;

        public BreadCrumbs() : base()
        {
            BackColor = Color.Transparent;
            Dock = DockStyle.None;
            GripStyle = ToolStripGripStyle.Hidden;

            StartButton = new ToolStripSplitButton //ToolStripDropDownButton
            {
                DisplayStyle = ToolStripItemDisplayStyle.Text
            };

            Items.Add(StartButton);
            StartButtonCaption = "Press this button to start";
            StartButton.Click += StartButton_Click;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            var senderItem = (ToolStripItem)sender;
            RemoveRoute(senderItem);
            this.ResumeLayout();

            OnBreadCrumbClick(null);
        }

        [Description("Text for start button")]
        public string StartButtonCaption
        {
            get
            {
                return StartButton.Text;
            }
            set
            {
                StartButton.Text = value;
            }
        }

        public class BreadCrumbItem
        {
            public BreadCrumbItem(string text, int row, int column, int level, Object tag = null)
            {
                Text = text;
                Tag = tag;
                Row = row;
                Column = column;
                Level = level;
            }

            public int Row { get; set; }
            public int Column { get; set; }

            public BreadCrumbItem Parent { get; set; }
            public string Text { get; set; }

            /// <summary>
            /// Кнопка с которой ассоциирован данный item 
            /// </summary>
            internal ToolStripItem RelatedButton { get; set; }

            /// <summary>
            /// Производьный объект присязанный к item
            /// </summary>
            public Object Tag { get; private set; }
            public int Level { get; internal set; }
        }

        private IEnumerable<BreadCrumbItem> _BreadCrumbItems;
        public IEnumerable<BreadCrumbItem> BreadCrumbItems
        {
            get
            {
                return _BreadCrumbItems;
            }
            set
            {
                _BreadCrumbItems = value;
                InitializeRoute();
            }
        }

        private void InitializeRoute()
        {
            this.SuspendLayout();
            StartButton.DropDownItems.Clear();
            if (BreadCrumbItems == null)
            {
                //StartButton.DropDownItems.Clear();
            }
            else
            {
                foreach (var item in BreadCrumbItems.Where(item => item.Parent == null))
                {
                    CreateSubMenu(item, StartButton);
                }
            }
            this.ResumeLayout();
        }

        private void CreateSubMenu(BreadCrumbItem item, ToolStripDropDownItem root)
        {
            var newMenuItem = new ToolStripMenuItem(item.Text)
            {
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                Tag = item,
            };

            item.RelatedButton = newMenuItem;
            root.DropDownItems.Add(newMenuItem);
            newMenuItem.Click += NewMenuItem_Click;
        }

        private void NewMenuItem_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            var senderItem = (ToolStripItem)sender;
            var breadCrumbItem = (BreadCrumbItem)senderItem.Tag;
            RemoveRoute(senderItem.OwnerItem);
            InsertInTheEnd(breadCrumbItem);
            this.ResumeLayout();

            OnBreadCrumbClick(breadCrumbItem);
        }

        private void RemoveRoute(ToolStripItem itemToClearAfter)
        {
            var lastIndex = Items.IndexOf(itemToClearAfter);

            // удаляем все пункты меню справа от выбранного
            foreach (var itemToRemove in Items
                .Cast<ToolStripItem>()
                .Where((s, i) => i > lastIndex)
                .ToList())
            {
                Items.Remove(itemToRemove);
            }
        }

        private void InsertInTheEnd(BreadCrumbItem selectedItem)
        {
            var lastButton = new ToolStripSplitButton(selectedItem.Text)
            {
                Tag = selectedItem
            };
            Items.Add(lastButton);
            lastButton.Click += LastButton_Click;

            foreach (var item in BreadCrumbItems.Where(item => item.Parent == selectedItem))
            {
                CreateSubMenu(item, lastButton);
            }
        }

        private void LastButton_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            var senderItem = (ToolStripItem)sender;
            RemoveRoute(senderItem);
            this.ResumeLayout();

            OnBreadCrumbClick((BreadCrumbItem)senderItem.Tag);
        }

        public void SwitchToItem(BreadCrumbItem selectedItem)
        {
            this.SuspendLayout();
            RemoveRoute(StartButton);

            if (selectedItem != null)
            {
                var newRoute = new List<BreadCrumbItem>()
                {
                    selectedItem
                };

                int level = selectedItem.Level;
                while (level >= 0)
                {
                    --level;
                    foreach (var item in BreadCrumbItems.Where(item => item.Level == level))
                    {
                        if (newRoute.Last().Parent == item)
                        {
                            newRoute.Add(item);
                            break;
                        }
                    }
                }
                newRoute.Reverse();
                foreach (var item in newRoute)
                {
                    InsertInTheEnd(item);
                }

                OnBreadCrumbSelected(selectedItem);
            }

            this.ResumeLayout();
        }

        public class BreadCrumbItemClickEvent : EventArgs
        {
            public BreadCrumbItem ClickedItem { get; set; }
        }

        public event EventHandler<BreadCrumbItemClickEvent> BreadCrumbClick;
        protected void OnBreadCrumbClick(BreadCrumbItem item)
        {
            if (BreadCrumbClick != null)
            {
                BreadCrumbClick(this, new BreadCrumbItemClickEvent
                {
                    ClickedItem = item
                });
            }
        }

        public event EventHandler<BreadCrumbItemClickEvent> BreadCrumbSelected;
        protected void OnBreadCrumbSelected(BreadCrumbItem item)
        {
            if (BreadCrumbSelected != null)
            {
                BreadCrumbSelected(this, new BreadCrumbItemClickEvent
                {
                    ClickedItem = item
                });
            }
        }

        public IEnumerable<BreadCrumbItem> SelectedItems
        {
            get
            {
                return this.Items
                    .OfType<ToolStripSplitButton>()
                    .Where(tssb => tssb.Tag != null)
                    .Select(tssb => (BreadCrumbItem)tssb.Tag);
            }
        }
    }

}
