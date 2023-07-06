using WorkoutTrackerClassLibrary.ViewModels;

namespace WorkoutTrackerApp.Services
{
	public interface IWorkoutDataService
	{
		public Task<IEnumerable<Workout>> GetWorkouts();
        public void SetWorkouts(IEnumerable<Workout> workouts);
    }
}
