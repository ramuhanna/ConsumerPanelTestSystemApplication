/*
* Description: This controller contains the Index, Create and Details Actions for CPTRequests.
* Author: R.M.
* Due date: 21/03/2018
*/

using ConsumerPanelTestSystemApplication.Models;
using ConsumerPanelTestSystemApplication.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        /// <summary>  
        /// The MarketingDirectorIndex action is utilized in order to generate a list of CPT Requests to be reviewed by the Marketing Director. 
        /// </summary>
        /// <returns>CPTRequest, Marketing Director Index View</returns>

        // GET: CPTRequest
        public ActionResult MarketingDirectorIndex()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Marketing Director"))
            {
                 var requests = db.CPTRequests.Where(b => b.RequestStatus != RequestStatus.BMRequestApproval).ToList();
                var model = new List<CPTRequestViewModel>();

                foreach (var item in requests)
                {
                    model.Add(new CPTRequestViewModel
                    {
                        Id = item.RequestID,
                        RequestTitle = item.RequestTitle,
                        RequestDate = item.RequestDate,
                        //REmployeeId = item.REmployeeId,
                        //SubmittedById = item.SubmittedById,
                        ProductDivision = item.ProductDivision,
                        RequestStatus = item.RequestStatus,
                        SubmittedByName = item.Employee.FullName                                       
                    });
                }
                return View(model);
            }
            else
            {
                return View("Error");
            }
                
        }

        /// <summary>  
        /// The SubmittedRequestsIndex action is utilized in order to generate a list of the CPT Requests that were submitted by the logged in user. 
        /// </summary>
        /// <returns>CPTRequest, Submitted Requests Index View</returns>

        // GET: CPTRequest
        public ActionResult SubmittedRequestsIndex()
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Brand Manager") || User.IsInRole("Requester")))
            {
                var loggeduserid = User.Identity.GetUserId<int>();
                var requests = db.CPTRequests.Where(d => d.SubmittedById == loggeduserid).ToList();
                var model = new List<CPTRequestViewModel>();

                foreach (var item in requests)
                {
                    model.Add(new CPTRequestViewModel
                    {
                        Id = item.RequestID,
                        RequestTitle = item.RequestTitle,
                        RequestDate = item.RequestDate,
                        ProductDivision = item.ProductDivision,
                        RequestStatus = item.RequestStatus,
                    });
                }
                return View(model);
            }

            else
            {
                return View("Error");
            }
            return View();
        }

        /// <summary>  
        /// The BrandManagerReviewIndex action is utilized in order to generate a list of the CPT Requests to be reviewed by the Brand Manager. 
        /// </summary>
        /// <returns>CPTRequest, Brand Manager Review Index View</returns>
        
        // GET: CPTRequest
        public ActionResult BrandManagerReviewIndex()
        {
            var isBrandManager = User.IsInRole("Brand Manager");
            var user = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id;
            var requests = db.CPTRequests.Where(b => b.BReviewRequest == user).ToList();

            var model = new List<CPTRequestViewModel>();

                foreach (var item in requests)
                {

                    model.Add(new CPTRequestViewModel
                    {
                        Id = item.RequestID,
                        RequestTitle = item.RequestTitle,
                        RequestDate = item.RequestDate,
                        ProductDivision = item.ProductDivision,
                        RequestStatus = item.RequestStatus,

                    });
                }

                return View(model);
                return View("Error");
               
        }


        /// <summary>  
        /// The Details action is utilized in order to view the details of a specific CPT Request. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CPTRequest, Details view</returns>

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
                City = request.Location.City,
                RequestDate = request.RequestDate??DateTime.Now.Date,
                //REmployeeId = request.REmployeeId,
                //SubmittedBy = request.SubmittedBy,
                RequestStatus = request.RequestStatus,
                BReviewRequest = request.BReviewRequest,  
                SubmittedByName = request.Employee.FullName                         
            };

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View(model);
        }

        /// <summary>  
        /// The Create action allows for the creation of a new CPT request. 
        /// </summary>
        /// <returns>CPTRequest, Create view</returns>

        // GET: CPTRequest/Create
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View();
        }

        /// <summary>  
        /// The Create action allows for the creation of a new CPT request. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>CPTRequest, Create view</returns>

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
                    RequestStatus = RequestStatus.BMRequestApproval,
                    ProductDivision = model.ProductDivision,
                    Justification = model.Justification,
                    MDecisionId = model.MDecisionId,
                    MReviewRequest = model.MReviewRequest,
                    BEmployeeId = model.BEmployeeId,
                    BDecisionId = model.BDecisionId,
                    REmployeeId = model.REmployeeId,
                    BDecisionMade = model.BDecisionMade,
                    BDecisionDate = model.BDecisionDate,
                    BReview = model.BReview,
                    MDecision = model.MDecision,
                    MDecisionDate = model.MDecisionDate,
                    MReview = model.MReview,
                    LocationId = model.LocationId,
                    SubmittedById = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                };
              
                //var BMID = db.BrandManagers.Where(b => b.ProductDivision == model.ProductDivision);
                //request.BReviewRequest = Convert.ToInt32(BMID);

                // Save the created request to the database.
                db.CPTRequests.Add(request);
                db.SaveChanges();

                return RedirectToAction("SubmittedRequestsIndex");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");

            bool isBrandManager = User.IsInRole("BrandManager");

            if (isBrandManager == true)
            {
                return View();
            }

            return View();

        }

        /// <summary>  
        /// The BrandManagerCreate action allows the Brand Manager user to create a new CPT request. 
        /// </summary>
        /// <returns>CPTRequest, Brand Manager Create view</returns>

        // GET: CPTRequest/BrandManagerCreate
        public ActionResult BrandManagerCreate()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View();
        }

        /// <summary>  
        /// The BrandManagerCreate action allows the Brand Manager user to create a new CPT request. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>CPTRequest, Brand Manager Create view</returns>

        // POST: CPTRequest/BrandManagerCreate
        [HttpPost]
        public ActionResult BrandManagerCreate(CPTRequestViewModel model)
        {
            if (ModelState.IsValid && User.IsInRole("Brand Manager"))
            {
                // Create the request from the model.
                var request = new CPTRequest
                {
                    RequestTitle = model.RequestTitle,
                    RequestDate = DateTime.Now.Date,
                    RequestStatus = RequestStatus.MDRequestApproval,
                    Justification = model.Justification,
                    ProductDivision = model.ProductDivision,
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
                    SubmittedById = User.Identity.GetUserId<int>(),
                };

                // Save the created request to the database.
                db.CPTRequests.Add(request);
                db.SaveChanges();

                return RedirectToAction("SubmittedRequestsIndex");
            }

            else
            {
                return View("Error");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");

            return View();
        }


        /// <summary>  
        /// The MarketingDirectorReview action allows the Marketing Director to review CPT Requests. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CPTRequest, Marketing Director Review view</returns>

        // GET: CPTRequest/MarketingDirectorReview/5
        public ActionResult MarketingDirectorReview(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            CPTRequest request = db.CPTRequests.Find(id);
            if (request == null)
            {
                return View("Error");
            }

            var model = new CPTRequestViewModel
            {
                Id = request.RequestID,
                RequestTitle = request.RequestTitle,
                ProductDivision = request.ProductDivision,
                Justification = request.Justification,
                LocationId = request.LocationId,
                RequestDate = request.RequestDate ?? DateTime.Now.Date,
                //SubmittedBy = request.SubmittedBy,
                RequestStatus = request.RequestStatus,
                BReviewRequest = request.BReviewRequest
            };

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View(model);
        }


        /// <summary>  
        /// The MarketingDirectorReview action allows the Marketing Director to review CPT Requests. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>CPTRequest, Marketing Director Review view</returns>

        // POST: CPTRequest/MarketingDirectorReview/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarketingDirectorReview (int id, CPTRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var request = db.CPTRequests.Find(id);
                if (request == null)
                {
                    return HttpNotFound();
                }

                // Review the request.

                request.BReview = model.BReview;
                request.LocationId = model.LocationId;
                request.BReviewRequest = User.Identity.GetUserId<int>();

                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View();
        }       
    }
}
