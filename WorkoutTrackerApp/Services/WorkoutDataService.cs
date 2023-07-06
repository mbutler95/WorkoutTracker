using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WorkoutTrackerClassLibrary;
using WorkoutTrackerClassLibrary.ViewModels;

namespace WorkoutTrackerApp.Services
{
	public class WorkoutDataService : IWorkoutDataService
	{
		private readonly HttpClient _httpClient;

		public WorkoutDataService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<IEnumerable<WorkoutTrackerClassLibrary.ViewModels.Workout>> GetWorkouts()
		{
			return await _httpClient.GetFromJsonAsync<List<WorkoutTrackerClassLibrary.ViewModels.Workout>>($"api/Workout", new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		}

        public async void SetWorkouts(IEnumerable<WorkoutTrackerClassLibrary.ViewModels.Workout> workouts)
        {
            await _httpClient.PostAsJsonAsync($"api/Workout", workouts);
        }
    }
}
