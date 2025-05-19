namespace SchoolService.Dtos.Lesson
{
    public class ResultLessonDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public string AcademicYear { get; set; }

        public int TeacherId { get; set; }
        public string TeacherFullName { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

}
