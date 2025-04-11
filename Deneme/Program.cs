using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfService.Services;
using PdfService.Services.Interfaces;

namespace Deneme
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPdfEditingService pdfEditingService = new PdfEditingService();

            var titlePage = pdfEditingService.AddTitlePage("Programlama Temelleri","Bil-101", "Mahmut KASAP");

            Console.WriteLine("Kapak Oluşturuldu");

            pdfEditingService.AddTransitionPage("Programlama Temelleri","DERS DEVAM CIZELGELERI");

            Console.WriteLine("Yoklama Ara Sayfası Oluşturuldu !!!");
        }
    }
}
