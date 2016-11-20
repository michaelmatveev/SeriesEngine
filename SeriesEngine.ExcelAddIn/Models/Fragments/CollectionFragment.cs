using SeriesEngine.Msk1;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models.Fragments
{
    public class CollectionFragment : BaseFragment
    {
        public IEnumerable<ObjectMetamodel> SupportedModels { get; set; }
        public int[] ObjectIds { get; set; }
    }
}
