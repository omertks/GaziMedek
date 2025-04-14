namespace SchoolService.Models
{
    public class Lesson : BaseEntity
    {


        public DateTime AcademicYear { get; set; }



        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }


        public int BaseLessonId { get; set; }
        public BaseLesson BaseLesson { get; set; }

    }
}
