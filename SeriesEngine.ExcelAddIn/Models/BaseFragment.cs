using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class BaseFragment
    {
        public string Name { get; set; }
        public BaseFragment Parent { get; set; }
    }
}
