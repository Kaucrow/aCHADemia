using aCHADemia.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace aCHADemia.Core.Classes
{
    public class DataGridRow : ObservableObject, IDataGridRow
    {
        public ObservableCollection<string> Values { get; set; } = [];

        public bool IsModified { get; set; }

        public DataGridRow(params object[] values)
        {
            foreach (var value in values)
            {
                Values.Add(value?.ToString() ?? string.Empty);
            }

            Values.CollectionChanged += (s, e) => IsModified = true;
        }

        public object this[int index]
        {
            get => Values[index];
            set
            {
                if (Values[index] != value?.ToString())
                {
                    Values[index] = value?.ToString() ?? "";
                    IsModified = true;
                }
            }
        }

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
                }
            }
        }
    }
}
