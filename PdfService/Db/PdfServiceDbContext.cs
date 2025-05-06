using Entity.Models;
using Microsoft.EntityFrameworkCore;
using PdfService.Models;

namespace PdfService.Db
{
    public class PdfServiceDbContext: DbContext
    {

        public DbSet<MedekForm> MedekForms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=pdf_service_db;Username=postgres;Password=gazi_postgre_medek;");

        }

    }
}
