namespace SchoolService.Models
{
    public class Branch: BaseEntity
    {
        public string BranchName { get; set; }


        public int DepartmentId { get; set; }
        public Department Department { get; set; }


        public List<Teacher> Teachers{ get; set; }



        public List<BaseLesson> BaseLessons { get; set; }
        public List<Lesson> Lessons { get; set; }

    }
}
