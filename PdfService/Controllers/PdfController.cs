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

        private readonly IPdfDbService _pdfDbService;

        public PdfController(IConfiguration configuration, IPdfEditingService pdfEditingService, IPdfDbService pdfDbService)
        {
            if (configuration != null && pdfEditingService != null)
            {
                _configuration = configuration;

                _pdfEditingService = pdfEditingService;

                _pdfDbService = pdfDbService;
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

        [HttpGet("medek/user/{id}")]
        public async Task<IActionResult> GetMedekFormsForUser(int id)
        {
            var rs = await _pdfDbService.GetMedekFormsForUser(id);

            return Ok(rs);
        }

        [HttpPost("medek/download/{id}")]
        public async Task<IActionResult> DownloadMedekForm(int id)
        {
            var form = await _pdfDbService.GetMedekFormById(id);

            if (form != null)
            {
                MemoryStream stream = new MemoryStream(System.IO.File.ReadAllBytes(form.Path)); // path deki dosyayı byte haline getirip rame yazdık

                stream.Position = 0; // bu dosyanın en başına gel demekmiş

                return File(stream, "application/pdf", "rapor.pdf");
            }
            else
            {
                return NotFound();
            }
        }


        [HttpDelete("medek/delete/{id}")]
        public async Task<IActionResult> DeleteMedekForm(int id)
        {

            await _pdfDbService.DeleteMedekFormById(id);

            return Ok(new { message = "Dosya, klasör ve veritabanı kaydı başarıyla silindi." });
        }
    }



}
