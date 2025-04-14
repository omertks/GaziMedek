namespace SchoolService.Models
{
    public class Teacher : BaseEntity
    {

        public string Name { get; set; }
        public string Surname { get; set; }

        public int UserId { get; set; } // Bu UserService'teki id'yi tutacak


        public int BranchId { get; set; }
        public Branch Branch { get; set; }


        public List<Lesson> Lessons { get; set; }

    }
}
