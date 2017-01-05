﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.Msk1;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class RibbonWrapper : IMainMenuView
    {
        public event EventHandler<FilterArgs> FilterSelected;
        public event EventHandler<CurrentSelectionArgs> InsertNewDataBlock;
        public event EventHandler<PaneArgs> ShowCustomPane;
        public event EventHandler RefreshAll;
        public event EventHandler SaveAll;


        public bool IsActive { get; set; }
        private readonly IMainMenuView _realView;
        public RibbonWrapper(IMainMenuView realView)
        {
            _realView = realView;
            realView.InsertNewDataBlock += (s, e) => 
            {
                if(IsActive && InsertNewDataBlock != null)
                {
                    InsertNewDataBlock(s, e);
                }
            };

            realView.ShowCustomPane += (s, e) =>
            {
                if(IsActive && ShowCustomPane != null)
                {
                    ShowCustomPane(s, e);
                }
            };

            realView.RefreshAll += (s, e) =>
            {
                if(IsActive && RefreshAll != null)
                {
                    RefreshAll(s, e);
                }
            };

            realView.SaveAll += (s, e) =>
            {
                if (IsActive && SaveAll != null)
                {
                    SaveAll(s, e);
                }
            };
        }

        public void SetPaneVisibleState(bool isVisible)
        {
            _realView.SetPaneVisibleState(isVisible);
        }
    }
}
