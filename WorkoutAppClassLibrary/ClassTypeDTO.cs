using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutTrackerClassLibrary
{
	public class ClassTypeDTO
	{
		[Key]
        public int Id { get; set; }
		public string TypeName { get; set; }
        public ICollection<WorkoutDTO> Workouts { get; set; }
	}
}
