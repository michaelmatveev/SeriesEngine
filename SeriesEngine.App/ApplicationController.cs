using SeriesEngine.App.CommandArgs;
using StructureMap;

namespace SeriesEngine.App
{
    public class ApplicationController : IApplicationController
    {
        public int CurrentSolutionId { get; set; }

        protected Container Container { get; set; }
        protected IEventPublisher EventPublisher { get; set; }

        public ApplicationController()
        {
            Container = new Container();
            EventPublisher = new EventPublisher();
        }
        
        public virtual void Execute<T>(T commandData) where T : BaseCommandArg
        {
            ICommand<T> command = Container.TryGetInstance<ICommand<T>>();
            if (command != null)
            {
                commandData.SolutionId = CurrentSolutionId;
                command.Execute(commandData);
            }
        }
        
        public virtual void Raise<T>(T eventData)
        {
            EventPublisher.Publish(eventData);
        }

    }
}
