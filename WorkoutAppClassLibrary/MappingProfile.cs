using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WorkoutTrackerClassLibrary.ViewModels;

namespace WorkoutTrackerClassLibrary
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<ClassType, ClassTypeDTO>();
            CreateMap<ClassTypeDTO, ClassType>();
            CreateMap<Workout, WorkoutDTO>();
            CreateMap<WorkoutDTO, Workout>().ForMember(dest => dest.ClassType, x => x.MapFrom(src => src.ClassType));
        }
    }
}
