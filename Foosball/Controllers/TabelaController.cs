using Foosball.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace Foosball.Controllers
{
    public class TabelaController : Controller
    {
        // GET: Tabela
        public ActionResult Index()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var rekordy = session.Query<Tabela>().Fetch(x => x.Druzyna).OrderByDescending(x=>x.Punkty).ToList();

                if (Session["UserID"] == null)
                {
                    ViewBag.info = 1;
                }
                else ViewBag.info = 0;
                
                return View(rekordy);
            }
        }

        // GET: Tabela/Details/5
        public ActionResult Details(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }
                var tabela = session.Get<Tabela>(id);
                NHibernateUtil.Initialize(tabela.Druzyna);
                return View(tabela);
            }
        }

        // GET: Tabela/Create
        public ActionResult Create()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ViewBag.druzyny = session.Query<Druzyna>().ToList();
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }
                return View();
            }
        }

        // POST: Tabela/Create
        [HttpPost]
        public ActionResult Create(Tabela tabela)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(tabela);
                    transaction.Commit();
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Tabela/Edit/5
        public ActionResult Edit(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }
                var tabela = session.Get<Tabela>(id);
                ViewBag.druzyny = session.Query<Druzyna>().ToList();
                return View(tabela);
            }
        }

        // POST: Tabela/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Tabela tabela)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var tabelaUpdate = session.Get<Tabela>(id);

                    tabelaUpdate.Druzyna = tabela.Druzyna;
                    tabelaUpdate.Punkty = tabela.Punkty;
                    tabelaUpdate.MeczeRozegrane = tabela.MeczeRozegrane;
                    tabelaUpdate.MeczeWygrane = tabela.MeczeWygrane;
                    tabelaUpdate.MeczePrzegrane = tabela.MeczePrzegrane;
                    tabelaUpdate.MeczeZremisowane = tabela.MeczeZremisowane;
                    tabelaUpdate.BramkiStrzelone = tabela.BramkiStrzelone;
                    tabelaUpdate.BramkiStracone = tabela.BramkiStracone;




                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(tabelaUpdate);
                        transaction.Commit();
                    }

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tabela/Delete/5
        public ActionResult Delete(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }
                var tabela = session.Get<Tabela>(id);
                NHibernateUtil.Initialize(tabela.Druzyna);
                return View(tabela);
            }
        }

        // POST: Tabela/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Tabela tabela)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        ViewBag.druzyny = session.Query<Druzyna>().ToList();
                        session.Delete(tabela);
                        transaction.Commit();
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EksportPDF(string model)
        {
            var data = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
            return new Rotativa.ActionAsPdf("Index") { FileName = "Ranking" + data + ".pdf" }; ;
            
        }
    }
}
