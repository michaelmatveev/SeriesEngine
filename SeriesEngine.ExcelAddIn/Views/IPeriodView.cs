﻿using SeriesEngine.ExcelAddIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface IPeriodView : IPanes
    {
        event EventHandler PeriodChanged;
        Period SelectedPeriod { get; set; }

    }
}