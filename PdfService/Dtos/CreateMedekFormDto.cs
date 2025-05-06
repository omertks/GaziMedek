namespace PdfService.Dtos
{
    public class CreateMedekFormDto
    {
        public int UserId { get; set; }

        public int LessonId { get; set; } // Burası boş olabilir

        public string Path { get; set; }

        public string Name { get; set; }
    }
}
