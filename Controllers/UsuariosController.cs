using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrailersWeb.Models;

namespace TrailersWeb.Controllers
{
    public class UsuariosController : Controller
    {
        

        private bdtrailerEntities db = new bdtrailerEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Index(string usuario1, string clave)
        {
            //COMPARACION DE LOS DATOS INTRODUCIDOS EN LA VISTA CON LOS ALMACENADOS EN LA BASE DE DATOS
            var usuario = db.usuarios.FirstOrDefault(x => x.usuario1 == usuario1 && x.clave == clave);
            
            if (usuario != null) 
            {
                return RedirectToAction("Admin", "Trailer");
            }

            else
            {
                ViewBag.Mensaje = "Datos Incorrectos";
                return View();
            }

        }

        public ActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //CREACION DEL USUARIO, SE REGISTRAN LOS DATOS EN LA BASE DE DATOS 
        public ActionResult Crear([Bind(Include = "id,nombre,usuario1,clave")] usuario usu)
        {
       
                if (ModelState.IsValid)
                {
                try
                {
                    db.usuarios.Add(usu);
                    db.SaveChanges();
                    return RedirectToAction("Admin","Trailer");
                }
                catch (Exception e)
                {
                    ViewBag.Mensaje = "Datos Incorrectos";
                    return View();
                }
            }
            return View(usu);
        }


        public ActionResult Admin()
        {
            return View(db.trailers.ToList());

        }

    }
}