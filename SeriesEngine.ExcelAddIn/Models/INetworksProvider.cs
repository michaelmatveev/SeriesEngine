using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    interface INetworksProvider
    {
        IEnumerable<Network> GetNetworks(string filter); 
    }
}
