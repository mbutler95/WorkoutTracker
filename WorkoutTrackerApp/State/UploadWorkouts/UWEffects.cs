using Fluxor;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using WorkoutTrackerApp.Services;
using WorkoutTrackerClassLibrary.ViewModels;

namespace WorkoutTrackerApp.State.UploadWorkouts
{
    public class UWEffects
    {
        private readonly IState<UWState> _uwState;
        private IWorkoutDataService WorkoutDataService { get; set; }
        // Can use injection
        public UWEffects(IState<UWState> uwState, IWorkoutDataService workoutDataService)
        {
            _uwState = uwState;
            WorkoutDataService = workoutDataService;
        }
        

        [EffectMethod]
        public async Task LoadWorkouts(UploadFileAction action, IDispatcher dispatcher)
        {
            string fileName = action.FileChange.File.Name;

            if (!fileName.EndsWith(".csv"))
            {
                dispatcher.Dispatch(new UploadFileErrorAction("Invalid File: File Needs to be a CSV"));

            }
            else
            {
                try
                {
                    List<Workout> loadedWorkouts = await WorkoutDataService.LoadFile(action.FileChange);
                    dispatcher.Dispatch(new FilesLoadedAction(loadedWorkouts, fileName));
                }
                catch (Exception ex) 
                {
                    dispatcher.Dispatch(new UploadFileErrorAction(ex.Message));
                }
            }
            

            return;
        }

        [EffectMethod]
        public Task LogWorkoutsLoaded(FilesLoadedAction action, IDispatcher dispatcher)
        {
            Debug.WriteLine($"{action.Workouts.Count} Workouts Successfully Loaded");

            return Task.CompletedTask;
        }
    }
}
