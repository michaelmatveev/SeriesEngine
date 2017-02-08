using SeriesEngine.App.CommandArgs;
using SeriesEngine.Core.DataAccess;
using System.ComponentModel;
using Container = StructureMap.Container;

namespace SeriesEngine.App
{
    public class ApplicationController : IApplicationController,
        INotifyPropertyChanged
    {
        private Solution _currentSolution;

        public event PropertyChangedEventHandler PropertyChanged;

        public Solution CurrentSolution
        {
            get
            {
                return _currentSolution;
            }
            set
            {
                if(value == null || (_currentSolution == null && value != null) || (_currentSolution.Id != value.Id))
                {
                    _currentSolution = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSolution)));
                }
            }
        }

        protected Container Container { get; set; }
        protected IEventPublisher EventPublisher { get; set; }

        public ApplicationController()
        {
            Container = new Container();
            EventPublisher = new EventPublisher();
        }
        
        public virtual void Execute<T>(T commandData) where T : BaseCommandArgs
        {
            ICommand<T> command = Container.TryGetInstance<ICommand<T>>();
            if (command != null)
            {
                commandData.Solution = CurrentSolution;
                command.Execute(commandData);
            }
        }
        
        public virtual void Raise<T>(T eventData)
        {
            EventPublisher.Publish(eventData);
        }

    }
}
