using Foosball.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Foosball.Controllers
{
    public class MeczController : Controller
    {
        // GET: Mecz
        public ActionResult Index(int? page)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                //if (Session["UserID"] == null)
                //{
                //    return RedirectToAction("../Access/Login");
                //}
                var mecze = session.Query<Mecz>().Fetch(x => x.Druzyna1).Fetch(x => x.Druzyna2).ToList().OrderByDescending(x => x.Data);
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(mecze.ToPagedList(pageNumber, pageSize));

                
            }
        }

        // GET: Mecz/Details/5
        public ActionResult Details(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }
                var mecz = session.Get<Mecz>(id);
                NHibernateUtil.Initialize(mecz.Druzyna1);
                NHibernateUtil.Initialize(mecz.Druzyna2);
                return View(mecz);
            }
        }

        // GET: Mecz/Create
        public ActionResult Create()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }
                ViewBag.druzyny = session.Query<Druzyna>().ToList();

                return View();
            }
        }

        // POST: Mecz/Create
        [HttpPost]
        public ActionResult Create(Mecz mecz)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var druzyna1 = mecz.Druzyna1.Id;
                    var druzyna2 = mecz.Druzyna2.Id;
                    var druzynaPierwsza = session.Query<Tabela>().Where(x => x.Druzyna.Id == druzyna1).Fetch(x => x.Druzyna).ToList().First();
                    var druzynaDruga = session.Query<Tabela>().Where(x => x.Druzyna.Id == druzyna2).Fetch(x => x.Druzyna).ToList().First();

                    var druzynaPierwszaUpdate = session.Query<Tabela>().Where(x => x.Druzyna.Id == druzyna1).Fetch(x => x.Druzyna).ToList().First();
                    var druzynaDrugaUpdate = session.Query<Tabela>().Where(x => x.Druzyna.Id == druzyna2).Fetch(x => x.Druzyna).ToList().First();

                    if (druzynaPierwsza != druzynaDruga)
                    {

                        session.Save(mecz);
                        transaction.Commit();

                        ViewBag.bramki1 = mecz.Druzyna1Bramki;
                        ViewBag.bramki2 = mecz.Druzyna2Bramki;

                  //  var druzynaPierwsza = session.Query<Tabela>().Fetch(x=>x.Druzyna.Id == druzyna1).ToArray();
                  //  var druzynaDruga = session.Query<Tabela>().Fetch(x => x.Druzyna.Id == druzyna2).ToArray();

                  //  var druzynaPierwszaUpdate = session.Query<Tabela>().Fetch(x => x.Druzyna.Id == druzyna1).ToList();
                  //  var druzynaDrugaUpdate = session.Query<Tabela>().Fetch(x => x.Druzyna.Id == druzyna2).ToList();

                    

                    //var druzyna1Punkty = session.Get<Tabela>(druzyna1);
                    //var druzyna1Rozegrane = session.Get<Tabela>(druzyna1);
                    //var druzyna1Wygrane = session.Get<Tabela>(druzyna1);
                    //var druzyna1Przegrane = session.Get<Tabela>(druzyna1);
                    //var druzyna1Zremisowane= session.Get<Tabela>(druzyna1);
                    //var druzyna1Strzelone = session.Get<Tabela>(druzyna1);
                    //var druzyna1Stracone = session.Get<Tabela>(druzyna1);

                    //var druzyna2Punkty = session.Get<Tabela>(druzyna2);
                    //var druzyna2Rozegrane = session.Get<Tabela>(druzyna2);
                    //var druzyna2Wygrane = session.Get<Tabela>(druzyna2);
                    //var druzyna2Przegrane = session.Get<Tabela>(druzyna2);
                    //var druzyna2Zremisowane = session.Get<Tabela>(druzyna2);
                    //var druzyna2Strzelone = session.Get<Tabela>(druzyna2);
                    //var druzyna2Stracone = session.Get<Tabela>(druzyna2);
                  
                        if (mecz.Druzyna1Bramki > mecz.Druzyna2Bramki)
                        {
                            druzynaPierwsza.Punkty = druzynaPierwszaUpdate.Punkty + 3;
                            druzynaPierwsza.MeczeRozegrane = druzynaPierwszaUpdate.MeczeRozegrane + 1;
                            druzynaPierwsza.MeczeWygrane = druzynaPierwszaUpdate.MeczeWygrane + 1;
                            druzynaPierwsza.MeczePrzegrane = druzynaPierwszaUpdate.MeczePrzegrane + 0;
                            druzynaPierwsza.MeczeZremisowane = druzynaPierwszaUpdate.MeczeZremisowane + 0;
                            druzynaPierwsza.BramkiStrzelone = druzynaPierwszaUpdate.BramkiStrzelone + mecz.Druzyna1Bramki;
                            druzynaPierwsza.BramkiStracone = druzynaPierwszaUpdate.BramkiStracone + mecz.Druzyna2Bramki;

                            druzynaDruga.Punkty = druzynaDrugaUpdate.Punkty + 0;
                            druzynaDruga.MeczeRozegrane = druzynaDrugaUpdate.MeczeRozegrane + 1;
                            druzynaDruga.MeczeWygrane = +druzynaDrugaUpdate.MeczeWygrane + 0;
                            druzynaDruga.MeczePrzegrane = druzynaDrugaUpdate.MeczePrzegrane + 1;
                            druzynaDruga.MeczeZremisowane = druzynaDrugaUpdate.MeczeZremisowane + 0;
                            druzynaDruga.BramkiStrzelone = druzynaDrugaUpdate.BramkiStrzelone + mecz.Druzyna2Bramki;
                            druzynaDruga.BramkiStracone = druzynaDrugaUpdate.BramkiStracone + mecz.Druzyna1Bramki;

                            using (ITransaction transaction1 = session.BeginTransaction())
                            {

                                session.Save(druzynaPierwsza);
                                session.Save(druzynaDruga);
                                transaction1.Commit();
                            }
                        }
                        else if (mecz.Druzyna1Bramki < mecz.Druzyna2Bramki)
                        {
                            druzynaPierwsza.Punkty = druzynaPierwszaUpdate.Punkty + 0;
                            druzynaPierwsza.MeczeRozegrane = druzynaPierwszaUpdate.MeczeRozegrane + 1;
                            druzynaPierwsza.MeczeWygrane = druzynaPierwszaUpdate.MeczeWygrane + 0;
                            druzynaPierwsza.MeczePrzegrane = druzynaPierwszaUpdate.MeczePrzegrane + 1;
                            druzynaPierwsza.MeczeZremisowane = druzynaPierwszaUpdate.MeczeZremisowane + 0;
                            druzynaPierwsza.BramkiStrzelone = druzynaPierwszaUpdate.BramkiStrzelone + mecz.Druzyna1Bramki;
                            druzynaPierwsza.BramkiStracone = druzynaPierwszaUpdate.BramkiStracone + mecz.Druzyna2Bramki;

                            druzynaDruga.Punkty = druzynaDrugaUpdate.Punkty + 3;
                            druzynaDruga.MeczeRozegrane = druzynaDrugaUpdate.MeczeRozegrane + 1;
                            druzynaDruga.MeczeWygrane = +druzynaDrugaUpdate.MeczeWygrane + 1;
                            druzynaDruga.MeczePrzegrane = druzynaDrugaUpdate.MeczePrzegrane + 0;
                            druzynaDruga.MeczeZremisowane = druzynaDrugaUpdate.MeczeZremisowane + 0;
                            druzynaDruga.BramkiStrzelone = druzynaDrugaUpdate.BramkiStrzelone + mecz.Druzyna2Bramki;
                            druzynaDruga.BramkiStracone = druzynaDrugaUpdate.BramkiStracone + mecz.Druzyna1Bramki;

                            using (ITransaction transaction1 = session.BeginTransaction())
                            {

                                session.Save(druzynaPierwsza);
                                session.Save(druzynaDruga);
                                transaction1.Commit();
                            }
                        }
                        else if (mecz.Druzyna1Bramki == mecz.Druzyna2Bramki)
                        {
                            druzynaPierwsza.Punkty = druzynaPierwszaUpdate.Punkty + 1;
                            druzynaPierwsza.MeczeRozegrane = druzynaPierwszaUpdate.MeczeRozegrane + 1;
                            druzynaPierwsza.MeczeWygrane = druzynaPierwszaUpdate.MeczeWygrane + 0;
                            druzynaPierwsza.MeczePrzegrane = druzynaPierwszaUpdate.MeczePrzegrane + 0;
                            druzynaPierwsza.MeczeZremisowane = druzynaPierwszaUpdate.MeczeZremisowane + 1;
                            druzynaPierwsza.BramkiStrzelone = druzynaPierwszaUpdate.BramkiStrzelone + mecz.Druzyna1Bramki;
                            druzynaPierwsza.BramkiStracone = druzynaPierwszaUpdate.BramkiStracone + mecz.Druzyna2Bramki;

                            druzynaDruga.Punkty = druzynaDrugaUpdate.Punkty + 1;
                            druzynaDruga.MeczeRozegrane = druzynaDrugaUpdate.MeczeRozegrane + 1;
                            druzynaDruga.MeczeWygrane = +druzynaDrugaUpdate.MeczeWygrane + 0;
                            druzynaDruga.MeczePrzegrane = druzynaDrugaUpdate.MeczePrzegrane + 0;
                            druzynaDruga.MeczeZremisowane = druzynaDrugaUpdate.MeczeZremisowane + 1;
                            druzynaDruga.BramkiStrzelone = druzynaDrugaUpdate.BramkiStrzelone + mecz.Druzyna2Bramki;
                            druzynaDruga.BramkiStracone = druzynaDrugaUpdate.BramkiStracone + mecz.Druzyna1Bramki;

                            using (ITransaction transaction1 = session.BeginTransaction())
                            {

                                session.Save(druzynaPierwsza);
                                session.Save(druzynaDruga);
                                transaction1.Commit();
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Wybrano dwie druzyny o takiej samej nazwie");
                        ViewBag.druzyny = session.Query<Druzyna>().ToList();
                        return View(mecz);
                    }
                }


                }
            
            return RedirectToAction("Index","Tabela");
        }

        // GET: Mecz/Edit/5
        public ActionResult Edit(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }
                var mecz = session.Get<Mecz>(id);
                ViewBag.druzyny = session.Query<Druzyna>().ToList();
                return View(mecz);
            }
        }

        // POST: Mecz/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Mecz mecz)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var meczUpdate = session.Get<Mecz>(id);

                    meczUpdate.Data = mecz.Data;
                    meczUpdate.Druzyna1Bramki = mecz.Druzyna1Bramki;
                    meczUpdate.Druzyna2Bramki = mecz.Druzyna2Bramki;
                    meczUpdate.Druzyna1 = mecz.Druzyna1;
                    meczUpdate.Druzyna2 = mecz.Druzyna2;
                   
               


                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(meczUpdate);
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

        // GET: Mecz/Delete/5
        public ActionResult Delete(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("../Access/Login");
                }
                var mecz = session.Get<Mecz>(id);
                return View(mecz);
            }
        }

        // POST: Mecz/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Mecz mecz)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(mecz);
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
