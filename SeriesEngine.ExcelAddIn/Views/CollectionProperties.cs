using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class CollectionProperties : Form,
        ICollectionPropertiesView
    {
        private Func<IList<string>> _worksheetNames;

        public CollectionProperties(Func<IList<string>> worksheetNamesFactory)
        {
            _worksheetNames = worksheetNamesFactory;
            InitializeComponent();
        }

        public CollectionDataBlock CollectionDataBlock { get; set; }
        public event EventHandler CollectionChanged;

        public void ShowIt()
        {
            comboBoxSheet.DataSource = _worksheetNames();

            textBoxName.DataBindings.Add(nameof(textBoxName.Text),
                CollectionDataBlock,
                nameof(CollectionDataBlock.Name));

            comboBoxSheet.DataBindings.Add(nameof(comboBoxSheet.SelectedItem),
                CollectionDataBlock,
                nameof(CollectionDataBlock.Sheet));

            textBoxCell.DataBindings.Add(nameof(textBoxCell.Text),
                CollectionDataBlock,
                nameof(CollectionDataBlock.Cell));

            if (ShowDialog() == DialogResult.OK)
            {
                CollectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}