namespace SchoolService.Dtos.Teacher
{
    public class CreateTeacherDto
    {

        public string Name { get; set; }
        public string Surname { get; set; }

        public int UserId { get; set; }

        public int BranchId { get; set; }
    }
}
