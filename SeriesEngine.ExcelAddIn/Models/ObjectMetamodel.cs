using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class Variable
    {
        public string Name;
    }

    public class ObjectMetamodel
    {
        public string Name { get; set; }
        public List<Variable> Variables { get; set; }
    }
}
