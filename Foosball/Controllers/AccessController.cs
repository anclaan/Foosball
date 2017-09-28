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
    public class AccessController : Controller
    {
        // GET: Uzytkownik
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]  
        [ValidateAntiForgeryToken]  
        public ActionResult Login(Uzytkownik objUser)
        {  
            if (ModelState.IsValid)   
            {  
                using(ISession session = NHibernateHelper.OpenSession())  
                {
                    var obj = session.Query<Uzytkownik>().Where(x => x.Login.Equals(objUser.Login) && x.Password.Equals(objUser.Password)).FirstOrDefault();
                    var id = obj.Id;
                    
                        //.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();  
                    if (obj != null)  
                    {   
                        if(obj.Rola == "1" )
                        {
                            var zawodnik = session.Query<Zawodnik>().Where(x => x.IdUzytkownik.Id == obj.Id).FirstOrDefault();
                            Session["ZawodnikId"] = zawodnik.IdUzytkownik.Id;
                        }
                        Session["UserID"] = obj.Id.ToString();  
                        Session["UserName"] = obj.Login.ToString();
                        Session["UserRole"] = obj.Rola.ToString();
                        
                        return RedirectToAction("../Tabela/Index");  
                    }
                    else
                    {
                        ModelState.AddModelError("", "Niepoprawny login lub haslo");
                    }
                }  
            }  
            return View(objUser);  
        }  
  
        public ActionResult UserDashBoard()
        {  
            if (Session["UserID"] != null)  
            {  
                return View();  
            } else  
            {  
                return RedirectToAction("Login");  
            }  
        }  


        // GET: Uzytkownik/Details/5
        public ActionResult Logout()
        {


            Session.Abandon();


                return RedirectToAction("../Tabela/Index");
            
        }

        // GET: Uzytkownik/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Uzytkownik/Create
        [HttpPost]
        public ActionResult Create(Uzytkownik uzytkownik)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        uzytkownik.Rola = "1";
                        session.Save(uzytkownik);
                        transaction.Commit();
                    }
                }


                return RedirectToAction("Login");
            }
            catch (Exception exception)
            {
                return View();
            }
        }

        // GET: Uzytkownik/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Uzytkownik/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Uzytkownik/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Uzytkownik/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
