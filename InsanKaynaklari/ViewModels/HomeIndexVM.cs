using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using InsanKaynaklari;
using InsanKaynaklari.Models;

namespace InsanKaynaklari.ViewModels
{
    public class HomeIndexVM
    {
        public HomeIndexVM()
        {
            SeciliDepartmanId = -1; 
        }

        public IPagedList<Personel> Personeller { get; set; }

        public List<Departman> Departmanlar { get; set; }
      
        public int SeciliDepartmanId { get; set; }

        public string SeciliDepartmanAd { get; set; }

    }

}