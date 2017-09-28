using Foosball.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Foosball.Controllers
{
    public class ZawodnikController : Controller
    {
        // GET: Zawodnik
        public ActionResult Index()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }
                var zawodnicy = session.Query<Zawodnik>().ToList();
                return View(zawodnicy);
            }
                
        }

        // GET: Zawodnik/Details/5
        public ActionResult Details(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }
                var zawodnik = session.Get<Zawodnik>(id);
                return View(zawodnik);
            }
        }

        // GET: Zawodnik/Create
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("../Access/Login");
            }
            return View();
        }

        // POST: Zawodnik/Create
        [HttpPost]
        public ActionResult Create(Zawodnik zawodnik)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(zawodnik);
                        transaction.Commit();
                    }
                }


                    return RedirectToAction("Index");
            }
            catch(Exception exception)
            {
                return View();
            }
        }

        // GET: Zawodnik/Edit/5
        public ActionResult Edit(int id)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }
                var zawodnik = session.Get<Zawodnik>(id);
                return View(zawodnik);
            }
                
        }

        // POST: Zawodnik/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Zawodnik zawodnik)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var zawodnikUpdate = session.Get<Zawodnik>(id);

                    zawodnikUpdate.Imie = zawodnik.Imie;
                    zawodnikUpdate.Nazwisko = zawodnik.Nazwisko;
                 

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(zawodnikUpdate);
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

        // GET: Zawodnik/Delete/5
        public ActionResult Delete(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }
                var zawodnik = session.Get<Zawodnik>(id);
                return View(zawodnik);
            }
        }

        // POST: Zawodnik/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Zawodnik zawodnik)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(zawodnik);
                        transaction.Commit();
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return View();
            }
        }
    }
}
