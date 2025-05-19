using UserService.Enums;

namespace UserService.Dtos.User
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserRole Role { get; set; } // UserService tarafında tanımlı olan UserRole enum'ı, örneğin: ADMIN, MANAGER, TEACHER
        public int DepartmentId { get; set; }
    }
}
