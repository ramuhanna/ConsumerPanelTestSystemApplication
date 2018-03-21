/*
* Description: This controller contains the Index, Create and Details Actions for CPTRequests.
* Author: R.M.
* Due date: 21/03/2018
*/

using ConsumerPanelTestSystemApplication.Models;
using ConsumerPanelTestSystemApplication.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ConsumerPanelTestSystemApplication.Controllers
{
    public class CPTRequestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public CPTRequestController()
        {
        }

        // This action generates a list of CPT requests.
        // GET: CPTRequest
        public ActionResult Index()
        {
            var requests = db.CPTRequests.ToList();
            var model = new List<CPTRequestViewModel>();

            foreach (var item in requests)
            {
                model.Add(new CPTRequestViewModel
                {
                    Id = item.RequestID,
                    RequestTitle = item.RequestTitle,
                    RequestDate = item.RequestDate,
                    REmployeeId = item.REmployeeId,
                    ProductDivision = item.ProductDivision,
                    RequestStatus = item.RequestStatus
                });
            }
            return View(model);
        }

        // This action displays the details of a specific CPT request.
        // GET: CPTRequest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CPTRequest request = db.CPTRequests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }

            var model = new CPTRequestViewModel
            {
                Id = request.RequestID,
                RequestTitle = request.RequestTitle,
                ProductDivision = request.ProductDivision,
                Justification = request.Justification,
                LocationId = request.LocationId,
                RequestDate = request.RequestDate??DateTime.Now.Date,
                REmployeeId = request.REmployeeId,
                RequestStatus = request.RequestStatus
            };

            return View(model);
        }

        // GET: CPTRequest/Create
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View();
        }

        // This action allows for the creation of a new CPT request.
        // POST: CPTRequest/Create
        [HttpPost]
        public ActionResult Create(CPTRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create the request from the model.
                var request = new CPTRequest
                {
                    RequestTitle = model.RequestTitle,
                    RequestDate = DateTime.Now.Date,
                    RequestStatus = model.RequestStatus,
                    ProductDivision = model.ProductDivision,
                    Justification = model.Justification,
                    MDecisionId = model.MDecisionId,
                    MReviewRequest = model.MReviewRequest,
                    BEmployeeId = model.BEmployeeId,
                    BReviewRequest = model.BReviewRequest,
                    BDecisionId = model.BDecisionId,
                    REmployeeId = model.REmployeeId,
                    BDecisionMade = model.BDecisionMade,
                    BDecisionDate = model.BDecisionDate,
                    BReview = model.BReview,
                    MDecision = model.MDecision,
                    MDecisionDate = model.MDecisionDate,
                    MReview = model.MReview,
                    LocationId = model.LocationId,
                };

                // Save the created request to the database.
                db.CPTRequests.Add(request);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View();

        }

        //// GET: CPTRequest/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: CPTRequest/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: CPTRequest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // This action removes a CPT Request from the database.
        // POST: CPTRequest/Delete/5
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
