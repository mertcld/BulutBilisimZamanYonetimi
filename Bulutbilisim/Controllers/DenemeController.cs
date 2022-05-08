
using Bulutbilisim.Models;
using Bulutbilisim.Models.DTO;
using Bulutbilisim.Models.OrmConfigration;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Bulutbilisim.Controllers
{
    public class DenemeController : Controller
    {
        // GET: DenemeController
        public ActionResult Index()
        {
            var res= DapperService._dbConnection.Query<Denemes>("Select * from [dbo].[Denemes] c with(nolock)", null, null, false, null, CommandType.Text).ToList();
            return View(res);
        }

        // GET: DenemeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DenemeController/Create
        public ActionResult Create()
        {
 
            return View();
        }

        // POST: DenemeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Denemes deneme)
        {
             ResponseModel res= DapperCrud.Crud(deneme, EnumCrudModel.Insert);
            SonucMesajlari notificationMessage = SonucMesajlariUI.Response(res.Code, res.Message);
            return Json(notificationMessage);
        }

        // GET: DenemeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DenemeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DenemeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DenemeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
