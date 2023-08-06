using CsvHelper;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WorkoutTrackerApp.Pages;
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
		public async Task<IEnumerable<Workout>> GetWorkouts()
		{
			return await _httpClient.GetFromJsonAsync<List<Workout>>($"api/Workout", new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		}
        public async void SetWorkouts(IEnumerable<Workout> workouts)
        {
            await _httpClient.PostAsJsonAsync($"api/Workout", workouts);
        }

        public async Task<List<Workout>> LoadFile(InputFileChangeEventArgs e)
        {
            List<WorkoutCsvHelper> workoutsDays = new();
            
            using MemoryStream ms = new();
            await e.File.OpenReadStream().CopyToAsync(ms);
            ms.Seek(0, SeekOrigin.Begin);
            if (ms != null)
            {
                using var reader = new StreamReader(ms);
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {   
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var record = new WorkoutCsvHelper
                        {
                            Day = csv.GetField("Day"),
                            CrossFitA = csv.GetField("CrossFit A"),
                            CrossFitB = csv.GetField("CrossFit B"),
                            Strength = csv.GetField("Strength"),
                            Barbell = csv.GetField("Barbell"),
                            Endurance = csv.GetField("Endurance"),
                            Gymnastics = csv.GetField("Gymnastics")
                        };
                        workoutsDays.Add(record);
                    }
                }
            }

            if (workoutsDays.Count != 7) throw new FileLoadException("Invalid File Loaded: Check File Formatting");
            return ProcessWorkouts(workoutsDays, e.File.Name);
        }

        private static List<Workout> ProcessWorkouts(List<WorkoutCsvHelper> workoutDays, string fileName)
        {
            DateTime weekStart = DateTime.ParseExact(fileName.Substring(0, 6), "ddMMyy", null);
            List<Workout> workouts = new();
            int i = 0;
            foreach (var day in workoutDays)
            {
                var workoutDate = weekStart.AddDays(i++);
                CreateWorkout(workouts ,workoutDate, day.CrossFitA, "CrossFit A");
                CreateWorkout(workouts, workoutDate, day.CrossFitB, "CrossFit B");
                CreateWorkout(workouts, workoutDate, day.Strength, "Strength");
                CreateWorkout(workouts, workoutDate, day.Barbell, "Barbell");
                CreateWorkout(workouts, workoutDate, day.Endurance, "Endurance");
                CreateWorkout(workouts, workoutDate, day.Gymnastics, "Gymnastics");                
            }
            return workouts;
        }

        public static void CreateWorkout(List<Workout> workouts, DateTime date, string workout, string typeName)
        {
            if (workout.Equals(string.Empty) || workout.Equals("-")) return;
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                if (workout.StartsWith("Engine"))
                {
                    workouts.Add(new Workout { Date = date, Description = workout, ClassType = new ClassType { TypeName = "Engine" } });
                    return;
                }
                else if (typeName.Equals("CrossFit B"))
                {
                    workouts.Add(new Workout { Date = date, Description = workout, ClassType = new ClassType { TypeName = "CrossFit A" } });
                    return;
                }
            }
            workouts.Add(new Workout { Date = date, Description = workout, ClassType = new ClassType { TypeName = typeName } });
        }

        
    }
}
