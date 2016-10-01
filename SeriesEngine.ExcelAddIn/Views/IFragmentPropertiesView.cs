﻿using SeriesEngine.ExcelAddIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface IFragmentPropertiesView : IView
    {
        event EventHandler FragmentChanged;
        Fragment Fragment { get; set; }
        //IEnumerable<Network> Networks { get; set; }
        void ShowIt();
    }
}