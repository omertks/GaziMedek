using Microsoft.EntityFrameworkCore;
using PdfService.Db;
using PdfService.Dtos;
using PdfService.Models;
using PdfService.Services.Interfaces;
using System.ComponentModel.Design;
using System.IO;
using System.Threading.Tasks;

namespace PdfService.Services
{
    public class PdfDbService : IPdfDbService
    {

        private readonly PdfServiceDbContext _context;


        public PdfDbService(PdfServiceDbContext context)
        {
            _context = context;
        }

        public async Task DeleteMedekFormById(int id)
        {
            var form = await _context.MedekForms.FindAsync(id);

            if(form == null)
            {
                throw new NullReferenceException();
            }

            // 1. Dosyanın bulunduğu **klasör yolunu** al
            string filePath = form.Path;
            string folderPath = Path.GetDirectoryName(filePath); // Dosyanın bulunduğu klasör

            // 2. **Dosya varsa, sil**
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            // 3. **Klasör varsa, tüm içeriğini sil**
            if (!string.IsNullOrEmpty(folderPath) && Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true); // `true` parametresiyle içindeki tüm dosyalar da silinir
            }

            _context.MedekForms.Remove(form);

            await _context.SaveChangesAsync();

        }

        public async Task<MedekForm> GetMedekFormById(int id)
        {
            var rs = await _context.MedekForms.FindAsync(id);

            return rs;
        }

        public async Task<List<ResultMedekFormDto>> GetMedekFormsForUser(int id)
        {
            var forms = await _context.MedekForms.Where(m => m.UserId == id).ToListAsync();

            List<ResultMedekFormDto> rs = new List<ResultMedekFormDto>();

            foreach (var form in forms)
            {
                rs.Add(
                    new ResultMedekFormDto()
                    {
                        Id = form.Id,
                        Name = form.Name,
                        UserId = form.UserId,
                        CreatedAt = form.CreatedAt
                    }
                    );
            }

            return rs;
        }

        public async Task MedekFormSaveToDb(CreateMedekFormDto createMedekFormDto)
        {

            MedekForm medekForm = new MedekForm()
            {
                Name = createMedekFormDto.Name,
                UserId = createMedekFormDto.UserId,
                Path = createMedekFormDto.Path
            };


            await _context.MedekForms.AddAsync(medekForm);

            await _context.SaveChangesAsync();
        }
    }
}
