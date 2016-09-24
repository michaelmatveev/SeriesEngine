using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class MockModelProvider : IModelProvider
    {
        public IEnumerable<ObjectMetamodel> GetObjectMetamodels()
        {
            return new List<ObjectMetamodel>
            {
                new ObjectMetamodel
                {
                    Name = "ObjectA",
                    Variables = new List<Variable>
                    {
                        new Variable
                        {
                            Name = "Variable 1"
                        },
                        new Variable
                        {
                            Name = "Variable 2"
                        },
                    }
                },
                new ObjectMetamodel
                {
                    Name = "ObjectB",
                    Variables = new List<Variable>
                    {
                        new Variable
                        {
                            Name = "Variable 3"
                        },
                        new Variable
                        {
                            Name = "Variable 4"
                        },
                    }
                }
            };
        }
    }
}
