namespace SeriesEngine.App.CommandArgs
{
    public class InsertCollectionBlockCommandArgs : BaseCommandArgs
    {
        public CurrentSelection CurrentSelection { get; set; }
        public string InsertedBlockName { get; set; }
    }

    public class InsertSampleCollectionBlockCommandArgs : InsertCollectionBlockCommandArgs
    {
        public string SampleName { get; set; }
    }
}
