using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IProgressView
    {
        void Start(string caption);
        void Stop();
        void UpdateInfo(string descriptino);
    }
}
