using System;
using System.Collections.Generic;
using SeriesEngine.Msk1;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class SolutionProvider : ISolutionProvider
    {
        public IEnumerable<Solution> GetAllSolutions()
        {
            using (var context = new Model1())
            {
                return new List<Solution>(context.Solutions);
            }
        }
    }
}
