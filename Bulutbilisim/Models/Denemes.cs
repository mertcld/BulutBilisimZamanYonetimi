using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bulutbilisim.Models
{
    [Table("Denemes")]
    public class Denemes
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Adres { get; set; }
        public int SilinmisKayitMi { get; set; }
    }
}
