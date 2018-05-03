/*
* Description: This controller contains the Index, Create, Delete, Edit and Details Actions for Locations.
* Author: R.M.
* Due date: 05/05/2018
*/

using AutoMapper;
using ConsumerPanelTestSystemApplication.Models;
using ConsumerPanelTestSystemApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ConsumerPanelTestSystemApplication.Controllers
{
   
    public class LocationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        /// <summary>  
        /// The Index action is utilized in order to generate a list of Locations. 
        /// </summary>
        /// <returns>Index View</returns>
        // GET: Location
        public ActionResult Index()
        {
            var locations = db.Locations.ToList();

            var model = new List<LocationViewModel>();
            foreach (var item in locations)
            {
                model.Add(new LocationViewModel
                {
                    Id = item.LocationID,
                    City = item.City,
                    Region = item.Region
                });
            }
            return View(model);
        }


        /// <summary>  
        /// The Details action displays the details of a specific location.
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <returns>Details View</returns>
        // GET: Location/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }

            var model = new LocationViewModel
            {
                Id = location.LocationID,
                City = location.City,
                Region = location.Region
            };

            return View(model);
        }

        /// <summary>  
        /// The Create action allows for the addition of a new Location. 
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <returns>Create View</returns>
        // GET: Location/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>  
        /// The Create action allows for the addition of a new Location. 
        /// </summary>
        /// <param name="model">LocationViewModel as a parameter</param>
        /// <returns>Create View</returns>
        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create the location from the model
                var location = new Location
                {
                    City = model.City,
                    Region = model.Region
                };
                
                // Save the created course to the database
                db.Locations.Add(location);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        /// <summary>  
        /// The Edit action permits updating a Location's details.
        /// </summary>
        /// <param name="id">Location Id as a parameter</param>
        /// <returns>Edit View</returns>
        // GET: Location/Edit/5
        public ActionResult Edit(int? id)
        {           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }

            LocationViewModel model = new LocationViewModel
            {
                Id = location.LocationID,
                City = location.City,
                Region = location.Region
            };

            return View(model);
        }

        /// <summary>  
        /// The Edit action permits updating a Location's details.
        /// </summary>
        /// <param name="id">Location Id as a parameter</param>
        /// <param name="model">LocationViewModel as a parameter</param>
        /// <returns>Edit View</returns>
        // POST: Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var location = db.Locations.Find(id);
                if (location == null)
                {
                    return HttpNotFound();
                }

                // Edit the location info
                location.City = model.City;
                location.Region = model.Region;

                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");                
            }

            return View();
        }

        /// <summary>  
        /// The Delete action removes a specific Location from the database. 
        /// </summary>
        /// <param name="id">Location Id as a parameter</param>
        /// <returns>Delete View</returns>
        // GET: Location/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            var model = new LocationViewModel
            {
                Id = location.LocationID,
                City = location.City,
                Region = location.Region
            };

            return View(model);
        }

        /// <summary>  
        /// The Delete action removes a specific Location from the database. 
        /// </summary>
        /// <param name="id">Location Id as a parameter</param>
        /// <returns>Delete View</returns>
        // POST: Location/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
