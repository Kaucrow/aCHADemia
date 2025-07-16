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

        [ObservableProperty]
        private string? _courseId;

        public aCHADemia.MVVM.RelayCommand SearchCourseCommand { get; }
        public aCHADemia.MVVM.RelayCommand DeleteCourseCommand { get; }

        public MaintenanceCourseViewModel()
        {
            SearchCourseCommand = new aCHADemia.MVVM.RelayCommand(async _ => await SearchCourseAsync());
            DeleteCourseCommand = new aCHADemia.MVVM.RelayCommand(async _ => await DeleteCourseAsync());
        }

        private async Task SearchCourseAsync()
        {
            CourseRows.Clear();

            if (string.IsNullOrWhiteSpace(CourseId))
            {
                MessageBox.Show("Ingrese el ID del curso a buscar.");
                return;
            }

            try
            {
                using var reader = await App.Db.Fetch(
                    aCHADemia.Core.DBComponent.DbType.Postgres,
                    Config.Queries.Course.GetById,
                    new NpgsqlParameter("@curso_id", int.Parse(CourseId))
                );

                while (await reader.ReadAsync())
                {
                    var fechaIni = reader["curso_dt_ini"].ToString();
                    var fechaFin = reader["curso_dt_end"].ToString();
                    var descripcion = reader["curso_de"].ToString();

                    CourseRows.Add(new SelectableDataGridRow(fechaIni, fechaFin, descripcion));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar curso: {ex.Message}");
            }
        }

        private async Task DeleteCourseAsync()
        {
            var selectedRow = CourseRows.FirstOrDefault(r => r.IsSelected);
            if (selectedRow == null)
            {
                MessageBox.Show("Seleccione un curso para eliminar.");
                return;
            }

            // Suponiendo que el primer valor es el ID del curso, si no, ajusta según corresponda
            var id = CourseId;
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("No se puede determinar el ID del curso a eliminar.");
                return;
            }

            try
            {
                await App.Db.Execute(
                    aCHADemia.Core.DBComponent.DbType.Postgres,
                    Config.Queries.Course.DeleteById,
                    new NpgsqlParameter("@curso_id", int.Parse(id))
                );

                CourseRows.Remove(selectedRow);
                MessageBox.Show("Curso eliminado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar curso: {ex.Message}");
            }
        }
    }
}
