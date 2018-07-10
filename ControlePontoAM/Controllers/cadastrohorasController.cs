using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControlePontoAM.Models.entidades;
using ControlePontoAM.Models.repositorio;

namespace ControlePontoAM.Controllers
{
    public class cadastrohorasController : Controller
    {
        private Contexto db = new Contexto();

        // GET: cadastrohoras
        public ActionResult Index()
        {
            cadastrohora cadhora = new cadastrohora();
            DateTime datames = DateTime.Now;          
           // var result= primeiroDiaDoMes.DayOfWeek;
            DateTime ultimoDiaDoMes = new DateTime(datames.Year, datames.Month, DateTime.DaysInMonth(datames.Year, datames.Month));
            var primeirodia =new DateTime(datames.Year, datames.Month, 1).ToString("dd");
            var Ultimodia = new DateTime(datames.Year, datames.Month, DateTime.DaysInMonth(datames.Year, datames.Month)).ToString("dd");
            int contprimeirodia=Convert.ToInt32(primeirodia);
            int contultimodia = Convert.ToInt32(Ultimodia);

            var result = (from c in db.cadastrohora
                                                             where c.mes == "07"
                                                             select c).FirstOrDefault();
            if (result==null )
            {
                while (contprimeirodia <= contultimodia)
                {
                    cadhora.dia = Convert.ToString(contprimeirodia);
                    cadhora.mes = "07";
                    cadhora.ano = "2018";
                    cadhora.codigo_usuario = 1;
                    cadhora.horaEntradaInicio = "";
                    cadhora.horaEntradaTarde = "";
                    cadhora.horaSaidaInicio = "";
                    cadhora.horaSaidaTarde = "";
                    db.cadastrohora.Add(cadhora);
                    db.SaveChanges();
                    contprimeirodia = contprimeirodia + 1;
                }
            }
            var cadastrohora = db.cadastrohora.Include(c => c.usuario);
            return View(db.cadastrohora.ToList());
        }

        // GET: cadastrohoras/Details/5
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

        // GET: cadastrohoras/Create
        public ActionResult Create()
        {
            ViewBag.codigo_usuario = new SelectList(db.usuario, "codigo", "nome");
            return View();
        }

        // POST: cadastrohoras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,codigo_usuario,horaEntradaInicio,horaSaidaInicio,horaEntradaTarde,horaSaidaTarde,observacao,dataAlteracao,dia,mes,ano")] cadastrohora cadastrohora)
        {
            ViewBag.codigo_usuario = new SelectList(db.usuario, "codigo", "nome", cadastrohora.codigo_usuario);
            if (ModelState.IsValid)
            {
                db.cadastrohora.Add(cadastrohora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codigo_usuario = new SelectList(db.usuario, "codigo", "nome", cadastrohora.codigo_usuario);
            return View(cadastrohora);
        }
        public ActionResult CadastroHoras()
        {

            var cadastrohora = db.cadastrohora.Include(c => c.usuario);
            return View(cadastrohora.ToList());
        }
        [HttpPost]
        public ActionResult Update(cadastrohora cadastrohora)
        {
            using (Contexto entities = new Contexto())
            {
                cadastrohora updatedcadastrohora = (from c in entities.cadastrohora
                                                    where c.codigo == cadastrohora.codigo
                                                    select c).FirstOrDefault();
                updatedcadastrohora.horaEntradaInicio = cadastrohora.horaEntradaInicio;
                updatedcadastrohora.horaSaidaInicio = cadastrohora.horaSaidaInicio;
                updatedcadastrohora.horaEntradaTarde = cadastrohora.horaEntradaTarde;
                updatedcadastrohora.horaSaidaTarde = cadastrohora.horaSaidaTarde;
                updatedcadastrohora.observacao = cadastrohora.observacao;

                entities.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        // GET: cadastrohoras/Edit/5
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
            return View(cadastrohora);
        }

        // POST: cadastrohoras/Edit/5
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
            return View(cadastrohora);
        }

        // GET: cadastrohoras/Delete/5
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

        // POST: cadastrohoras/Delete/5
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
