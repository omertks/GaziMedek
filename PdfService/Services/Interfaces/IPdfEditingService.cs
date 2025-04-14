using iTextSharp.text;
using Org.BouncyCastle.Asn1.Mozilla;
using PdfService.Dtos;

namespace PdfService.Services.Interfaces
{
    public interface IPdfEditingService
    {

        // Ana metod aşşağıdakileri kullanacak
        public string ConvertMedekForm(string lessonName, string lessonCode, String teacherName, CreateMedekDto pdfs);

        public Document AddTitlePage(string lessonName, string lessonCode, String teacherName, string sourcePath);

        public string AddTransitionPage(string sourcePath, string content);


        public Document AddLastPage(Document src);
        public Document AddContentsPage(Document src);
        public Document AddAboutPage(Document src);

    }
}
