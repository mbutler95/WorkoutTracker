using Microsoft.AspNetCore.Components.Forms;
using WorkoutTrackerClassLibrary.ViewModels;

namespace WorkoutTrackerApp.Services
{
	public interface IWorkoutDataService
	{
		public Task<IEnumerable<Workout>> GetWorkouts();
        public void SetWorkouts(IEnumerable<Workout> workouts);
        public Task<List<Workout>> LoadFile(InputFileChangeEventArgs e);
    }
}
