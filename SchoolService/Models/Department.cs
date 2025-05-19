namespace SchoolService.Models
{
    public class Department : BaseEntity
    {

        public string DepartmentName { get; set; }

        public List<User> Users { get; set; } = new List<User>();

        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

        public int UniversityId { get; set; }
        public University University { get; set; }
    }
}
