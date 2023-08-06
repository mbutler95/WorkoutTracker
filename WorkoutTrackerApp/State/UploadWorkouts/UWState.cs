using Fluxor;
using WorkoutTrackerClassLibrary.ViewModels;

namespace WorkoutTrackerApp.State.UploadWorkouts
{
    public record UWState
    {
        public string FileName { get; init; }
        public List<Workout> Workouts { get; init; }
    }

    public class USFeatureState : Feature<UWState>
    {
        public override string GetName() => nameof(UWState);

        protected override UWState GetInitialState()
        {
            return new UWState
            {
                FileName = ""
            };
        }        
    }
}
