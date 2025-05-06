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


        [HttpPost("medek")]
        public async Task<IActionResult> ConvertMedekForm([FromForm] CreateMedekDto createMedekDto)
        {
            // Burada SchoolService ile iletişime geçerek teacher ve lesson bilgilerini çekicem


            var resultPath = await _pdfEditingService.ConvertMedekForm(createMedekDto);

            MemoryStream stream = new MemoryStream(System.IO.File.ReadAllBytes(resultPath)); // path deki dosyayı byte haline getirip rame yazdık

            stream.Position = 0; // bu dosyanın en başına gel demekmiş

            return File(stream, "application/pdf", "rapor.pdf");

        }
    }
}
