using Bulutbilisim.Models;
using Bulutbilisim.Models.OrmConfigration;
using Bulutbilisim.Models.ViewModels;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Bulutbilisim.Controllers
{
    [Route("ToDo")]
    public class ToDoController : Controller
    {
        public IActionResult Index()
        {

            var kullanicilar = DapperService._dbConnection.Query<Kullanicilar>("Select * from [dbo].[Kullanicilar] c with(nolock)", null, null, false, null, CommandType.Text).ToList();
            var etiketler = DapperService._dbConnection.Query<Etiketler>("Select * from [dbo].[Etiketler] c with(nolock)", null, null, false, null, CommandType.Text).ToList();
            var todoList = DapperService._dbConnection.Query<TodoList>("Select * from [dbo].[TodoList] c with(nolock)  ", null, null, false, null, CommandType.Text).ToList();
            var joinli = DapperService._dbConnection.Query<TodoEtiketKullanici>("Select c.id ,c.gorevadi,c.kullaniciId,k.adsoyad,c.tarih,c.aciklama,c.etiketId, e.ad as etiketAdi from [dbo].[TodoList] c with(nolock) left join Kullanicilar k on c.kullaniciId=k.id left join Etiketler e on e.id=c.etiketId where (c.silinmisKayitMi is null or c.silinmisKayitMi=0) order by id desc ", null, null, false, null, CommandType.Text).ToList();
            TodoViewModel todoviewmodel = new TodoViewModel();
            todoviewmodel.EtiketlerList = etiketler;
            todoviewmodel.KullanicilarList = kullanicilar;
            todoviewmodel.TodoListList = todoList;
            todoviewmodel.TodoEtiketKullanici = joinli;

            return View(todoviewmodel);
        }
        [HttpPost, Route("InsertTask")]
        [ValidateAntiForgeryToken]
        public JsonResult InsertTask(TodoList todoList)
        {
            ResponseModel res = DapperCrud.Crud(todoList, EnumCrudModel.Insert);
            SonucMesajlari notificationMessage = SonucMesajlariUI.Response(res.Code, res.Message);
            return Json(notificationMessage);
        } 
        [HttpPost, Route("DeleteTask")]
        public JsonResult DeleteTask(int gorevId)
        {
            string sqlQuery = string.Format("Delete   from [dbo].[TodoList]   where id= '{0}' select @@ROWCOUNT", gorevId);
            int res= DapperService._dbConnection.QuerySingleOrDefault<int>(sqlQuery, null, null, null);
            if (res > 0)
            {
                SonucMesajlari notificationMessage = SonucMesajlariUI.Response(100, "Başarılı!");
                return Json(notificationMessage);

            }
            else
            {
                SonucMesajlari notificationMessage = SonucMesajlariUI.Response(101, "Başarısız!");
                return Json(notificationMessage);
            }
          
        }


    }
}
