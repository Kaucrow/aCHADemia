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
    internal partial class MaintenanceSubjectViewModel : ViewModelBase
    {
        public List<string> ColumnHeaders { get; } = ["ID", "Nombre"];

        [ObservableProperty]
        private ObservableCollection<SelectableDataGridRow> _subjectRows = [];

        [ObservableProperty]
        private string? _subjectId;

        public aCHADemia.MVVM.RelayCommand SearchSubjectCommand { get; }
        public aCHADemia.MVVM.RelayCommand DeleteSubjectCommand { get; }
        

        public MaintenanceSubjectViewModel()
        {
            SearchSubjectCommand = new aCHADemia.MVVM.RelayCommand(async _ => await SearchSubjectAsync());
            DeleteSubjectCommand = new aCHADemia.MVVM.RelayCommand(async _ => await DeleteSubjectAsync());
        }

        private async Task SearchSubjectAsync()
        {
            SubjectRows.Clear();

            if (string.IsNullOrWhiteSpace(SubjectId))
            {
                MessageBox.Show("Ingrese el ID de la materia a buscar.");
                return;
            }

            try
            {
                MessageBox.Show($"Buscando materia con ID: {SubjectId}");

                using var reader = await App.Db.Fetch(
                    aCHADemia.Core.DBComponent.DbType.Postgres,
                    Config.Queries.Subject.GetById,
                    new NpgsqlParameter("@materia_id", int.Parse(SubjectId))
                );

                bool found = false;
                while (await reader.ReadAsync())
                {
                    found = true;
                    var id = reader["materia_id"].ToString();
                    var nombre = reader["materia_de"].ToString();

                    SubjectRows.Add(new SelectableDataGridRow(id, nombre));
                    // DEBUG MessageBox.Show(string.Join(" | ", SubjectRows.Select(row => string.Join(",", row.Values))));
                }

                if (!found)
                    MessageBox.Show("No se encontró ninguna materia con ese ID.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar materia: {ex.Message}");
            }
        }

        private async Task DeleteSubjectAsync()
        {
            var selectedRow = SubjectRows.FirstOrDefault(r => r.IsSelected);
            if (selectedRow == null)
            {
                MessageBox.Show("Seleccione una materia para eliminar.");
                return;
            }

            var id = selectedRow.Values[0];
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("No se puede determinar el ID de la materia a eliminar.");
                return;
            }

            try
            {
                await App.Db.Execute(
                    aCHADemia.Core.DBComponent.DbType.Postgres,
                    Config.Queries.Subject.DeleteById,
                    new NpgsqlParameter("@materia_id", int.Parse(id))
                );

                SubjectRows.Remove(selectedRow);
                MessageBox.Show("Materia eliminada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar materia: {ex.Message}");
            }
        }
    }
}
