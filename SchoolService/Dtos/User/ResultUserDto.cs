namespace SchoolService.Dtos.User
{
    public class ResultUserDto
    {

        public int Id { get; set; }           
        public string Name { get; set; }
        public string Surname { get; set; }

        public int UserId { get; set; }       
        public string Role { get; set; }      

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public List<string> LessonNames { get; set; }  // Opsiyonel: sadece adlar


    }
}
