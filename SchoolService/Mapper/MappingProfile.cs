using AutoMapper;
using SchoolService.Dtos.Department;
using SchoolService.Dtos.Lesson;
using SchoolService.Dtos.University;
using SchoolService.Dtos.User;
using SchoolService.Models;

namespace SchoolService.Mapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {

            // User

            CreateMap<User, ResultUserDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName))
            .ForMember(dest => dest.LessonNames, opt => opt.MapFrom(src => src.Lessons.Select(l => l.Teacher.Name)))
            .ReverseMap();

            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();


            // Lesson

            CreateMap<Lesson, CreateLessonDto>().ReverseMap();

            CreateMap<Lesson, ResultLessonDto>()
                .ForMember(dest => dest.TeacherFullName, opt => opt.MapFrom(src => $"{src.Teacher.Name} {src.Teacher.Surname}"))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName))
                .ReverseMap();

            CreateMap<Lesson, UpdateLessonDto>().ReverseMap();



            // Department

            CreateMap<Department, CreateDepartmentDto>().ReverseMap();
            CreateMap<Department, UpdateDepartmentDto>().ReverseMap();
            CreateMap<Department, ResultDepartmentDto>()
                .ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src =>  src.University.UniversityName != null ? src.University.UniversityName: "Üni Ad Bilgisi Gelemedi"))
                .ReverseMap();

            // University

            CreateMap<University, CreateUniversityDto>().ReverseMap();
            CreateMap<University, UpdateUniversityDto>().ReverseMap();
            CreateMap<University, ResultUniversityDto>().ReverseMap();


        }
    }
}
