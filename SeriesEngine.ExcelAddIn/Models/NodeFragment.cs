using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public enum ExportNodeValue
    {
        Name,
        Since,
        Till
    }

    /// <summary>
    /// This fragment is conencted with object in specified hierachy
    /// </summary>
    public class NodeFragment : DataFragment
    {
        public NodeFragment(BaseFragment parent, Period defaultPeriod) : base(parent, defaultPeriod)
        {
        }

        public ObjectMetamodel Model { get; set; }
        public ExportNodeValue NodeValue { get; set; }
    }
}
