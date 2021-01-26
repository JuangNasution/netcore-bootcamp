using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Training.Context;
using Training.Models;

namespace Training.Controllers
{
    public class EducationController : Controller
    {
        private readonly MyContext myContext;
        public EducationController(MyContext myContext)
        {
            this.myContext = myContext;   
        }
        public IActionResult Index()
        {

            var educations = myContext.Educations.Include(m => m.University);
            return View(educations);
        }
        public IActionResult Create()
        {
            var universityList = (from university in myContext.Universities.ToList()
                                  select new SelectListItem()
                                  {
                                      Text = university.Name,
                                      Value = university.Id.ToString(),
                                  }).ToList();
            ViewBag.University_Id = universityList;
            return View();
        }
        [HttpPost]
        public IActionResult Create (Education education)
        {
            myContext.Educations.Add(education);
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
            var universityList = (from university in myContext.Universities.ToList()
                                  select new SelectListItem()
                                  {
                                      Text = university.Name,
                                      Value = university.Id.ToString(),
                                  }).ToList();
            ViewBag.University_Id = universityList;
            var result = myContext.Educations.Find(id);
            if (result == null)
            {
                return View();
            }
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, Education education)
        {
            if (id.Equals(null))
            {
                return View();
            }
            var get = myContext.Educations.Find(id);
            if (get != null)
            {
                get.Degree = education.Degree;
                get.GPA = education.GPA;
                get.University = myContext.Universities.Find(education.University_Id);
                myContext.Entry(get).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return RedirectToAction("Index");
                return View(result);
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            var get = myContext.Educations.Find(id);
            if (get != null)
            {
                myContext.Educations.Remove(get);
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Json(result);
            }

            return Json(0);
        }
    }
}
