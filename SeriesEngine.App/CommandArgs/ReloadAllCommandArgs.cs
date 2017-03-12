namespace SeriesEngine.App.CommandArgs
{
    public class ReloadAllCommandArgs : BaseCommandArgs
    {
    }

    public class ReloadDataBlockCommandArgs : BaseCommandArgs
    {
        public string BlockName { get; set; }
    }

}
