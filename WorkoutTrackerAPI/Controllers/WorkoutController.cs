using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net.Http;
using System.Security.Principal;
using System.Text.Json;
using WorkoutTrackerClassLibrary;
using WorkoutTrackerClassLibrary.ViewModels;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WorkoutTrackerAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class WorkoutController : ControllerBase
	{

		private readonly IMapper _mapper;
		public WorkoutController(IMapper mapper)
		{
			_mapper = mapper;
		}

        [HttpGet(Name = "GetWorkouts")]

		public IActionResult Get()
		{
			using (var db = new TrackerDataContext())
			{
				var workouts = db.Workouts.Include(w => w.ClassType).OrderBy(w => w.Date).ToList();
				
				return Ok(_mapper.Map<List<Workout>>(workouts));
			}           
		}

        [HttpPost(Name = "SetWorkouts")]
        public ActionResult Post(List<WorkoutDTO> Workouts)
        {
			List<ClassTypeDTO> types = Workouts.Select(x => x.ClassType).DistinctBy(y => y.TypeName).ToList();
			using (var db = new TrackerDataContext())
			{
				var existingTypes = db.ClassTypes.ToList();
				db.ClassTypes.AddRange(types.Where(t => !existingTypes.Any(et => et.TypeName == t.TypeName)));
				db.SaveChanges();
				
				existingTypes = db.ClassTypes.ToList();
				foreach (var workout in Workouts) 
				{
					workout.ClassType = existingTypes.Where(et => et.TypeName == workout.ClassType.TypeName).FirstOrDefault();
					db.Workouts.Add(workout);
				}
				db.SaveChanges();
				
			}
			return Ok();

           
        }
    }
}
