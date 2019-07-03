using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InsanKaynaklari.Models
{
    [Table("Departmanlar")]

    public class Departman
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [DisplayName("Departman Adı")]

        public string DepartmanAd { get; set; }

        public virtual List<Personel> Personeller { get; set; }
    }
}