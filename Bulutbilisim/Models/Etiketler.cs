using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bulutbilisim.Models
{
    [Table("Etiketler")]
    public class Etiketler
    {
        public int id { get; set; }
        public string ad { get; set; }
        public string sinifAdi { get; set; }
    }
}
