using Microsoft.EntityFrameworkCore;
using SchoolService.Models;

namespace SchoolService.Db
{
    public class SchoolServiceDbContext : DbContext
    {

        public DbSet<University> Universities { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Branch> Branches { get; set; }


        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<BaseLesson> BaseLessons { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=school_db;Username=postgres;Password=gazi_postgre_medek;");

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



            // Depratment 1 - - - > 0..n Branch

            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Department)
                .WithMany(d => d.Branches)
                .HasForeignKey(b => b.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);


            // Branch 1 - - - - > 0..n Teacher
            
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Branch)
                .WithMany(b => b.Teachers)
                .HasForeignKey(t => t.BranchId); // bunun için onDelete bak


            // Branch 1 - - - - - > 0..1 BaseLesson

            modelBuilder.Entity<BaseLesson>()
                .HasOne(b => b.Branch)
                .WithMany(b => b.BaseLessons)
                .HasForeignKey(b => b.BranchId);



            // burada dikkat et

            // Teacher 1 - - - - - > 0..1 Lesson

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Teacher)
                .WithMany(t => t.Lessons)
                .HasForeignKey(l => l.TeacherId);


            // BaseLesson 1  ---  -- - -- - > 0..1 Lesson

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.BaseLesson)
                .WithMany(b => b.Lessons)
                .HasForeignKey(l => l.BaseLessonId)
                .OnDelete(DeleteBehavior.Cascade);

            




        }
    }
}
