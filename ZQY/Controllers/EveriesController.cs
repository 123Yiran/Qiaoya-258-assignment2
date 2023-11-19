using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZQY.Data;
using ZQY.Models;
using ZQY.ViewModels;
using PagedList;

namespace ZQY.Controllers
{
    public class EveriesController : Controller
    {
        private ZQYContext db = new ZQYContext();

        // GET: Everies
        public ActionResult Index(string season,string search,string sortBy,int ?page)
        {
            /****************/
           EveryIndexViewModel viewModel = new EveryIndexViewModel();
            var everies = db.Everies.Include(p => p.Season);
            
            if (!String.IsNullOrEmpty(search))
            {
                everies = everies.Where(p => p.Name.Contains(search) ||
                p.Description.Contains(search) ||
                   p.Length.Contains(search)||
                p.Season.Name.Contains(search));
           
                //ViewBag.Search = search;
                viewModel.Search = search;
            }
            viewModel.SeasWithCount = from matchingEveries in everies
                                      where matchingEveries.SeasonID != null
                                      group matchingEveries by
                                      matchingEveries.Season.Name into
                                      seaGroup
                                      select new SeasonWithCount()
                                      {
                                          SeasonName = seaGroup.Key,
                                          EveryCount = seaGroup.Count()
                                      };
            //var categories = products.OrderBy(p => p.Category.Name).Select(p => p.Season.Name).Distinct();
            if (!String.IsNullOrEmpty(season))
            {
                everies = everies.Where(p => p.Season.Name == season);
                viewModel.Season=season;
            }

            switch (sortBy)
            {
                case "description_lowest":
                    everies = everies.OrderBy(p => p.Description);
                    break;
                case "description_highest":
                    everies = everies.OrderByDescending(p => p.Description);
                    break;
                default:
                    everies = everies.OrderBy(p => p.Name);
                    break;
            }

            everies = everies.OrderBy(p => p.Name); 
            const int PageItems = 3;
            int currentPage = (page ?? 1);
           

            viewModel.Everies = everies.ToPagedList(currentPage, PageItems);
            viewModel.SortBy = sortBy;
            viewModel.Sorts = new Dictionary<string, string>
 {
 {"Description low to high", "description_lowest" },
 {"Description high to low", "description_highest" }
 };

            return View(viewModel);




            //ViewBag.Category = new SelectList(categories); 
            // viewModel.Everies = everies;
            // return View(products.ToList());
        }

        /*
                var seasons = everies.OrderBy(p => p.Season.Name).Select(p => p.Season.Name).Distinct();
                    if (!String.IsNullOrEmpty(season))
                    {
                        everies = everies.Where(p => p.Season.Name == season);
                    }
                    ViewBag.Season = new SelectList(seasons);
                    return View(everies.ToList());

                }*/

        // GET: Everies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Every every = db.Everies.Find(id);
            if (every == null)  
            {
                return HttpNotFound();
            }
            return View(every);
        }

        // GET: Everies/Create
        public ActionResult Create() 
        {
            ViewBag.SeasonID = new SelectList(db.Seasons, "ID", "Name");
            return View();
        }

        // POST: Everies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Length,Description,SeasonID")] Every every)
        {
            if (ModelState.IsValid)
            {
                db.Everies.Add(every);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SeasonID = new SelectList(db.Seasons, "ID", "Name", every.SeasonID);
            return View(every);
        }

        // GET: Everies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Every every = db.Everies.Find(id);
            if (every == null)
            {
                return HttpNotFound();
            }
            ViewBag.SeasonID = new SelectList(db.Seasons, "ID", "Name", every.SeasonID);
            return View(every);
        }

        // POST: Everies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Length,Description,SeasonID")] Every every)
        {
            if (ModelState.IsValid)
            {
                db.Entry(every).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SeasonID = new SelectList(db.Seasons, "ID", "Name", every.SeasonID);
            return View(every);
        }

        // GET: Everies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Every every = db.Everies.Find(id);
            if (every == null)
            {
                return HttpNotFound();
            }
            return View(every);
        }

        // POST: Everies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Every every = db.Everies.Find(id);
            db.Everies.Remove(every);
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
