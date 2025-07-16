using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aCHADemia.MVVM;
using CommunityToolkit.Mvvm.ComponentModel;
using aCHADemia.Core.Classes;
using CommunityToolkit.Mvvm.Input;
using Npgsql;
using System.Windows;
using System.Collections.ObjectModel;
using System.Data;
using aCHADemia.Core.DBComponent;

namespace aCHADemia.ViewModel
{
    internal partial class MaintenanceCourseViewModel : ViewModelBase

    {
        public List<string> ColumnHeaders { get; } = ["Fecha Inicio", "Fecha Fin", "Descripcion"];

        [ObservableProperty]
        private ObservableCollection<SelectableDataGridRow> _courseRows = [];

    }
}
