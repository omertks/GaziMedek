namespace SchoolService.Models
{
    public class Lesson : BaseEntity
    {

        public string Name { get; set; }

        public string Code { get; set; }


        public string AcademicYear { get; set; } // Burayı stringe çek


        public int TeacherId { get; set; }
        public User Teacher { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
