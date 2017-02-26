using System;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IErrorAware
    {
        event EventHandler<ErrorOccuredEventArgs> ErrorOccured;
    }
}
