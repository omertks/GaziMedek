namespace PdfService.Dtos
{
    public class CreateMedekDto
    {

        // Generic olabilir birden fazla dosya yüklenebilir

        public int UserId { get; set; }

        public string FakulteName { get; set; } = "TUSAS KAZAN MYO";

        // bu kısmı şimdi ekledim
        public string Name { get; set; } // burası uniq yada anlamlı olmalı
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }
        public string Year { get; set; }


        public string LessonName { get; set; }

        public string LessonCode { get; set; }


        public List<IFormFile> Icindekiler { get; set; }

        public List<IFormFile> AktsEctsFormlari { get; set; }

        public List<IFormFile> SinavImzaCizergeleri { get; set; }

        public List<IFormFile> SinavIstatistikleri { get; set; }

        public List<IFormFile> VizeSorulari { get; set; }

        public List<IFormFile> VizeSinavDusukOrtaYuksekNotlar { get; set; }

        public List<IFormFile> FinalSorulari { get; set; }

        public List<IFormFile> FinalSinavDusukOrtaYuksekNotlar { get; set; }

        public List<IFormFile> ButSorulari { get; set; }

        public List<IFormFile> ResmiNotCizergesi { get; set; }

        public List<IFormFile> DersDevamCizergesi { get; set; }

        public List<IFormFile> DegerlendirmeAnketleri { get; set; }

    }
}
