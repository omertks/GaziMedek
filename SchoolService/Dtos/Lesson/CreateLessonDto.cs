namespace SchoolService.Dtos.Lesson
{
    public class CreateLessonDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string AcademicYear { get; set; }

        public int TeacherId { get; set; }
        public int DepartmentId { get; set; }
    }

}
