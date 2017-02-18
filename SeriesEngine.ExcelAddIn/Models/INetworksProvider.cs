using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface INetworksProvider
    {
        ICollection<NetworkTree> GetNetworks(int solutionId);
        NetworkTree GetNetworkById(int networkId);
    }
}
