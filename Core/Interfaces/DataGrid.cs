using System.Collections.ObjectModel;

namespace aCHADemia.Core.Interfaces
{
    public interface IDataGridRow
    {
        ObservableCollection<string> Values { get; set; }
        int ColumnCount { get; }
    }

    public interface ISelectableRow : IDataGridRow
    {
        bool IsSelected { get; }
    }
}
