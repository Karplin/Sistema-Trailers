using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrailersWeb.Models;
using System.Net;
using System.Data.Entity;

namespace TrailersWeb.Controllers
{
    public class TrailerController : Controller
    {

        private bdtrailerEntities db = new bdtrailerEntities();
        // GET: Trailer
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        // ACTION RESULT QUE SIRVE PARA CREAR EL USUARIO, SE LLAMAN LOS CAMPOS DE LA BASE DE DATOS
        public ActionResult Crear([Bind(Include = "id,nombre,link,descripcion")] trailer trai)
        {

            if (ModelState.IsValid)
            {
                try
                { 
                    db.trailers.Add(trai);
                    db.SaveChanges();
                    return RedirectToAction("Admin", "Trailer");
                }

                catch (Exception e) // EL TRY CATCH FUNCIONA PARA PRESENTAR LOS ERRORES QUE PUEDAN PRESENTARSE
                {
                    ViewBag.Mensaje = "Datos Incorrectos";
                    return View();
                }
            }
            return View(trai);
        }


        public ActionResult Ver()
        {
            return View(db.trailers.ToList());
        }

        // EL EDITAR TE BUSCA EL CAMPO ID DE LA TABLA USUARIOS Y TE VERIFICA QUE ESE CAMBIO ES CORRECTO PARA QUE LOS DATOS SE CARGUEN.
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trailer tra = db.trailers.Find(id);
            if (tra == null)
            {
                return HttpNotFound();
            }
            return View(tra);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //CUANDO SE EDITEN LOS DATOS ESTOS SE GUARDAN EN LA BD 
        public ActionResult Editar([Bind(Include = "id,nombre,link,descripcion")] trailer tra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin", "Trailer");
            }
            return View(tra);
        }

         
        public ActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trailer tra = db.trailers.Find(id);
            if (tra == null)
            {
                return HttpNotFound();
            }
            return View(tra);
        }
        
        // POST: Home/Delete/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            trailer tra = db.trailers.Find(id);
            db.trailers.Remove(tra);
            db.SaveChanges();
            return RedirectToAction("Admin", "Trailer");
        }

        public ActionResult Admin()
        {
            return View(db.trailers.ToList());
        }

    }
}