namespace SeriesEngine.App.CommandArgs
{
    public class ShowCustomPaneCommandArgs : BaseCommandArg
    {
        public bool IsVisible { get; set; }
        public string ViewNameToOpen { get; set; }
    }
}
