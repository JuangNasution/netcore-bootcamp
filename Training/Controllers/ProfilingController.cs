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
    public class ProfilingController : Controller
    {
        private readonly MyContext myContext;

        public ProfilingController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            var profile = myContext.Profilings.Include(m => m.Account).Include(e => e.Education);
            return View(profile);
        }

        public IActionResult Create()
        {
            var educationList = (from education in myContext.Educations.ToList()
                                  select new SelectListItem()
                                  {
                                      Text = education.Degree,
                                      Value = education.Id.ToString(),
                                  }).ToList();
            var NIKList = (from account in myContext.Accounts.ToList()
                                 select new SelectListItem()
                                 {
                                     Text = account.NIK,
                                     Value = account.NIK.ToString(),
                                 }).ToList();
            ViewBag.Education_Id = educationList;
            ViewBag.NIK = NIKList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Profiling profile)
        {
            var get = myContext.Profilings.Find(profile.NIK);
            if (!get.Equals(null))
            {
                return RedirectToAction("Index");
            }
            myContext.Profilings.Add(profile);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index");
            return View();
        }

        public IActionResult Edit(string id)
        {
            if (id.Equals(null))
            {
                return View();
            }
            var educationList = (from education in myContext.Educations.ToList()
                                 select new SelectListItem()
                                 {
                                     Text = education.Degree,
                                     Value = education.Id.ToString(),
                                 }).ToList();
            ViewBag.Education_Id = educationList;
            var result = myContext.Profilings.Find(id);
            if (result == null)
            {
                return View();
            }
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string? id, Profiling profile)
        {
            if (id.Equals(null))
            {
                return View();
            }
            var get = myContext.Profilings.Find(id);
            if (get != null)
            {
                get.NIK = profile.NIK;
                get.Education = myContext.Educations.Find(profile.Education_Id);
                myContext.Entry(get).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return RedirectToAction("Index");
                return View(result);
            }
            return View();
        }

        public IActionResult Delete(string? id)
        {
            var get = myContext.Profilings.Find(id);
            if (get != null)
            {
                myContext.Profilings.Remove(get);
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Json(result);
            }

            return Json(0);
        }
    }
}
