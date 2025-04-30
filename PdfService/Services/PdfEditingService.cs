using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfService.Dtos;
using PdfService.Services.Interfaces;
using System.IO;

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

        public Document AddTitlePage(string lessonName, string lessonCode, String teacherName, string fakulteName, string sourcePath)
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

                WriteToPdf(pdfWriter.DirectContent, fakulteName, 25, 300, 674, BaseFont.TIMES_BOLD);


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
            //BaseFont bf = BaseFont.CreateFont(font, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

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


        public string ConvertMedekForm(CreateMedekDto createMedekDto) // path dönecek
        {
            if (createMedekDto.Icindekiler == null)
            {
                throw new NullReferenceException("İçindekiler Null Geldi !! ");
            }

            String sourcePath = "D:\\SistemAnalizi\\Pdfs\\Deneme/" + createMedekDto.LessonName + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

            Directory.CreateDirectory(sourcePath);

            // Bizim Oluşturmamız Gereken Dökümanlar:

            var title_page = AddTitlePage(createMedekDto.LessonName, createMedekDto.LessonCode, createMedekDto.TeacherName, createMedekDto.FakulteName, sourcePath); // Create Title Page


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
                foreach (var Icindeki in createMedekDto.Icindekiler)
                {
                    MergePdfFiles(pdfCopy, Icindeki);
                }


                // burayı generic bir metot haline getir
                var pathAkts = AddTransitionPage(sourcePath, "AKTS-ECTS FORMLARI"); // Bu yöntemi title içinde yap

                AddTransitionPageToPdfCopy(pathAkts, pdfCopy);


                foreach (var AktsEctsForm in createMedekDto.AktsEctsFormlari)
                {
                    MergePdfFiles(pdfCopy, AktsEctsForm);
                }

                var pathSinavC = AddTransitionPage(sourcePath, "SINAV İMZA ÇİZELGELERİ");

                AddTransitionPageToPdfCopy(pathSinavC, pdfCopy);

                foreach (var pdf in createMedekDto.SinavImzaCizergeleri)
                {
                    MergePdfFiles(pdfCopy, pdf);
                }

                var pathSinavI = AddTransitionPage(sourcePath, "SINAV İSTATİSTİKLERİ");

                AddTransitionPageToPdfCopy(pathSinavI, pdfCopy);


                foreach (var pdf in createMedekDto.SinavIstatistikleri)
                {
                    MergePdfFiles(pdfCopy, pdf);
                }
                
                var pathVizeSS = AddTransitionPage(sourcePath, "VİZE SINAV SORULARI");

                AddTransitionPageToPdfCopy(pathVizeSS, pdfCopy);


                foreach (var pdf in createMedekDto.VizeSorulari)
                {
                    MergePdfFiles(pdfCopy, pdf);
                }

                var pathVizeSE = AddTransitionPage(sourcePath, "VİZE SINAVI EN DÜŞÜK, EN YÜKSEK VE ORTA NOTLAR ");

                AddTransitionPageToPdfCopy(pathVizeSE, pdfCopy);

                foreach (var pdf in createMedekDto.VizeSinavDusukOrtaYuksekNotlar)
                {
                    MergePdfFiles(pdfCopy, pdf);
                }

                var pathVizeFSS = AddTransitionPage(sourcePath, "FİNAL SINAV SORULARI");

                AddTransitionPageToPdfCopy(pathVizeSE, pdfCopy);

                foreach (var pdf in createMedekDto.FinalSorulari)
                {
                    MergePdfFiles(pdfCopy, pdf);
                }

                var pathFinalSE = AddTransitionPage(sourcePath, "FİNAL SINAVI EN DÜŞÜK, EN YÜKSEK VE ORTA NOTLAR ");

                AddTransitionPageToPdfCopy(pathFinalSE, pdfCopy);

                foreach (var pdf in createMedekDto.FinalSinavDusukOrtaYuksekNotlar)
                {
                    MergePdfFiles(pdfCopy, pdf);
                }

                var pathButunlemeSS = AddTransitionPage(sourcePath, "BÜTÜNLEME SINAV SORULARI");

                AddTransitionPageToPdfCopy(pathButunlemeSS, pdfCopy);

                foreach (var pdf in createMedekDto.ButSorulari)
                {
                    MergePdfFiles(pdfCopy, pdf);
                }

                var pathRNC = AddTransitionPage(sourcePath, "RESMİ NOT ÇİZELGELERİ");

                AddTransitionPageToPdfCopy(pathButunlemeSS, pdfCopy);

                foreach (var pdf in createMedekDto.ResmiNotCizergesi)
                {
                    MergePdfFiles(pdfCopy, pdf);
                }


                var pathDDC = AddTransitionPage(sourcePath, "DERS DEVAM ÇİZELGELERİ");

                AddTransitionPageToPdfCopy(pathDDC, pdfCopy);

                foreach (var pdf in createMedekDto.DersDevamCizergesi)
                {
                    MergePdfFiles(pdfCopy, pdf);
                }

                var pathDDA = AddTransitionPage(sourcePath, "DERS DEĞERLENDİRME ANKETLERİ");

                AddTransitionPageToPdfCopy(pathDDA, pdfCopy);

                foreach (var pdf in createMedekDto.DegerlendirmeAnketleri )
                {
                    MergePdfFiles(pdfCopy, pdf);
                }


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
