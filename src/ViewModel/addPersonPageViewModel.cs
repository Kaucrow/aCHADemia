using aCHADemia.Core.DBComponent;
using aCHADemia.MVVM;
using aCHADemia.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Npgsql;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Security;

namespace aCHADemia.ViewModel
{
    internal partial class AddPersonPageViewModel : ViewModelBase
    {

        [ObservableProperty]
        private Person _currentPerson = new Person();

        [ObservableProperty]
        private ObservableCollection<string> _personTypes;

        [ObservableProperty]
        private string _selectedPersonType;

        public AsyncRelayCommand AddPersonCommand
        {
            get;
        }

        public AddPersonPageViewModel()
        {

            // Initialize with 2 mock values
            PersonTypes = new ObservableCollection<string>
            {
                "Profesor",
                "Alumno"
            };

            SelectedPersonType = PersonTypes.FirstOrDefault();
            // default value for CurrentPerson
            CurrentPerson.PersonTypeId = 1;

            AddPersonCommand = new AsyncRelayCommand(AddPersonAsync);

        }

        partial void OnSelectedPersonTypeChanged(string value)
        {
            // DB: Profesor = 1, Alumno = 2
            if (value == "Profesor")
            {
                CurrentPerson.PersonTypeId = 1;
                CurrentPerson.PersonType = value;
            }
            else if (value == "Alumno")
            {
                CurrentPerson.PersonTypeId = 2;
                CurrentPerson.PersonType = value;
            }
        }

        private async Task AddPersonAsync()
        {
            // data validation
            if (string.IsNullOrWhiteSpace(CurrentPerson.CI) || !int.TryParse(CurrentPerson.CI, out int ciValue))
            {
                MessageBox.Show("La cédula debe ser un número válido");
                return;
            }

            if (string.IsNullOrWhiteSpace(CurrentPerson.Name))
            {
                MessageBox.Show("Debe ingresar un nombre");
                return;
            }

            try
            {
                // using query.yaml
                int rowsAffected = await App.Db.Execute(
                    DbType.Postgres,
                    Config.Queries.AddPerson.InsertPerson,
                    new NpgsqlParameter("@ci", ciValue),
                    new NpgsqlParameter("@person_type_id", CurrentPerson.PersonTypeId),
                    new NpgsqlParameter("@name", CurrentPerson.Name)
                );

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Persona agregada correctamente");
                    // reset model
                    CurrentPerson = new Person
                    {
                        // every query will have the same structure
                        PersonTypeId = CurrentPerson.PersonTypeId,
                        PersonType = CurrentPerson.PersonType
                    };
                }
                else
                {
                    MessageBox.Show("No se pudo agregar la persona");
                }
            }
            catch (NpgsqlException ex)
            {
                if (ex.SqlState == "23505") //Unique violation, same PK
                {
                    MessageBox.Show($"La cédula {CurrentPerson.CI} ya existe en el sistema");
                }
                else
                {
                    MessageBox.Show($"Error al agregar persona: {ex.Message}");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}");
            }
        }
    }
}