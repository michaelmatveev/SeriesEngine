using System;

namespace SeriesEngine.ExcelAddIn.Models.Fragments
{
    [Serializable]
    public abstract class SheetFragment : BaseFragment
    {
        public Period CustomPeriod { get; set; }

        public string Sheet { get; set; }
        public string Cell { get; set; }

        public SheetFragment(BaseFragment parent, Period defaultPeriod)
        {
            Parent = parent;
            CustomPeriod = defaultPeriod;
        }

    }
}
