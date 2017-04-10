using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class ObjectCache : IObjectCache
    {
        public void AddObject(int solutionId, string type, string name)
        {
        }

        public IEnumerable<string> GetObjectsOfType(int solutionId, string type)
        {
            yield return type + "1";
            yield return type + "2";
        }

        public void RemoveObject(int solutionId, string type, string name)
        {
        }
    }
}
