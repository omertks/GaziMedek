namespace UserService.Dtos.Teacher
{
    public class CreateTeacherDto
    {

        public string Name { get; set; }
        public string Surname { get; set; }

        public int UserId { get; set; }

        public string Role { get; set; }

        public int DepartmentId { get; set; }
    }
}
