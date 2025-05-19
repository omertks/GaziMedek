using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace PdfService.Dtos
{
    public class ResultMedekFormDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        //public string Path { get; set; } // path eklenebilir

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
