using System.Linq;
using System.Collections.Generic;
using Solution = SeriesEngine.Core.DataAccess.Solution;
using SeriesEngine.Core;
using System;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class SolutionProvider : ISolutionProvider
    {
        public IEnumerable<Solution> GetAllSolutions()
        {
            using (var context = ModelsDescription.GetModel())
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
            using (var context = ModelsDescription.GetModel())
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

        public void UpdateSolution(Solution solution)
        {
            using (var context = ModelsDescription.GetModel())
            {
                context.Entry(solution).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void InsertSolution(Solution solution)
        {            
            using (var context = ModelsDescription.GetModel())
            {
                context.Entry(solution).State = System.Data.Entity.EntityState.Added;
                var model = ModelsDescription.All.Single(m => m.Name == solution.ModelName);
                foreach (var networkModel in model.HierarchyModels)
                {
                    foreach (var sysNetwork in networkModel.SystemNetworks)
                    {
                        var network = Activator.CreateInstance(networkModel.HierarchyType) as Network;
                        network.Name = sysNetwork;
                        network.Solution = solution;
                        network.IsSystem = true;
                        context.Networks.Add(network);
                    }
                }
                context.SaveChanges();
            }            
        }

        public void DeleteSolution(Solution solution)
        {
            using (var context = ModelsDescription.GetModel())
            {
                context.Entry(solution).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public bool ValidateSolutionName(Solution solutionToCheck, out string errorMessage)
        {
            if(string.IsNullOrEmpty(solutionToCheck.Name))
            {
                errorMessage = "Имя решения не может быть пустым";
                return false;
            }

            using (var context = ModelsDescription.GetModel())
            {
                if(context.Solutions.Any(s => s.Name == solutionToCheck.Name && s.ModelName == solutionToCheck.ModelName))
                {
                    errorMessage = $"Решение '{solutionToCheck.Name}' уже существует для модели '{solutionToCheck.ModelName}'";
                    return false;
                }
            }

            errorMessage = string.Empty;
            return true;
        }

    }
}
