using Microsoft.Office.Tools;
using SeriesEngine.ExcelAddIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IController
    {
        T GetInstance<T>();
    }
}
