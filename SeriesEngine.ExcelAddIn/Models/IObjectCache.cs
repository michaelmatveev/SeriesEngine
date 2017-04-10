using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IObjectCache
    {
        IEnumerable<string> GetObjectsOfType(int solutionId, string type);
        void RemoveObject(int solutionId, string type, string name);
        void AddObject(int solutionId, string type, string name);
    }
}
