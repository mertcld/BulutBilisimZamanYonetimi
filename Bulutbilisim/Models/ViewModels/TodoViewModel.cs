using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bulutbilisim.Models.ViewModels
{
    public class TodoViewModel
    {
        public Etiketler Etiketler { get; set; }
        public List<Etiketler> EtiketlerList { get; set; }
        public Kullanicilar Kullanicilar { get; set; }
        public List<Kullanicilar> KullanicilarList { get; set; }
        public List<TodoEtiketKullanici> TodoEtiketKullanici { get; set; }

        
        public TodoList TodoList { get; set; }
        public List<TodoList> TodoListList { get; set; }
    }
}
