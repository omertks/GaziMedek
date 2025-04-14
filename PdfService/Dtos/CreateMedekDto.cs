namespace PdfService.Dtos
{
    public class CreateMedekDto
    {

        // Generic olabilir birden fazla dosya yüklenebilir

        public int TeacherId { get; set; }
        
        public int LessonId { get; set; }


        public List<IFormFile> Icindekiler { get; set; }

        public List<IFormFile> AktsEctsFormlari { get; set; }

        //public List<IFormFile> SinavImzaCizergeleri { get; set; }

        //public List<IFormFile> SinavIstatistikleri { get; set; }

        //public List<IFormFile> VizeSorulari { get; set; }

        //public List<IFormFile> VizeSinavDusukOrtaYuksekNotlar { get; set; }

        //public List<IFormFile> FinalSorulari { get; set; }

        //public List<IFormFile> FinalSinavDusukOrtaYuksekNotlar { get; set; }

        //public List<IFormFile> ButSorulari { get; set; }

        //public List<IFormFile> ResmiNotCizergesi { get; set; }

        //public List<IFormFile> DegerlendirmeAnketleri { get; set; }

    }
}
