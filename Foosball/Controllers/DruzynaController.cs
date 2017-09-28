using Foosball.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Foosball.Controllers
{
    public class DruzynaController : Controller
    {
        // GET: Druzyna
        public ActionResult Index()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var druzyny1 = session.Query<Druzyna>().Fetch(x=>x.Zawodnik1).Fetch(x => x.Zawodnik2).ToList();

                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }

                return View(druzyny1);
            }
        }

        // GET: Druzyna/Details/5
        public ActionResult Details(int id)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }
                var druzyna1 = session.Query<Tabela>().Where(x => x.Druzyna.Id == id).FirstOrDefault();
                var druzyna = session.Get<Druzyna>(id);
                NHibernateUtil.Initialize(druzyna.Zawodnik1);
                NHibernateUtil.Initialize(druzyna.Zawodnik2);
                ViewBag.druzyna = druzyna1;
                return View(druzyna);
            }
        }

        // GET: Druzyna/Create
        public ActionResult Create()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ViewBag.zawodnicy = session.Query<Zawodnik>().ToList();
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }
                return View();
            }
        }
        
        // POST: Druzyna/Create
        [HttpPost]
        public ActionResult Create(Druzyna druzyna)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {   if (druzyna.Zawodnik1.Id != druzyna.Zawodnik2.Id)
                    {
                        
                        session.Save(druzyna);
                        transaction.Commit();
                        
                    }
                    else
                    {
                        ModelState.AddModelError("", "Nie mozna wybrac dwoch tych samych zawodnikow");
                        ViewBag.zawodnicy = session.Query<Zawodnik>().ToList();
                        return View(druzyna);
                    }
                }
            }

            using (ISession session1 = NHibernateHelper.OpenSession())
            {
                Tabela tabela = new Tabela();
                tabela.Druzyna = druzyna;
                tabela.Punkty = 0;
                tabela.MeczeRozegrane = 0;
                tabela.MeczeWygrane = 0;
                tabela.MeczePrzegrane = 0;
                tabela.BramkiStrzelone = 0;
                tabela.BramkiStracone = 0;
                using (ITransaction transaction = session1.BeginTransaction())
                {

                    session1.Save(tabela);
                    transaction.Commit();
                }
            }
                return RedirectToAction("Index");
             
            
        }

        // GET: Druzyna/Edit/5
        public ActionResult Edit(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }
                var druzyna = session.Get<Druzyna>(id);
                ViewBag.zawodnicy = session.Query<Zawodnik>().ToList();
                return View(druzyna);
            }
        }

        // POST: Druzyna/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Druzyna druzyna, HttpPostedFileBase files)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var druzynaUpdate = session.Get<Druzyna>(id);

                    druzynaUpdate.Nazwa = druzyna.Nazwa;
                    druzynaUpdate.Zawodnik1 = druzyna.Zawodnik1;
                    druzynaUpdate.Zawodnik2 = druzyna.Zawodnik2;

                    if (files != null)
                    {
                        string path = Server.MapPath("~/images/");
                        files.SaveAs(path + Path.GetFileName(files.FileName));
                        ViewBag.Message = "Awatar został dodany!";
                        druzynaUpdate.Logo = files.FileName;
                    }
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(druzynaUpdate);
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

        // GET: Druzyna/Delete/5
        public ActionResult Delete(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }
                var druzyna = session.Get<Druzyna>(id);
                return View(druzyna);
            }
        }

        // POST: Druzyna/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Druzyna druzyna)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(druzyna);
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
    }
}
