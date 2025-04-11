namespace PdfService.Dtos
{
    public class CreateMedekDto
    {

        // Generic olabilir birden fazla dosya yüklenebilir

        public IFormFile Icindekiler { get; set; }
        public IFormFile AktsEctsFormlari { get; set; }
        //public IFormFile SinavImzaCizergeleri { get; set; }
        //public IFormFile SinavIstatistikleri { get; set; }
        //public IFormFile VizeSorulari { get; set; }
        //public IFormFile VizeSinavDusukOrtaYuksekNotlar { get; set; }
        //public IFormFile FinalSorulari { get; set; }
        //public IFormFile FinalSinavDusukOrtaYuksekNotlar { get; set; }
        //public IFormFile ButSorulari { get; set; }
        //public IFormFile ResmiNotCizergesi { get; set; }
        //public IFormFile DegerlendirmeAnketleri { get; set; }
    }
}
