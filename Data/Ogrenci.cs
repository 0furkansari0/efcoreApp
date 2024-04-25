using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Ogrenci
    {
        [Key]
        [DisplayName("Id")]
        public int OgrenciId { get; set; }
        [DisplayName("Öğrenci Ad")]
        public string? OgrenciAd { get; set; }
        [DisplayName("Öğrenci Soyad")]
        public string? OgrenciSoyad { get; set; }
        [DisplayName("Eposta")]
        public string? Eposta { get; set; }
        [DisplayName("Telefon")]
        public string? Telefon { get; set; }

    }
}