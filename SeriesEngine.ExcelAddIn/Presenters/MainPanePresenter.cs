using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.App.EventData;
using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Presenters
{
           
    public class MainPanePresenter : Presenter<IMainPane>, ICommand<ShowCustomPaneCommandArgs>, IEventHandler<SwitchToViewEventData>
    {
        private ICollection<string> ViewsToSwitch = new List<string>
        {
            "Периоды",
            "Фрагменты",
            "Фильтры"
        };

        public MainPanePresenter(IMainPane view, IApplicationController controller) : base(view, controller)
        {
            View.PaneClosed += (s, e) => Controller.Raise(new MainPaneClosed());
            View.SwitchToTheView += (s, e) =>
            {
                switch (e.ViewName)
                {
                    case "Периоды":
                        Controller.Execute(new SwitchToPeriodCommandArgs()); break;
                    case "Фрагменты":
                        Controller.Execute(new SwitchToFragmentsCommandArgs()); break;
                    case "Фильтры":
                        View.InflateControl(null); break;
                    //Controller.Execute(new SwitchToFiltersCommandArgs()); break;
                    default:
                        throw new InvalidOperationException();
                }
            };
        }

        void ICommand<ShowCustomPaneCommandArgs>.Execute(ShowCustomPaneCommandArgs commandData)
        {
            if (commandData.IsVisible)
            {
                View.SetViews(ViewsToSwitch);
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
