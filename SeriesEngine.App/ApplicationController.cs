using StructureMap;

namespace SeriesEngine.App
{
    public class ApplicationController : IApplicationController
    {
        protected Container Container { get; set; }
        protected IEventPublisher EventPublisher { get; set; }

        public ApplicationController()
        {
            Container = new Container();
            EventPublisher = new EventPublisher();
        }
        
        public virtual void Execute<T>(T commandData)
        {
            ICommand<T> command = Container.TryGetInstance<ICommand<T>>();
            if (command != null)
            {
                command.Execute(commandData);
            }
        }
        
        public virtual void Raise<T>(T eventData)
        {
            EventPublisher.Publish(eventData);
        }

    }
}
