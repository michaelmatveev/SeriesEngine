using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface IViewEmbedder
    {
        void Embed<T>(T viewToEmbed, string caption);
        void Release<T>(T viewToRelease);
    }
}
