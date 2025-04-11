//using iTextSharp.text.pdf.parser;
//using iTextSharp.text.pdf;
//using static System.Net.Mime.MediaTypeNames;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;

namespace PdfExampleConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //PdfMetinDegistir("D:\\SistemAnalizi/docConvertPdf.pdf", 1, "BAHAR", "Bigisayar Programcılığı- Bil-101", "D:\\SistemAnalizi/cikti.pdf");

            DocxMetinDegistir("D:\\SistemAnalizi/kapak.docx", "DERS", "A", "");

        }

        //public static void PdfMetinDegistir(string pdfDosyaYolu, int sayfaNumarasi, string aranacakMetin, string yeniMetin, string ciktiDosyaYolu)
        //{
        //    using (PdfReader okuyucu = new PdfReader(pdfDosyaYolu))
        //    {
        //        using (PdfStamper damga = new PdfStamper(okuyucu, new FileStream(ciktiDosyaYolu, FileMode.Create)))
        //        {
        //            ITextExtractionStrategy strateji = new SimpleTextExtractionStrategy();
        //            string mevcutMetin = PdfTextExtractor.GetTextFromPage(okuyucu, sayfaNumarasi, strateji);

        //            if (mevcutMetin.Contains(aranacakMetin))
        //            {
        //                mevcutMetin = mevcutMetin.Replace(aranacakMetin, yeniMetin);

        //                // Burada, metni değiştirmek için PdfStamper'ı kullanmanız gerekecektir.
        //                // Bu, metnin konumunu bulmayı ve üzerine yeni metni yazmayı içerir.
        //                // Bu işlem, metnin karmaşıklığına ve konumuna bağlı olarak değişebilir.

        //                // Örnek olarak, basit bir metin değiştirme işlemi gösterilmiştir:
        //                damga.FormFlattening = true;
        //                for (int i = 1; i <= okuyucu.NumberOfPages; i++)
        //                {
        //                    if (i == sayfaNumarasi)
        //                    {
        //                        // Burada, metni değiştirmek için PdfContentByte'ı kullanmanız gerekebilir.
        //                        // PdfContentByte cb = damga.GetOverContent(i);
        //                        // ...
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        public static void DocxMetinDegistir(string docxDosyaYolu, string aranacakMetin, string yeniMetin, string ciktiDosyaYolu)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(docxDosyaYolu, true))
            {
                var metinler = wordDoc.MainDocumentPart.Document.Body.Descendants<Text>();

                foreach (Text metin in metinler)
                {
                    if (metin.Text.Contains(aranacakMetin))
                    {
                        metin.Text = metin.Text.Replace(aranacakMetin, yeniMetin);
                    }
                }
                wordDoc.Save();
            }
        }


    }
}
