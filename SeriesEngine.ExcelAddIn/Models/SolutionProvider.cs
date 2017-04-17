using System.Linq;
using System.Collections.Generic;
using Solution = SeriesEngine.Core.DataAccess.Solution;
using SeriesEngine.msk1;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class SolutionProvider : ISolutionProvider
    {
        public IEnumerable<Solution> GetAllSolutions()
        {
            using (var context = new Model1())
            {
                return context.Solutions.ToList().Select(s => new Solution
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    ModelName = s.ModelName
                });
            }
        }

        public Solution GetSolutionById(int solutionId)
        {
            using (var context = new Model1())
            {
                var s = context.Solutions.Find(solutionId);
                if (s != null)
                {
                    return new Solution
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                        ModelName = s.ModelName
                    };
                }

                return null;
            }
        }

    }
}
