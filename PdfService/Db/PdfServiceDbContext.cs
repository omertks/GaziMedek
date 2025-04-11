using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace PdfService.Db
{
    public class PdfServiceDbContext: DbContext
    {

        // tables
        public DbSet<Pdf> pdfs { get; set; }

        public DbSet<PdfTransactionLog> pdf_transaction_logs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Kullanılacak db yi buraya gir
        }

    }
}
