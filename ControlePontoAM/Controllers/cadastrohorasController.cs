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
        #region transformarHoraSeg
        public int transformarHoraSeg(string HORA)
        {
            int horaRetorno = Convert.ToInt32(HORA.Split(':')[0]);
            int minRetorno = Convert.ToInt32(HORA.Split(':')[1]);
            int conversaosegu = (horaRetorno * 3600) + (minRetorno * 60);
            return conversaosegu;
        }
        #endregion
        #region transformarsegHORA
        public string transformarsegHORA(int? SEG)
        {
            int? horaRetorno = (SEG / 3600);
            int? minRetorno = ((SEG - (horaRetorno * 60 * 60)) / 60);

            String _minuto = "00" + Convert.ToString(minRetorno);
            _minuto = _minuto.Substring(_minuto.Length - 2, 2);

            String _hora = "00" + Convert.ToString(horaRetorno);
            _hora = _hora.Substring(_hora.Length - 2, 2);
            String conversaosegu = _hora + ":" + _minuto;
            return conversaosegu;
        }
        #endregion
        public void calcularhoraExtra(cadastrohora cadastrohora)
        {
            //var horaseg = ((transformarHoraSeg(cadastrohora.horaEntradaInicio)- transformarHoraSeg(cadastrohora.horaSaidaInicio)) + (transformarHoraSeg(cadastrohora.horaEntradaTarde) - transformarHoraSeg(cadastrohora.horaSaidaTarde)) - transformarHoraSeg("08:00"));
          
            var result = (from c in db.cadastrohora                         
                          select c).ToList();
            int cont = result.Count;
            while (cont != 0)
            {
                using (Contexto entities = new Contexto())
                {
                    cadastrohora updatedcadastrohora = (from c in entities.cadastrohora
                                                        where c.codigo == cont
                                                        select c).FirstOrDefault();
                    if (String.IsNullOrWhiteSpace(updatedcadastrohora.horaEntradaInicio) != true && String.IsNullOrWhiteSpace(updatedcadastrohora.horaSaidaTarde) != true )
                    {
                        var horamanha = ((transformarHoraSeg(updatedcadastrohora.horaEntradaInicio) - transformarHoraSeg(updatedcadastrohora.horaSaidaInicio)));
                        var horatarde = ((transformarHoraSeg(updatedcadastrohora.horaEntradaTarde) - transformarHoraSeg(updatedcadastrohora.horaSaidaTarde)));
                        var Horariodia = transformarHoraSeg("08:00");
                        //var totalhoras = (Horariodia-(-(horamanha) + -(horatarde)));
                        var totalhorasteste = ( (-(horamanha) + -(horatarde))- Horariodia);                        
                        //var horaextra = transformarsegHORA(totalhoras);
                        var horaextrateste = transformarsegHORA(totalhorasteste);
                        updatedcadastrohora.horateste = horaextrateste;
                        entities.SaveChanges();
                    }
                }
                cont = cont - 1;
            }
        }
        // GET: cadastrohoras
        public ActionResult Index()
        {

            cadastrohora cadhora = new cadastrohora();
            DateTime datames = DateTime.Now;
            // var result= primeiroDiaDoMes.DayOfWeek;
            DateTime ultimoDiaDoMes = new DateTime(datames.Year, datames.Month, DateTime.DaysInMonth(datames.Year, datames.Month));
            var primeirodia = new DateTime(datames.Year, datames.Month, 1).ToString("dd");
            var Ultimodia = new DateTime(datames.Year, datames.Month, DateTime.DaysInMonth(datames.Year, datames.Month)).ToString("dd");
            int contprimeirodia = Convert.ToInt32(primeirodia);
            int contultimodia = Convert.ToInt32(Ultimodia);

            var result = (from c in db.cadastrohora
                          where c.mes == "07"
                          select c).FirstOrDefault();
            if (String.IsNullOrWhiteSpace(result.horaEntradaInicio) != true && String.IsNullOrWhiteSpace(result.horaSaidaTarde) != true)
            {
                calcularhoraExtra(result);
            }
            ViewBag.nomeusuario = result.usuario.nome;
            ViewBag.data = datames;
            if (result == null)
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
                //updatedcadastrohora.horaEntradaInicio = cadastrohora.horaEntradaInicio;
                updatedcadastrohora.horaEntradaInicio = (!string.IsNullOrEmpty(cadastrohora.horaEntradaInicio) ? cadastrohora.horaEntradaInicio : "");
                updatedcadastrohora.horaSaidaInicio = (!string.IsNullOrEmpty(cadastrohora.horaSaidaInicio) ? cadastrohora.horaSaidaInicio : ""); //cadastrohora.horaSaidaInicio;
                updatedcadastrohora.horaEntradaTarde = (!string.IsNullOrEmpty(cadastrohora.horaEntradaTarde) ? cadastrohora.horaEntradaTarde : ""); //cadastrohora.horaEntradaTarde;
                updatedcadastrohora.horaSaidaTarde = (!string.IsNullOrEmpty(cadastrohora.horaSaidaTarde) ? cadastrohora.horaEntradaTarde : ""); //.horaSaidaTarde;
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
