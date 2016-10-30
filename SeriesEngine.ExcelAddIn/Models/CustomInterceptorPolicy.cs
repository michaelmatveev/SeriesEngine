using StructureMap.Building.Interception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap.Pipeline;
using SeriesEngine.App;
using StructureMap;
using System.Linq.Expressions;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class CustomInterceptorPolicy : IInterceptorPolicy
    {
        public string Description
        {
            get
            {
                return "EventHandler";
            }
        }

        public IEnumerable<IInterceptor> DetermineInterceptors(Type pluginType, Instance instance)
        {
            bool matchesType = pluginType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEventHandler<>));
            if (matchesType)
            {
                yield return new FuncInterceptor<object>(d => d);
            }
        }
    }

    //public class MyInterception : IInterceptor
    //{
    //    public Type Accepts
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public string Description
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public Type Returns
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public InterceptorRole Role
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public Expression ToExpression(Policies policies, ParameterExpression session, ParameterExpression variable)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
