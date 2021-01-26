using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training.Context;
using Training.Models;

namespace Training.Controllers
{
    public class UniversityController : Controller
    {
        private readonly MyContext myContext;

        public UniversityController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            var universities = myContext.Universities.ToList();
            return View(universities);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(University university)
        {
            myContext.Universities.Add(university);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index");
            return View();
        }

        public IActionResult Edit(int id)
        {
            if (id.Equals(null))
            {
                return View();
            }
            var result = myContext.Universities.Find(id);
            //var result = myContext.Universities.Where(x => x.Id == id).SingleOrDefault();
            if (result == null)
            {
                return View();
            }
            return View(result);
        }
        [HttpPost]
        public IActionResult Edit(int? id, University university)
        {
            if (id.Equals(null))
            {
                return View();
            }
            var get = myContext.Universities.Find(id);
            if (get != null)
            {
                get.Name = university.Name;
                myContext.Entry(get).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Json(result);
                return View(result);
            }
            return Json(0);
        }


        public IActionResult Delete(int? id)
        {
            var get = myContext.Universities.Find(id);
            if (get != null)
            {
                myContext.Universities.Remove(get);
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Json(result);
            }
            
            return Json(0);
        }

        public IActionResult Find(int id)
        {
            var univ = myContext.Universities.Find(id);
            return new JsonResult(univ);
        }
    }
}
