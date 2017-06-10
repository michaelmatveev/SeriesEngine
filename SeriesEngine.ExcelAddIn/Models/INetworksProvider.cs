using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface INetworksProvider
    {
        NetworkTree GetNetworkById(int networkId);
        NetworkTree GetNetwork(Solution solution, string name, IEnumerable<DataBlock> variables = null, Period period = null);
    }
}
