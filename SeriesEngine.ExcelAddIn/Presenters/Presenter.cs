using SeriesEngine.App;
using SeriesEngine.ExcelAddIn.Views;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public abstract class Presenter<V>
        where V : IView
    {
        protected readonly V View;
        protected readonly IApplicationController Controller;

        public Presenter(V view, IApplicationController controller)
        {
            View = view;
            Controller = controller;
        }
    }
}
