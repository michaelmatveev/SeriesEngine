using SeriesEngine.App.CommandArgs;

namespace SeriesEngine.App
{
    public interface IApplicationController
    {
        int CurrentSolutionId { get; set; }
        void Execute<T>(T commandData) where T : BaseCommandArg;
        void Raise<T>(T eventData);
    }
}
