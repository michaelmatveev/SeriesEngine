namespace SeriesEngine.App.CommandArgs
{
    public class InsertCollectionBlockCommandArgs : BaseCommandArg
    {
        public string Sheet { get; set; }
        public string Cell { get; set; }
        public string Name { get; set; }
    }

    public class InsertSampleCollectionBlockCommandArgs : InsertCollectionBlockCommandArgs
    {
    }
}
