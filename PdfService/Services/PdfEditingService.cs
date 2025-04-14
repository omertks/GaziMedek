using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http.HttpResults;
using PdfService.Dtos;
using PdfService.Services.Interfaces;
using System.Reflection.PortableExecutable;

namespace PdfService.Services
{
    public class PdfEditingService : IPdfEditingService
    {
        public Document AddAboutPage(Document src)
        {
            throw new NotImplementedException();
        }

        public Document AddContentsPage(Document src)
        {
            throw new NotImplementedException();
        }

        public Document AddLastPage(Document src)
        {
            throw new NotImplementedException();
        }

        public Document AddTitlePage(string lessonName, string lessonCode, String teacherName, string sourcePath)
        {

            // Document nesnesi oluştur
            Document doc = new Document(PageSize.A4);

            string path = sourcePath + "/title_page.pdf";


            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {

                PdfWriter pdfWriter = PdfWriter.GetInstance(doc, stream);

                pdfWriter.Open();

                // PDF aç
                doc.Open();

                // x en fazla 595 y en fazla 842 olabilir.

                WriteToPdf(pdfWriter.DirectContent, "TUSAS KAZAN MYO", 25, 300, 674, BaseFont.TIMES_BOLD);


                WriteToPdf(pdfWriter.DirectContent, lessonName + " -" + lessonCode, 15, 300, 420, BaseFont.TIMES_BOLD);


                WriteToPdf(pdfWriter.DirectContent, "2024-2025 BAHAR", 15, 300, 350, BaseFont.TIMES_BOLD);


                WriteToPdf(pdfWriter.DirectContent, "DERS DOSYASI", 15, 300, 250, BaseFont.TIMES_BOLD);


                WriteToPdf(pdfWriter.DirectContent, teacherName, 15, 300, 50, BaseFont.TIMES_BOLD);

                // PDF'i kapat


                doc.Close();

                return doc;
            }
        }

        public string AddTransitionPage(string sourcePath, string content)
        {
            // Document nesnesi oluştur
            Document doc = new Document(PageSize.A4);

            String path = sourcePath + "/T" + DateTime.Now.Millisecond.ToString() + DateTime.Now.Second.ToString() + ".pdf";


            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(doc, stream);

                pdfWriter.Open();

                // PDF aç
                doc.Open();

                // x en fazla 595 y en fazla 842 olabilir.

                WriteToPdf(pdfWriter.DirectContent, content, 20, 300, 420, BaseFont.TIMES_ROMAN);

                // PDF'i kapat
                doc.Close();

                return path;
            }
        }

        private void WriteToPdf(PdfContentByte cb, String content, int fontSize = 20, /*BaseColor color,*/ int x = 20, int y = 20, string font = BaseFont.TIMES_BOLD)
        {
            cb.BeginText();
            BaseFont bf = BaseFont.CreateFont(font, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            // mETNİ ORTALAMAK İÇİN basılacak metnin genişliğinin alınması
            float textWidth = bf.GetWidthPoint(content, fontSize);

            float centeredX = x - (textWidth / 2); // Ortalamak için metnin yarı genişliğini x ten çıkar


            cb.SetTextMatrix(centeredX, y);

            cb.SetFontAndSize(bf, fontSize);
            cb.SetColorFill(BaseColor.BLACK); // Yazı rengi
            //cb.SetTextMatrix(x, y); // X ve Y koordinatları. Bunda ortalam sıkıntısı var
            cb.ShowText(content);
            cb.EndText();
        }

        public void MergePdfFiles(PdfCopy pdfCopy, IFormFile pdfFile)
        {
            if (pdfCopy == null)
            {
                throw new ArgumentNullException(nameof(pdfCopy), "PdfCopy nesnesi null olamaz.");
            }

            if (pdfFile != null && pdfFile.Length > 0)
            {
                using (var inputStream = pdfFile.OpenReadStream())
                {
                    PdfReader pdfReader = new PdfReader(inputStream);

                    if (pdfReader.NumberOfPages == 0)
                    {
                        throw new InvalidOperationException("PDF dosyasında hiç sayfa bulunamadı.");
                    }

                    for (int i = 1; i <= pdfReader.NumberOfPages; i++)
                    {
                        PdfImportedPage page = pdfCopy.GetImportedPage(pdfReader, i);

                        if (page == null)
                        {
                            throw new InvalidOperationException($"PDF sayfası ({i}. sayfa) yüklenirken hata oluştu.");
                        }

                        pdfCopy.AddPage(page);
                    }
                }
            }
        }


        public string ConvertMedekForm(string lessonName, string lessonCode, String teacherName, CreateMedekDto pdfs) // path dönecek
        {
            if (pdfs.Icindekiler == null)
            {
                throw new NullReferenceException("İçindekiler Null Geldi !! ");
            }

            String sourcePath = "D:\\SistemAnalizi\\Pdfs\\Deneme/" + lessonName + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

            Directory.CreateDirectory(sourcePath);

            // Bizim Oluşturmamız Gereken Dökümanlar:

            var title_page = AddTitlePage(lessonName, lessonCode, teacherName, sourcePath); // Create Title Page


            //Gönderilen Dökümanların Pdf e Eklenmesi

            Document result = new Document(PageSize.A4);

            using (FileStream outputStream = new FileStream(sourcePath + "/medek.pdf", FileMode.Create))
            {
                PdfCopy pdfCopy = new PdfCopy(result, outputStream);

                result.Open();

                using (PdfReader pdfReaderTitle = new PdfReader(sourcePath + "/title_page.pdf"))
                {
                    pdfCopy.AddPage(pdfCopy.GetImportedPage(pdfReaderTitle, 1)); // Title Page'in eklenmesi
                }


                // İçindekiler sayfasının eklenmesi
                foreach (var Icindeki in pdfs.Icindekiler)
                {
                    MergePdfFiles(pdfCopy, Icindeki);
                }


                // burayı generic bir metot haline getir
                var pathAkts = AddTransitionPage(sourcePath, "AKTS-ECTS FORMLARI"); // Bu yöntemi title içinde yap

                AddTransitionPageToPdfCopy(pathAkts, pdfCopy);


                foreach (var AktsEctsForm in pdfs.AktsEctsFormlari)
                {
                    MergePdfFiles(pdfCopy, AktsEctsForm);
                }


                var pathSinavC = AddTransitionPage(sourcePath, "SINAV İMZA ÇİZELGELERİ");

                AddTransitionPageToPdfCopy(pathSinavC, pdfCopy);

                result.Close();

                

            }

            return sourcePath + "/medek.pdf";

        }

        public void AddTransitionPageToPdfCopy(string path, PdfCopy pdfCopy)
        {
            using (PdfReader pdfReader = new PdfReader(path))
            {

                // Burası sayfa sayısı kadar dönebilir
                pdfCopy.AddPage(pdfCopy.GetImportedPage(pdfReader, 1));
            }
        }

    }
}
