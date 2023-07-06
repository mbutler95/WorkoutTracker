using Microsoft.AspNetCore.Components;
using WorkoutTrackerApp.Services;
using WorkoutTrackerClassLibrary.ViewModels;
using static System.Net.WebRequestMethods;
using CsvHelper;
using System.Globalization;

namespace WorkoutTrackerApp.Pages
{
	public partial class Workouts
	{
		[Inject]
		public IWorkoutDataService WorkoutDataService { get; set; }

		private List<Workout> workouts;

		protected override async Task OnInitializedAsync()
		{
			workouts = (await WorkoutDataService.GetWorkouts()).ToList();
		}
		
	}
}
