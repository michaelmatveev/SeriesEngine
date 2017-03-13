using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface INetworksProvider
    {
        ICollection<NetworkTree> GetNetworks(int solutionId);
        NetworkTree GetNetworkById(int networkId);
        NetworkTree GetNetwork(int solutionId, string name, IList<VariableDataBlock> variables);
    }
}
