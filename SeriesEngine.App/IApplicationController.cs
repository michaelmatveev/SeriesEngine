using SeriesEngine.App.CommandArgs;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.App
{
    public interface IApplicationController
    {
        Solution CurrentSolution { get; set; }
        void Execute<T>(T commandData) where T : BaseCommandArgs;
        void Raise<T>(T eventData);
    }
}
