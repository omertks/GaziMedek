namespace SchoolService.Models
{
    public class University: BaseEntity
    {

        public string UniversityName { get; set; }

        public List<Department>Departments { get; set; }


    }
}
