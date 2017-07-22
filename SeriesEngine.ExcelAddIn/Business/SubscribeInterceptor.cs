using StructureMap.Building.Interception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using System.Linq.Expressions;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class SubscribeInterceptor : IInterceptor
    {
        public Type Accepts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Type Returns
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public InterceptorRole Role
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Expression ToExpression(Policies policies, ParameterExpression session, ParameterExpression variable)
        {
            throw new NotImplementedException();
        }
    }
}
