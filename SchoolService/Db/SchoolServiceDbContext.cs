using Microsoft.EntityFrameworkCore;
using SchoolService.Models;

namespace SchoolService.Db
{
    public class SchoolServiceDbContext : DbContext
    {

        public DbSet<University> Universities { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder): base()
        //{
        //    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=school_db;Username=postgres;Password=gazi_postgre_medek;");
        //}

        public SchoolServiceDbContext(DbContextOptions options):base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // University 1 - - - > 0..n Department

            modelBuilder.Entity<Department>()
                .HasOne(d => d.University) // Her Dep bir üni si olur
                .WithMany(u => u.Departments) // uninin birden fazla depi olabilir
                .HasForeignKey(d => d.UniversityId)
                .OnDelete(DeleteBehavior.Cascade); // Uni sislinirse Deplerde silinsin



            // Department 1 - - - - > 0..n Teacher
            
            modelBuilder.Entity<User>()
                .HasOne(t => t.Department)
                .WithMany(b => b.Users)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);


            // Department 1 - - - - - > 0..1 Lesson

            modelBuilder.Entity<Lesson>()
                .HasOne(b => b.Department)
                .WithMany(b => b.Lessons)
                .HasForeignKey(b => b.DepartmentId);



            // burada dikkat et

            // Teacher 1 - - - - - > 0..1 Lesson

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Teacher)
                .WithMany(t => t.Lessons)
                .HasForeignKey(l => l.TeacherId);

        }
    }
}
