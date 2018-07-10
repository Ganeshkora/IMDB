using IMDB.com.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.com.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetMovieDetails()
        {
            using (IMDBEntities dc = new IMDBEntities())
            {
                var movies = dc.Movies.OrderBy(a => a.MovieName).ToList();
                return Json(new { data = movies }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Save(int id)
        {
            using (IMDBEntities dc = new IMDBEntities())
            {
                var v = dc.Movies.Where(a => a.MovieID == id).FirstOrDefault();
                return View(v);
            }
        }
        [HttpPost]
        public ActionResult Save(Movy mov)
        {
            bool status = false;
            if(ModelState.IsValid)
            {
                using (IMDBEntities dc = new IMDBEntities())
                {
                    if(mov.MovieID > 0)
                    {
                        //Edit
                        var v = dc.Movies.Where(a => a.MovieID == mov.MovieID).FirstOrDefault();
                        if(v!=null)
                        {
                            v.MovieName = mov.MovieName;
                            v.Cast = mov.Cast;
                            v.Director = mov.Director;
                            v.Producer = mov.Producer;
                            v.RDate = mov.RDate;
                        }
                    }
                    else
                    {
                        //Save
                        dc.Movies.Add(mov);
                    }
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (IMDBEntities dc = new IMDBEntities())
            {
                var v = dc.Movies.Where(a => a.MovieID == id).FirstOrDefault();
                if(v!=null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteMovie(int id)
        {
            bool status = false;
            using (IMDBEntities dc = new IMDBEntities())
            {
                var v = dc.Movies.Where(a => a.MovieID == id).FirstOrDefault();
                if(v!=null)
                {
                    dc.Movies.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}