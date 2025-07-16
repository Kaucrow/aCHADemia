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
    internal partial class MaintenancePersonViewModel : ViewModelBase
    {
        public List<string> ColumnHeaders { get; } = ["C.I.", "Nombre", "Tipo de Persona"];

        [ObservableProperty]
        private ObservableCollection<SelectableDataGridRow> _personRows = [];

        [ObservableProperty]
        private string _personName;

       
        [ObservableProperty]
        private string? _personCI;

        public aCHADemia.MVVM.RelayCommand SearchPersonCommand { get; }
        public aCHADemia.MVVM.RelayCommand DeletePersonCommand { get; }

        public MaintenancePersonViewModel()
        {
            SearchPersonCommand = new aCHADemia.MVVM.RelayCommand(async _ => await SearchPersonAsync());
            DeletePersonCommand = new aCHADemia.MVVM.RelayCommand(async _ => await DeletePersonAsync());
        }

        private async Task SearchPersonAsync()
        {
            PersonRows.Clear();

            if (string.IsNullOrWhiteSpace(PersonCI))
            {
                MessageBox.Show("Ingrese cedula a buscar.");
                return;
            }

            try
            {
                using var reader = await App.Db.Fetch(
                    aCHADemia.Core.DBComponent.DbType.Postgres,
                    Config.Queries.Person.GetByName,
                    new NpgsqlParameter("@person_ci", int.Parse(PersonCI))
                );

                while (await reader.ReadAsync())
                {
                    var ci = reader["persona_ci"].ToString();
                    var nombre = reader["persona_nom"].ToString();
                    var tipo = reader["tipo_persona_de"].ToString(); 

                    PersonRows.Add(new SelectableDataGridRow(ci, nombre, tipo));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar personas: {ex.Message}");
            }
        }

        private async Task DeletePersonAsync()
        {
            var selectedRow = PersonRows.FirstOrDefault(r => r.IsSelected);
            if (selectedRow == null)
            {
                MessageBox.Show("Seleccione un registro para eliminar.");
                return;
            }

            var ci = selectedRow.Values[0]; 

            try
            {
                await App.Db.Execute(
                    aCHADemia.Core.DBComponent.DbType.Postgres,
                    Config.Queries.Person.DeleteByCI,
                    new NpgsqlParameter("@person_ci", int.Parse(ci))
                );

                PersonRows.Remove(selectedRow);
                MessageBox.Show("Registro eliminado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar persona: {ex.Message}");
            }
        }
    }
}
