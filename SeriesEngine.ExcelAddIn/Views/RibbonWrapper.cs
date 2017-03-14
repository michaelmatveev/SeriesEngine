﻿using System;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class RibbonWrapper : IMainMenuView
    {
        public event EventHandler<FilterArgs> FilterSelected;
        public event EventHandler InsertNewDataBlock;
        public event EventHandler InsertSampleBlock;
        public event EventHandler<PaneArgs> ShowCustomPane;
        public event EventHandler RefreshAll;
        public event EventHandler SaveAll;
        public event EventHandler Connect;
        public event EventHandler Disconnect;
        public event EventHandler RenameObject;
        public event EventHandler DeleteObject;
        public event EventHandler EditVariable;
        
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

            realView.InsertSampleBlock += (s, e) =>
            {
                if (IsActive && InsertSampleBlock != null)
                {
                    InsertSampleBlock(s, e);
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

            realView.Connect += (s, e) =>
            {
                if (IsActive)
                {
                    Connect?.Invoke(s, e);
                }
            };

            realView.Disconnect += (s, e) =>
            {
                if (IsActive)
                {
                    Disconnect?.Invoke(s, e);
                }
            };

            realView.DeleteObject += (s, e) =>
            {
                if (IsActive)
                {
                    DeleteObject?.Invoke(s, e);
                }
            };

            realView.RenameObject += (s, e) =>
            {
                if (IsActive)
                {
                    RenameObject?.Invoke(s, e);
                }
            };

            realView.EditVariable += (s, e) =>
            {
                if (IsActive)
                {
                    EditVariable?.Invoke(s, e);
                }
            };
        }

        public void SetTabVisibleState(bool isEnabled)
        {
            _realView.SetTabVisibleState(isEnabled);
        }

        public void SetButtonToggleState(bool isVisible)
        {
            _realView.SetButtonToggleState(isVisible);
        }
    }
}
