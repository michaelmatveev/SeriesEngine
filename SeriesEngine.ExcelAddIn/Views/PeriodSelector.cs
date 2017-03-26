using System;
using System.Windows.Forms;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class PeriodSelector : UserControl, IPeriodView
    {
        public PeriodSelector() : base()
        {
            InitializeComponent();
        }
        
        public event EventHandler PeriodChanged;

        public Period SelectedPeriod
        {
            get
            {
                return new Period
                {
                    From = dateTimePickerStart.Value,
                    Till = dateTimePickerFinish.Value
                };
            }

            set
            {
                if (value == null)
                {
                    dateTimePickerStart.Value = dateTimePickerFinish.Value = DateTime.Now;
                }
                else
                {
                    dateTimePickerStart.Value = value.From;
                    dateTimePickerFinish.Value = value.Till;
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (dateTimePickerStart.Value < dateTimePickerFinish.Value)
            {
                PeriodChanged?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}