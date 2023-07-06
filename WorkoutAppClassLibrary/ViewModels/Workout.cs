using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTrackerClassLibrary.ViewModels
{
    public class Workout
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int ClassTypeId { get; set; }
        public ClassType ClassType { get; set; }
    }
}
