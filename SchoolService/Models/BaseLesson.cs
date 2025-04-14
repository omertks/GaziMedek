namespace SchoolService.Models
{
    public class BaseLesson : BaseEntity
    {

        public string Name { get; set; }

        public string Code { get; set; }

        public string TotalAkts { get; set; }



        public int BranchId { get; set; }
        public Branch Branch { get; set; }


        public List<Lesson> Lessons { get; set; }
    }
}
