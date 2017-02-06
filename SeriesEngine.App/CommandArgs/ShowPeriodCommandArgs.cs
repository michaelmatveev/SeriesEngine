namespace SeriesEngine.App.CommandArgs
{
    public class ShowCustomPaneCommandArgs : BaseCommandArgs
    {
        public bool IsVisible { get; set; }
        public string ViewNameToOpen { get; set; }
    }
}
