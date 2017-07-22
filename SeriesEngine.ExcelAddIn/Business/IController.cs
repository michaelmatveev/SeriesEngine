namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IController
    {
        bool IsActive { get; set; }
        string Filter { get; set; }
        //T GetInstance<T>();
    }
}
