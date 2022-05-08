using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bulutbilisim.Models.DTO
{
    public class DenemeDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Adres { get; set; }
        public int SilinmisKayitMi { get; set; }
    }
    public class DenemeUpdateDTO
    {
      
        public string Name { get; set; }
        public string Adres { get; set; }
        public int SilinmisKayitMi { get; set; }
    }
}
