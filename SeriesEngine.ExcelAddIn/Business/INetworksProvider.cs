using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface INetworksProvider
    {
        NetworkTree GetNetworkById(string modelName, int networkId);
        NetworkTree GetNetwork(Solution solution, string networkName, IEnumerable<DataBlock> variables = null, Period period = null);
    }
}
