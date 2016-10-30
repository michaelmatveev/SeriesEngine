namespace SeriesEngine.App
{
    public interface IApplicationController
    {
        void Execute<T>(T commandData);
        void Raise<T>(T eventData);
        //void Subscribe<T>(IEventHandler<T> handler);
        //void Unsubscribe<T>(IEventHandler<T> handler);
    }
}
