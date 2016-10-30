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
