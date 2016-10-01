using SeriesEngine.ExcelAddIn.Models;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class ApplicationController : IController
    {
        protected IContainer Container { get; set; }

        public ApplicationController()
        {
            Container = new Container();
        }

        public bool IsActive { get; set; }

        public T GetInstance<T>()
        {
            //if (IsActive)
            //{
                return Container.GetInstance<T>();
            //}
            throw new Exception("Inactive");
        }
    }
}
