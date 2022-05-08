﻿using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bulutbilisim.Models
{
    [Table("TodoList")]
    public class TodoList
    {
        public int id { get; set; }
        public string gorevAdi { get; set; }
        public int kullaniciId { get; set; }
        public DateTime tarih { get; set; }
        public string aciklama { get; set; }
        public int etiketId { get; set; }
        public bool silinmisKayitMi { get; set; }

    }
}
