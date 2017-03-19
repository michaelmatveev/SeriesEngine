using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class PeriodVariableEditor : Form, 
        IPeriodVariableView
    {
        public PeriodVariableEditor()
        {
            InitializeComponent();
        }

        void IPeriodVariableView.ShowIt(EditPeriodVariables valuesCollection)
        {
            Text = $"{valuesCollection.VariableMetamodel.Name} - {valuesCollection.ObjectName}";
            var items = valuesCollection
                .ValuesForPeriod
                .Select(vp => new ListViewItem(new[] { vp.Period.ToString(), vp.Value.ToString() })
                {
                    Tag = vp
                }).ToArray();

            listViewVariable.Items.AddRange(items);
            if (ShowDialog() == DialogResult.OK)
            {
             
            }
        }

    }
}
