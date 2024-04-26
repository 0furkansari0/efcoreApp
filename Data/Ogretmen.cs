
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Ogretmen
    {
        [Key]
        [DisplayName("Öğretmen Id")]
        public int OgretmenId { get; set; }

        public string? Ad { get; set; }
        public string? Soyad { get; set; }

        [DisplayName("Öğretmen Ad Soyad")]
        public string AdSoyad{
            get{
                return this.Ad +" "+ this.Soyad;
            }
        }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }

        [DisplayName("İşe Başlama Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}", ApplyFormatInEditMode =false)]
        public DateTime BaslamaTarihi { get; set; }
        public ICollection<Kurs> Kurslar { get; set; } = new List<Kurs>();
    }
}