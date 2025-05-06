using PdfService.Db;
using PdfService.Dtos;
using PdfService.Models;
using PdfService.Services.Interfaces;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace PdfService.Services
{
    public class PdfSaveService : IPdfSaveService
    {

        private readonly PdfServiceDbContext _context;


        public PdfSaveService (PdfServiceDbContext context)
        {
            _context = context;
        }


        public async Task MedekFormSaveToDb(CreateMedekFormDto createMedekFormDto)
        {

            MedekForm medekForm = new MedekForm()
            {
                Name = createMedekFormDto.Name,
                UserId = createMedekFormDto.UserId,
                LessonId = createMedekFormDto.LessonId,
                Path = createMedekFormDto.Path
            };


            await _context.MedekForms.AddAsync(medekForm);

            await _context.SaveChangesAsync();
        }
    }
}
