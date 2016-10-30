namespace SeriesEngine.App
{
	public interface IEventHandler<T>
	{
		void Handle(T eventData);
	}
}