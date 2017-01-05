using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.App.EventData;
using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Presenters
{
           
    public class MainPanePresenter : Presenter<IMainPane>, 
        ICommand<ShowCustomPaneCommandArgs>, 
        IEventHandler<SwitchToViewEventData>
    {
        private ICollection<string> ViewsToSwitch = new List<string>
        {
            ViewNames.PeriodSelectorViewName,
            ViewNames.DataBlocksViewName,
        };

        public MainPanePresenter(IMainPane view, IApplicationController controller) : base(view, controller)
        {
            View.PaneClosed += (s, e) => Controller.Raise(new MainPaneClosed());
            View.SwitchToTheView += (s, e) =>
            {
                switch (e.ViewName)
                {
                    case ViewNames.PeriodSelectorViewName:
                        Controller.Execute(new SwitchToPeriodCommandArgs()); break;
                    case ViewNames.DataBlocksViewName:
                        Controller.Execute(new SwitchToDataBlocksCommandArgs()); break;
                    default:
                        throw new InvalidOperationException();
                }
            };
        }

        void ICommand<ShowCustomPaneCommandArgs>.Execute(ShowCustomPaneCommandArgs commandData)
        {
            if (commandData.IsVisible)
            {
                View.SetViews(ViewsToSwitch, commandData.ViewNameToOpen ?? ViewNames.PeriodSelectorViewName);
                View.ShowIt();
            }
            else
            {
                View.HideIt();
            }
        }

        void IEventHandler<SwitchToViewEventData>.Handle(SwitchToViewEventData eventData)
        {
            View.InflateControl(eventData.InflatedControl);
        }
    }
}
