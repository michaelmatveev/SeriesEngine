using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public abstract class Presenter<V>
        where V : IView
    {
        protected readonly V View;
        protected readonly IController Controller;

        public Presenter(V view, IController controller)
        {
            View = view;
            Controller = controller;
        }
    }
}
