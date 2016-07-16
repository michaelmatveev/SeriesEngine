using System;

namespace SeriesEngine.Core
{
    [Flags]
    public enum Reliability : short
    {
        None                = 0,
        PreliminaryForecast = 1 << 0,
        ReliableForecast    = 1 << 1,
        PreliminaryPlan     = 1 << 2,
        ReliablePlan        = 1 << 3,
        OperationalData     = 1 << 4,
        SpecifiedData       = 1 << 5,
        CommercialFact      = 1 << 6,
        FinalFact           = 1 << 7
    }
}
