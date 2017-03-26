namespace SeriesEngine.Core.DataAccess
{
    public enum ObjectState
    {
        Unchanged = 0,
        Added,
        Modified,
        Deleted
    }

    public interface IStateObject
    {
        ObjectState State { get; }
    }
}
