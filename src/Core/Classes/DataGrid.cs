using aCHADemia.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using DependencyPropertyGenerator;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace aCHADemia.Core.Classes
{
    public class DataGridRow : ObservableObject, IDataGridRow
    {
        public ObservableCollection<string> Values { get; set; } = [];

        public DataGridRow(params object[] values)
        {
            foreach (var value in values)
            {
                Values.Add(value?.ToString() ?? string.Empty);
            }
        }

        public object this[int index] => Values[index];

        public int ColumnCount => Values.Count;
    }

    public class SelectableDataGridRow : DataGridRow, ISelectableRow
    {
        private bool _isSelected;

        public SelectableDataGridRow(params object[] values) : base(values)
        {
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
