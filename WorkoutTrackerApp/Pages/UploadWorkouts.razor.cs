using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.IO;
using WorkoutTrackerClassLibrary.ViewModels;
using WorkoutTrackerApp.Services;
using WorkoutTrackerApp.State.UploadWorkouts;

using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace WorkoutTrackerApp.Pages
{
    
    public partial class UploadWorkouts : FluxorComponent
    {
        [Inject]
        public IDispatcher Dispatcher { get; set; }
        [Inject]
        private IState<UWState> UWState { get; set; }

        [Inject]
        public IWorkoutDataService WorkoutDataService { get; set; }

        private List<Workout> Workouts => UWState.Value.Workouts;

        private string SelectedFile => UWState.Value.FileName;

        private void UpdateAndLoadFile(InputFileChangeEventArgs e) 
        {
            Dispatcher.Dispatch(new UploadFileAction { FileChange = e });           
        }

        private void Submit()
        {
            WorkoutDataService.SetWorkouts(Workouts);
        }
    }
}
