/*
* Description: This controller contains the Index, Create and Details Actions for CPTRequests.
* Author: R.M.
* Due date: 18/04/2018
*/

using ConsumerPanelTestSystemApplication.Models;
using ConsumerPanelTestSystemApplication.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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
        /// The MarketingDirectorReviewIndex action is utilized in order to generate a list of CPT Requests to be reviewed by the Marketing Director. 
        /// </summary>
        /// <returns>CPTRequest, MarketingDirectorReviewIndex View</returns>

        [Authorize(Roles = "Marketing Director")]
        // GET: CPTRequest
        public ActionResult MarketingDirectorReviewIndex()
        {
            if (User.Identity.IsAuthenticated)
            {
                 var requests = db.CPTRequests.Where(b => b.RequestStatus != RequestStatus.BMRequestApproval && b.BReview == Review.Approved).ToList();
                var model = new List<CPTRequestViewModel>();

                foreach (var item in requests)
                {
                    model.Add(new CPTRequestViewModel
                    {
                        Id = item.RequestID,
                        RequestTitle = item.RequestTitle,
                        RequestDate = item.RequestDate,
                        QuestionnaireExist = item.QuestionnaireExist,
                        QuestionnaireId = item.QuestionnaireId,
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
        /// The CPTCoordinatorIndex action is utilized in order to generate a list of CPT Requests to be viewed by the CPT Coordinator.
        /// </summary>
        /// <returns> CPTRequest, CPTCoordinatorIndex View</returns>

        [Authorize(Roles = "CPT Coordinator")]
        // GET: CPTCoordinatorIndex
        public ActionResult CPTCoordinatorIndex()
        {
                var requests = db.CPTRequests.Where(b => (b.RequestStatus != RequestStatus.BMRequestApproval) && (b.RequestStatus != RequestStatus.Rejected) && (b.RequestStatus != RequestStatus.MDRequestApproval)).ToList();
                var model = new List<CPTRequestViewModel>();

                foreach (var item in requests)
                {
                    model.Add(new CPTRequestViewModel
                    {
                        Id = item.RequestID,
                        QuestionnaireId = item.QuestionnaireId,
                        RequestTitle = item.RequestTitle,
                        RequestDate = item.RequestDate,
                        //REmployeeId = item.REmployeeId,
                        //SubmittedById = item.SubmittedById,
                        ProductDivision = item.ProductDivision,
                        RequestStatus = item.RequestStatus,
                        SubmittedByName = item.Employee.FullName,
                        QuestionnaireExist = item.QuestionnaireExist
                    });
                }
                return View(model);          

        }


        /// <summary>  
        /// The SubmittedRequestsIndex action is utilized in order to generate a list of the CPT Requests that were submitted by the logged in user. 
        /// </summary>
        /// <returns>CPTRequest, Submitted Requests Index View</returns>

        [Authorize(Roles = "Brand Manager, Requester")]
        // GET: CPTRequest
        public ActionResult SubmittedRequestsIndex()
        {
            if (User.Identity.IsAuthenticated)
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
        }

        /// <summary>  
        /// The BrandManagerReviewIndex action is utilized in order to generate a list of the CPT Requests to be reviewed by the Brand Manager. 
        /// </summary>
        /// <returns>CPTRequest, Brand Manager Review Index View</returns>

        [Authorize(Roles = "Brand Manager")]
        // GET: CPTRequest
        public ActionResult BrandManagerReviewIndex()
        {
            var user = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id;
            var requests = db.CPTRequests.Where(b => b.BReviewRequest == user).ToList();

            var model = new List<CPTRequestViewModel>();

            foreach (var item in requests)
            {

                model.Add(new CPTRequestViewModel
                {
                    Id = item.RequestID,
                    QuestionnaireId = item.QuestionnaireId,
                    QuestionnaireExist = item.QuestionnaireExist,
                    RequestTitle = item.RequestTitle,
                    RequestDate = item.RequestDate,
                    RequestStatus = item.RequestStatus,
                    SubmittedById = item.SubmittedById,
                    SubmittedByName = item.Employee.FullName

                });
            }

            return View(model);

        }


        /// <summary>  
        /// The BrandManagerReviewIndexPartial action is utilized in order to generate a list of the CPT Requests to be reviewed by the Brand Manager. 
        /// </summary>
        /// <returns>CPTRequest, Brand Manager Review Index Partial View</returns>

        [Authorize(Roles = "Brand Manager")]
        // GET: CPTRequest
        public PartialViewResult BrandManagerReviewIndexPartial()
        {
            var user = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id;
            var requests = db.CPTRequests.Where(b => b.BReviewRequest == user && b.SubmittedById != user).ToList();

            var model = new List<CPTRequestViewModel>();

            foreach (var item in requests)
            {

                model.Add(new CPTRequestViewModel
                {
                    Id = item.RequestID,
                    RequestTitle = item.RequestTitle,
                    RequestDate = item.RequestDate,
                    //ProductDivision = item.ProductDivision,
                    RequestStatus = item.RequestStatus,
                    SubmittedById = item.SubmittedById,
                    SubmittedByName = item.Employee.FullName

                });
            }

            return PartialView(model);
        }


        /// <summary>  
        /// The PendingRequestsDetails action is utilized by the Marketing Director and Brand Manager in order to view the details of a specific CPT Request. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CPTRequest, PendingRequestsDetails view</returns>

        [Authorize(Roles = "Marketing Director, Brand Manager")]
        // GET: CPTRequest/PendingRequestsDetails/5
        public ActionResult PendingRequestsDetails(int? id)
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
                SubmittedById = request.SubmittedById,
                RequestStatus = request.RequestStatus,
                BReviewRequest = request.BReviewRequest,  
                SubmittedByName = request.Employee.FullName,
                BReview = request.BReview,
                BrandManagerName = db.BrandManagers.Find(request.BReviewRequest).FullName,
                MReview = request.MReview
            };

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View(model);
        }

        /// <summary>  
        /// The Details action is utilized by the CPT Coordinator in order to view the details of a specific CPT Request. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CPTRequest, Details view</returns>

        [Authorize(Roles = "CPT Coordinator")]
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
                RequestDate = request.RequestDate ?? DateTime.Now.Date,
                //REmployeeId = request.REmployeeId,
                SubmittedById = request.SubmittedById,
                RequestStatus = request.RequestStatus,
                SubmittedByName = request.Employee.FullName
            };

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View(model);
        }

        /// <summary>  
        /// The SubmittedRequestsDetails action is utilized by the Brand Manager and Requester users in order to view the details of a specific CPT Request. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CPTRequest, SubmittedRequestsDetails view</returns>

        [Authorize(Roles = "Brand Manager, Requester")]
        // GET: CPTRequest/SubmittedRequestsDetails/5
        public ActionResult SubmittedRequestsDetails(int? id)
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
                RequestDate = request.RequestDate ?? DateTime.Now.Date,
                RequestStatus = request.RequestStatus,
                BReviewRequest = request.BReviewRequest,
                BReview = request.BReview,
                BrandManagerName = db.BrandManagers.Find(request.BReviewRequest).FullName,
            };

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View(model);
        }

        /// <summary>  
        /// The RequesterCreate action allows for the creation of a new CPT request by a requester user. 
        /// </summary>
        /// <returns>CPTRequest, Requester Create view</returns>

        [Authorize(Roles = "Requester")]
        // GET: CPTRequest/RequesterCreate
        public ActionResult RequesterCreate()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View();
        }

        /// <summary>  
        /// The RequesterCreate action allows for the creation of a new CPT request by a requester user. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>CPTRequest, RequesterCreate view</returns>

        [Authorize(Roles = "Requester")]
        // POST: CPTRequest/RequesterCreate
        [HttpPost]
        public ActionResult RequesterCreate(CPTRequestViewModel model)
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
                    //BReviewRequest = 
                    
                };
                var result = from b in db.BrandManagers
                             where b.ProductDivision == model.ProductDivision
                             select b.Id;

                request.BReviewRequest = result.First();

                //var BMID = db.BrandManagers.Where(b => b.ProductDivision == model.ProductDivision).First();

                // Save the created request to the database.
                db.CPTRequests.Add(request);
                db.SaveChanges();

                return RedirectToAction("SubmittedRequestsIndex");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");

            return View();

        }

        /// <summary>  
        /// The BrandManagerCreate action allows the Brand Manager user to create a new CPT request. 
        /// </summary>
        /// <returns>CPTRequest, Brand Manager Create view</returns>

        [Authorize(Roles = "Brand Manager")]
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

        [Authorize(Roles = "Brand Manager")]
        // POST: CPTRequest/BrandManagerCreate
        [HttpPost]
        public ActionResult BrandManagerCreate(CPTRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create the request from the model.
                var request = new CPTRequest
                {
                    RequestTitle = model.RequestTitle,
                    RequestDate = DateTime.Now.Date,
                    RequestStatus = RequestStatus.MDRequestApproval,
                    Justification = model.Justification,
                    MDecisionId = model.MDecisionId,
                    MReviewRequest = model.MReviewRequest,
                    BEmployeeId = model.BEmployeeId,
                    BReviewRequest = User.Identity.GetUserId<int>(),
                    BDecisionId = model.BDecisionId,
                    REmployeeId = model.REmployeeId,
                    BDecisionMade = model.BDecisionMade,
                    BDecisionDate = model.BDecisionDate,
                    BReview = Review.Approved,
                    MDecision = model.MDecision,
                    MDecisionDate = model.MDecisionDate,
                    MReview = model.MReview,
                    LocationId = model.LocationId,
                    SubmittedById = User.Identity.GetUserId<int>(),
                };

                // Save the created request to the database.
                db.CPTRequests.Add(request);
                db.SaveChanges();

                var result = from b in db.BrandManagers
                             where b.Id == request.SubmittedById
                             select b.ProductDivision;

                request.ProductDivision = result.First();

                db.SaveChanges();

                return RedirectToAction("SubmittedRequestsIndex");
            }
            
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View();
        }


        /// <summary>  
        /// The MarketingDirectorReview action allows the Marketing Director to review CPT Requests. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CPTRequest, Marketing Director Review view</returns>

        [Authorize(Roles = "Marketing Director")]
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
                RequestDate = request.RequestDate,
                RequestStatus = request.RequestStatus,
                BReviewRequest = request.BReviewRequest,
                BReview = request.BReview,
                SubmittedByName = db.Employees.Find(request.SubmittedById).FullName,
                SubmittedById = request.SubmittedById,
                BrandManagerName = db.BrandManagers.Find(request.BReviewRequest).FullName,
                City = request.Location.City,
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
        [Authorize(Roles = "Marketing Director")]
        public ActionResult MarketingDirectorReview(int id, CPTRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var request = db.CPTRequests.Find(id);
                if (request == null)
                {
                    return HttpNotFound();
                }

                // Review the request.
                request.MReview = model.MReview;
                request.MReviewRequest = 2;

                if (request.MReview == Review.Approved)
                {
                    request.RequestStatus = RequestStatus.QuestionnaireCreation;
                }
                else if (request.MReview == Review.Rejected)
                {
                    request.RequestStatus = RequestStatus.Rejected;
                }

                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("MarketingDirectorReviewIndex");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View();
        }

        /// <summary>  
        /// The BrandManagerReview action allows the Marketing Director to review CPT Requests. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CPTRequest, BrandManagerReview view</returns>

        [Authorize(Roles = "Brand Manager")]
        // GET: CPTRequest/BrandManagerReview/5
        public ActionResult BrandManagerReview(int? id)
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
                RequestDate = request.RequestDate,
                RequestStatus = request.RequestStatus,                
                SubmittedByName = db.Employees.Find(request.SubmittedById).FullName,
                SubmittedById = request.SubmittedById,               
                City = request.Location.City,
            };

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View(model);
        }


        /// <summary>  
        /// The BrandManagerReview action allows the Brand Manager to review CPT Requests. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>CPTRequest, BrandManagerReview view</returns>

        [Authorize(Roles = "Brand Manager")]
        // POST: CPTRequest/BrandManagerReview/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BrandManagerReview(int id, CPTRequestViewModel model)
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

                if (request.BReview == Review.Approved)
                {
                    request.RequestStatus = RequestStatus.MDRequestApproval;
                }
                else if (request.BReview == Review.Rejected)
                {
                    request.RequestStatus = RequestStatus.Rejected;
                }

                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("BrandManagerReviewIndex");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View();
        }
    }
}
