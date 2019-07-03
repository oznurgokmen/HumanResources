using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InsanKaynaklari.Models
{
    [Table("Personeller")]

    public class Personel
    {
        public int Id { get; set; }

        [DisplayName("Fotoğraf")]

        public string FotoAd { get; set; }

        [DisplayName("Departman Adı")]

        public int DepartmanId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage ="Ad alanı gereklidir!")]
        [DisplayName("Ad")]

        public string PersonelAd { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Soyad alanı gereklidir!")]
        [DisplayName("Soyad")]

        public string PersonelSoyad { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Email alanı gereklidir!")]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Lütfen Email formatıyla giriş yapınız!")]       

        public string PersonelEmail { get; set; }

        [Required]
        [DisplayName("Telefon")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Telefon numarası 10 rakamdan oluşmalıdır!")]

        public string TelefonNo { get; set; }

        [Required]
        [DisplayName("İşe Başlama Tarihi")]

        public DateTime? IseBaslamaTarihi { get; set; }

        [DisplayName("Maaş")]

        public decimal Maas { get; set; }

        public virtual Departman Departman { get; set; }

    }
}