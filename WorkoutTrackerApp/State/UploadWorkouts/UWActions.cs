using Microsoft.AspNetCore.Components.Forms;
using WorkoutTrackerClassLibrary.ViewModels;

namespace WorkoutTrackerApp.State.UploadWorkouts
{
    public record UploadFileAction
    {
        public InputFileChangeEventArgs FileChange { get; set; }
        
    }

    public record UploadFileErrorAction
    {
        public UploadFileErrorAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
    }

    public record FilesLoadedAction
    {
        public FilesLoadedAction(List<Workout> workouts, string fileName)
        {
            Workouts = workouts;
            FileName = fileName;
        }

        public List<Workout> Workouts { get; set; }
        public string FileName { get; set; } 
    }
}
