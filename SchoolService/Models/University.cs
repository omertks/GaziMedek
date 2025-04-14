namespace SchoolService.Models
{
    public class University: BaseEntity
    {

        public string UniverstyName { get; set; }

        public List<Department>Departments { get; set; }




        // Ticarileştirirsen
        //public string LogoUrl { get; set; }

    }
}
