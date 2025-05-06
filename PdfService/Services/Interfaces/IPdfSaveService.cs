using PdfService.Dtos;
using PdfService.Models;

namespace PdfService.Services.Interfaces
{
    public interface IPdfSaveService
    {

        public Task MedekFormSaveToDb(CreateMedekFormDto createMedekFormDto);

    }
}
