using SchoolService.Enums;

namespace SchoolService.Dtos.User
{
    public class CreateUserDto
    {

        public string Name { get; set; }
        public string Surname { get; set; }

        public int UserId { get; set; }

        public Role Role { get; set; } // buraya dikkat et

        public int DepartmentId { get; set; }


    }
}
