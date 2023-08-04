using CsvHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.IO;
using WorkoutTrackerClassLibrary.ViewModels;
using WorkoutTrackerApp.Services;

namespace WorkoutTrackerApp.Pages
{
    
    public partial class Counter
    {
        [Inject]
        public IWorkoutDataService WorkoutDataService { get; set; }
        private List<Workout> _workouts = new();
        private string selectedFile;

        private void UpdateAndLoadFile(InputFileChangeEventArgs e) 
        {
            if (!e.File.Name.EndsWith(".csv")){
                InvalidFile();
                return;
            }
            LoadFile(e);            
        }

        private async void LoadFile(InputFileChangeEventArgs e)
        {
            List<dynamic> dynamicWorkoutsDays = new();
            try
            {
                selectedFile = e.File.Name;
                using MemoryStream ms = new ();
                await e.File.OpenReadStream().CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);
                if (ms != null)
                {
                    using var reader = new StreamReader(ms);
                    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                    dynamicWorkoutsDays = csv.GetRecords<dynamic>().ToList();
                }
                if (dynamicWorkoutsDays.Count == 0) throw new FileLoadException();
                _workouts = ProcessWorkouts(dynamicWorkoutsDays); 
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                InvalidFile();
            }
                
            
        }

        private List<Workout> ProcessWorkouts(dynamic dynamicWorkoutDays)
        {
            DateTime weekStart = DateTime.ParseExact(selectedFile.Substring(0, 6), "ddMMyy", null);
            List<Workout> workouts = new();
            int i = 0;
            foreach (var dynamicDay in dynamicWorkoutDays)
            {
                var workoutDate = weekStart.AddDays(i++);
                var dayDict = (IDictionary<string, object>)dynamicDay;
                foreach (var workout in dynamicDay)
                {
                    if (workout.Key.Equals("Day") || workout.Value.Equals(string.Empty) || workout.Value.Equals("-")) continue;

                    if (workoutDate.DayOfWeek == DayOfWeek.Saturday || workoutDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        if (workout.Value.StartsWith("Engine"))
                        {
                            workouts.Add(new Workout { Date = workoutDate, Description = workout.Value.ToString(), ClassType = new ClassType { TypeName = "Engine" } });
                            continue;
                        } 
                        else if(workout.Key.Equals("CrossFit B"))
                        {
                            workouts.Add(new Workout { Date = workoutDate, Description = workout.Value.ToString(), ClassType = new ClassType { TypeName = "CrossFit A" } });
                            continue;
                        }
                    }
                    workouts.Add(new Workout { Date = workoutDate, Description = workout.Value.ToString(), ClassType = new ClassType { TypeName = workout.Key } });
                    
                }
            }
            return workouts;
        }

        private void InvalidFile()
        {
            selectedFile = "Invalid File";
        }

        private async void Submit()
        {
            WorkoutDataService.SetWorkouts(_workouts);
        }
    }
}
