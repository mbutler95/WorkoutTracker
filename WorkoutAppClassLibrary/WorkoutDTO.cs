using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutTrackerClassLibrary
{
	public class WorkoutDTO
	{
		[Key]
        public int Id { get; set; }
		public DateTime Date{ get; set; }
		public string Description { get; set; }
		public int ClassTypeId { get; set; }
		public ClassTypeDTO ClassType { get; set; }

	}
}
