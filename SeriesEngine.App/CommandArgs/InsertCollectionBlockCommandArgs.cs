using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.App.CommandArgs
{
    public class InsertCollectionBlockCommandArgs
    {
        public string Sheet { get; set; }
        public string Cell { get; set; }
        public string Name { get; set; }
    }
}
