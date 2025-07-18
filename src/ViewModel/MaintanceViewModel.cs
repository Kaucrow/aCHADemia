using aCHADemia.MVVM;
using aCHADemia.View.Pages;
using System.Windows;

namespace aCHADemia.ViewModel
{
    internal class MaintanceViewModel : ViewModelBase
    {
        public MaintanceViewModel()
        {
            NavigateToAddPersonCommand = new RelayCommand(execute => NavigateTo<AddPersonPage>());
            NavigateToPersonMaintenanceCommand = new RelayCommand(execute => NavigateTo<MaintenancePersonPage>());
            NavigateToCourseMaintenanceCommand = new RelayCommand(execute => NavigateTo<MaintenanceCoursePage>());
            NavigateToSubjectMaintenanceCommand = new RelayCommand(execute => NavigateTo<MaintenanceSubjectPage>());
            NavigateToSectionMaintenanceCommand = new RelayCommand(execute => NavigateTo<MaintenanceSectionPage>());

        }

        public RelayCommand NavigateToAddPersonCommand { get; }
        public RelayCommand NavigateToPersonMaintenanceCommand { get; }
        public RelayCommand NavigateToCourseMaintenanceCommand { get; }
        public RelayCommand NavigateToSubjectMaintenanceCommand { get; }
        public RelayCommand NavigateToSectionMaintenanceCommand { get; }
    }
}
