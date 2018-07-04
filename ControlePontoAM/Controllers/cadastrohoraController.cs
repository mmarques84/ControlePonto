using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControlePontoAM.Models.ADO.NET;

namespace ControlePontoAM.Controllers
{
    public class cadastrohoraController : Controller
    {
        private ControlePontoEntities2 db = new ControlePontoEntities2();
        //teste git
        // GET: cadastrohora
        public ActionResult Index()
        {
        
            var cadastrohora = db.cadastrohora.Include(c => c.usuario);
            return View(cadastrohora.ToList());
        }

        // GET: cadastrohora/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cadastrohora cadastrohora = db.cadastrohora.Find(id);
            if (cadastrohora == null)
            {
                return HttpNotFound();
            }
            return View(cadastrohora);
        }

        // GET: cadastrohora/Create
        public ActionResult Create()
        {
            ViewBag.codigo_usuario = new SelectList(db.usuario, "codigo", "nome");
            return View();
        }

        // POST: cadastrohora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,codigo_usuario,horaEntradaInicio,horaSaidaInicio,horaEntradaTarde,horaSaidaTarde,observacao,dataAlteracao,dia,mes,ano")] cadastrohora cadastrohora)
        {
            if (ModelState.IsValid)
            {
                db.cadastrohora.Add(cadastrohora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codigo_usuario = new SelectList(db.usuario, "codigo", "nome", cadastrohora.codigo_usuario);
            return View(cadastrohora);
        }

        // GET: cadastrohora/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cadastrohora cadastrohora = db.cadastrohora.Find(id);
            if (cadastrohora == null)
            {
                return HttpNotFound();
            }
            ViewBag.codigo_usuario = new SelectList(db.usuario, "codigo", "nome", cadastrohora.codigo_usuario);
            return View(cadastrohora);
        }

        // POST: cadastrohora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,codigo_usuario,horaEntradaInicio,horaSaidaInicio,horaEntradaTarde,horaSaidaTarde,observacao,dataAlteracao,dia,mes,ano")] cadastrohora cadastrohora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cadastrohora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codigo_usuario = new SelectList(db.usuario, "codigo", "nome", cadastrohora.codigo_usuario);
            return View(cadastrohora);
        }

        // GET: cadastrohora/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cadastrohora cadastrohora = db.cadastrohora.Find(id);
            if (cadastrohora == null)
            {
                return HttpNotFound();
            }
            return View(cadastrohora);
        }

        // POST: cadastrohora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cadastrohora cadastrohora = db.cadastrohora.Find(id);
            db.cadastrohora.Remove(cadastrohora);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
