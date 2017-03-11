using System;
using System.Runtime.InteropServices;

namespace SeriesEngine.ExcelAddIn.Helpers
{
    public class SafeCom
    {
        public static IDisposable ReleaseAfterUsing(object comObject)
        {
            return new Disposable(() =>
            {
                if(comObject != null)
                {
                    Marshal.ReleaseComObject(comObject);
                    comObject = null;
                }
            });
        }   
    }
}
