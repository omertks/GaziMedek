using Microsoft.AspNetCore.Mvc;
using PdfService.Dtos;
using PdfService.Models;

namespace PdfService.Services.Interfaces
{
    public interface IPdfDbService
    {

        public Task MedekFormSaveToDb(CreateMedekFormDto createMedekFormDto);

        public Task<List<ResultMedekFormDto>> GetMedekFormsForUser(int id);

        public Task<MedekForm> GetMedekFormById(int id);

        public Task DeleteMedekFormById(int id);
    }
}
