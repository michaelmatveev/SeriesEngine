using System;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class ErrorOccuredEventArgs : EventArgs
    {
        public string Message { get; set; }
        public bool Cancel { get; set; }
    }
}
