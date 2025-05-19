namespace PdfService.Models
{
    public class MedekForm
    {

        public int Id { get; set; }

        public int UserId { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
