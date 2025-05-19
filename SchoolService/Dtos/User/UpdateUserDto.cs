using SchoolService.Enums;

namespace SchoolService.Dtos.User
{
    public class UpdateUserDto
    {

        public string Name { get; set; }
        public string Surname { get; set; }

        public int DepartmentId { get; set; }
        public Role Role { get; set; }


    }
}
