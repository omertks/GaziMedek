
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolService.Dtos.Teacher;
using SchoolService.Models;
using SchoolService.Repositories.Interfaces;
using SchoolService.Services.Interfaces;
using System.Threading.Tasks;

namespace SchoolService.Services
{
    public class TeacherService : ITeacherService
    {

        private readonly ITeacherRepository _teacherRepository;

        private readonly IBranchRepository _branchRepository;

        public TeacherService(ITeacherRepository teacherRepository, IBranchRepository branchRepository)
        {
            _teacherRepository = teacherRepository;

            _branchRepository = branchRepository;
        }


        public void DeleteTeacher(int id)
        {
            throw new NotImplementedException();
        }

        public Teacher GetTeacherById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Teacher>> GetTeachers()
        {
            return await _teacherRepository.GetListAsync();
        }

        public async Task SaveTeacher(CreateTeacherDto createTeacherDto)
        {

            //Branch branch = await _branchRepository.GetByIdAsync(createTeacherDto.BranchId); 

            //if(branch == null)
            //{
            //    throw new NullReferenceException("Branch Null Geldi");
                
            //}

            Teacher teacher = new Teacher()
            {
                Name = createTeacherDto.Name,
                Surname = createTeacherDto.Surname,
                BranchId = createTeacherDto.BranchId,
                UserId = createTeacherDto.UserId
            };

            await _teacherRepository.CreateAsync(teacher);

           
        }

        public void UpdateTeacher(Teacher teacher, int id)
        {
            throw new NotImplementedException();
        }
    }
}
