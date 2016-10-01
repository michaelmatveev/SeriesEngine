using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class CollectionFragment : BaseFragment
    {
        public IEnumerable<ObjectMetamodel> SupportedModels { get; set; }
        public int[] ObjectIds { get; set; }
    }
}
