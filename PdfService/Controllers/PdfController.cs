using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using PdfService.Dtos;
using PdfService.Services.Interfaces;
using System.Reflection.Metadata;

namespace PdfService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PdfController : Controller
    {

        private readonly IConfiguration _configuration;

        private readonly IPdfEditingService _pdfEditingService;

        public PdfController(IConfiguration configuration, IPdfEditingService pdfEditingService)
        {
            if (configuration != null && pdfEditingService != null)
            {
                _configuration = configuration;

                _pdfEditingService = pdfEditingService;
            }
            else
            {
                Console.WriteLine("Servisler Null Geliyor");
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Erişti");
        }


        [HttpPost]
        public async Task<IActionResult> UploadPdf(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Dosya Gönderilemedi!!!");
            }
            else if (file.ContentType != "application/pdf")
            {
                return BadRequest("Sadece .pdf uzantılı dosyalar gönderilebilir!!!!");
            }
            else if (_configuration["PdfFolderPath"] == null)
            {
                Console.WriteLine("Path Değeri null!!!!");
                return BadRequest("Path Değeri null!!!!");
            }


            // Dosyayı işle

            var filePath = Path.Combine(_configuration["PdfFolderPath"], file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }


            return Ok(new
            {
                Result = "Başarılı Bir Şekilde Kaydedildi!!",
                FilePath = filePath
            });
        }

        [HttpPost("medek")]
        public async Task<IActionResult> ConvertMedekForm( int LessonId, int TeacherId,[FromForm] CreateMedekDto pdfs) // Burada List yerine kapsayıcı bir dto oluştur
        {

            var result = _pdfEditingService.ConvertMedekForm("Deneme","Den-111","Ömer Tekeş",pdfs);

            return Ok(result);
        }

        [HttpGet("kapak-getir")]
        public async Task<IFormFile> GetCoverByLesson(string lessonName, string lessonCode)
        {

            var pdfReader = new PdfReader("D:\\SistemAnalizi/icindekiler.pdf");

            var acroField = pdfReader.AcroFields;

            var acroForm = pdfReader.AcroForm;
          
            
            Console.WriteLine("sa");

            return null;

        }








    }
}
