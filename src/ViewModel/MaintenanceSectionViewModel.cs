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
    internal partial class MaintenanceSectionViewModel : ViewModelBase
    {
        public List<string> ColumnHeaders { get; } = ["ID", "Descripcion"];

        [ObservableProperty]
        private ObservableCollection<SelectableDataGridRow> _sectionRows = [];

        [ObservableProperty]
        private string? _sectionId;

        public aCHADemia.MVVM.RelayCommand SearchSectionCommand { get; }
        public aCHADemia.MVVM.RelayCommand DeleteSectionCommand { get; }

        public MaintenanceSectionViewModel()
        {
            SearchSectionCommand = new aCHADemia.MVVM.RelayCommand(async _ => await SearchSectionAsync());
            DeleteSectionCommand = new aCHADemia.MVVM.RelayCommand(async _ => await DeleteSectionAsync());
        }

        private async Task SearchSectionAsync()
        {
            SectionRows.Clear();

            if (string.IsNullOrWhiteSpace(SectionId))
            {
                MessageBox.Show("Ingrese el ID de la sección a buscar.");
                return;
            }

            try
            {
                using var reader = await App.Db.Fetch(
                    aCHADemia.Core.DBComponent.DbType.Postgres,
                    Config.Queries.Section.GetById,
                    new NpgsqlParameter("@seccion_id", int.Parse(SectionId))
                );

                bool found = false;
                while (await reader.ReadAsync())
                {
                    found = true;
                    var id = reader["seccion_id"].ToString();
                    var descripcion = reader["seccion_de"].ToString();

                    SectionRows.Add(new SelectableDataGridRow(id, descripcion));
                }

                if (!found)
                    MessageBox.Show("No se encontró ninguna sección con ese ID.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar sección: {ex.Message}");
            }
        }

        private async Task DeleteSectionAsync()
        {
            var selectedRow = SectionRows.FirstOrDefault(r => r.IsSelected);
            if (selectedRow == null)
            {
                MessageBox.Show("Seleccione una sección para eliminar.");
                return;
            }

            var id = selectedRow.Values[0];
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("No se puede determinar el ID de la sección a eliminar.");
                return;
            }

            try
            {
                await App.Db.Execute(
                    aCHADemia.Core.DBComponent.DbType.Postgres,
                    Config.Queries.Section.DeleteById,
                    new NpgsqlParameter("@seccion_id", int.Parse(id))
                );

                SectionRows.Remove(selectedRow);
                MessageBox.Show("Sección eliminada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar sección: {ex.Message}");
            }
        }
    }
}
