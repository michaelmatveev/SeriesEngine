using System.ComponentModel;

namespace SeriesEngine.Core.DataAccess
{
    public class EditorObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private string _name;
        public string Name {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int NetworkId { get; set; }
        public int NodeId { get; set; }        
    }
}
