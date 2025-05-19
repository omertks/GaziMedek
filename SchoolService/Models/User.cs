using Microsoft.AspNetCore.Identity;
using SchoolService.Enums;

namespace SchoolService.Models
{
    public class User : BaseEntity
    {

        public string Name { get; set; }

        public string Surname { get; set; }

        public int UserId { get; set; } // Bu UserService'teki id'yi tutacak

        public Role Role { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }


        public List<Lesson> Lessons { get; set; }

    }
}
