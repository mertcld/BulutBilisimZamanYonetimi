using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bulutbilisim.Models
{
    [Table("Kullanicilar")]
    public class Kullanicilar
    {
        public int id { get; set; }
        public string adsoyad { get; set; }

    }
}
