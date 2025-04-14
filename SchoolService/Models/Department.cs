namespace SchoolService.Models
{
    public class Department : BaseEntity
    {

        public string DepartmentName { get; set; }


        public int UniversityId { get; set; }
        public University University { get; set; }

        public List<Branch> Branches { get; set; }

    }
}
