using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class ObjectProvider : IObjectProvider
    {
        public MyObject GetSelectedObject()
        {
            return new MyObject
            {
                Name = "My name"
            };   
        }

        public void UpdateObject(MyObject objectToUpdate)
        {
   
        }
    }
}
