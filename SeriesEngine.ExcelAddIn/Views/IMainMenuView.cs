using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class PaneArgs: EventArgs
    {
        public bool Visible { get; set; }
    }
    
    public class FilterArgs: EventArgs
    {
        public Network SelectedNetwork { get; set; }
    }

    public class SampleArgs: EventArgs
    {
        public string SampleName { get; set; }
        public string XmlQuery { get; set; }
    } 

    public interface IMainMenuView : IView
    {
        event EventHandler<PaneArgs> ShowCustomPane;
        event EventHandler RefreshAll;
        event EventHandler SaveAll;
        event EventHandler MergeAll;
        event EventHandler InsertNewDataBlock;
        event EventHandler<SampleArgs> InsertSampleBlock;
        event EventHandler Connect;
        event EventHandler Disconnect;
        event EventHandler RenameObject;
        event EventHandler DeleteObject;
        event EventHandler EditVariable;
        event EventHandler ReloadStoredQueries;
        event EventHandler EditStoredQueries;

        void SetButtonToggleState(bool isVisible);
        void SetTabVisibleState(bool isVisible);
        void UpdateListOfStoredQueries(IEnumerable<StoredQuery> query);

    }
}
