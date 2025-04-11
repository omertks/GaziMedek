
using System;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace WordDeneme
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string xmlPath = "D:\\SistemAnalizi/kapak.xml";
            string wordPath = "D:\\SistemAnalizi/kapak.docx";
            string outputPath = "D:\\SistemAnalizi/Sonuc.docx";

            // XML dosyasını oku
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            File.Copy(wordPath, outputPath, true);

            // Word dosyasını aç
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(outputPath, true))
            {
                MainDocumentPart mainPart = wordDoc.MainDocumentPart;

                // Tüm içerik denetimlerini kontrol et
                foreach (SdtElement contentControl in mainPart.Document.Descendants<SdtElement>())
                {
                    // İçerik denetiminin içindeki metni al
                    SdtAlias alias = contentControl.Descendants<SdtAlias>().FirstOrDefault();
                    if (alias != null)
                    {
                        string fieldName = alias.Val.Value; // İçerik denetiminin adı

                        // XML'deki ilgili düğümü bul
                        XmlNode xmlNode = xmlDoc.SelectSingleNode($"/Document/CoverPage/{fieldName}");
                        if (xmlNode != null)
                        {
                            // İçerik denetiminin içindeki metni değiştir
                            Text text = contentControl.Descendants<Text>().FirstOrDefault();
                            if (text != null)
                            {
                                text.Text = xmlNode.InnerText;
                            }
                        }
                    }
                }

                wordDoc.Save();
            }

            Console.WriteLine("XML verisi başarıyla Word belgesine aktarıldı!");
        }
    }
}
