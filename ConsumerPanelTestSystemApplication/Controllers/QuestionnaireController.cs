﻿/*
* Description: This controller contains the Index, Create, Edit and Details Actions for Questionnaire.
* Author: R.M.
* Due date: 18/04/2018
*/

using ConsumerPanelTestSystemApplication.Models;
using ConsumerPanelTestSystemApplication.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ConsumerPanelTestSystemApplication.Controllers
{
    public class QuestionnaireController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>  
        /// The Index action is utilized in order to generate a list of the questionnaires. 
        /// </summary>
        /// <returns>Questionnaire, Index view</returns>

        [Authorize(Roles = "CPT Coordinator")]
        // GET: Questionnaire
        public ActionResult Index()
        {

            var questionnaires = db.Questionnaires.ToList();

            var model = new List<QuestionnaireViewModel>();
            foreach (var item in questionnaires)
            {
                model.Add(new QuestionnaireViewModel
                {
                    Id = item.QuestionnaireID,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    ResponseQuantityRequired = item.ResponseQuantityRequired,
                    Status = item.Status,
                    QuestionnaireTypeId = item.QuestionnaireTypeId
                });
            }

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View(model);
        }

        [Authorize(Roles = "CRU Member")]
        // GET: Questionnaire
        public ActionResult CRUMemberIndex()
        {
            var questionnaires = db.Questionnaires.Where(q => q.Status == QuestionnaireStatus.QuestionnaireExecution ).ToList();

            var model = new List<QuestionnaireViewModel>();
            foreach (var item in questionnaires)
            {
                model.Add(new QuestionnaireViewModel
                {
                    Id = item.QuestionnaireID,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    ResponseQuantityRequired = item.ResponseQuantityRequired,
                    Status = item.Status,
                    QuestionnaireTypeId = item.QuestionnaireTypeId,
                    QuestionnaireTypeName = item.QuestionnaireTypeName,
                    QuestionnaireTitle = item.QuestionnaireTitle
                });

                var sq = db.SelectQuestionnaires.Where(s => s.QuestionnaireID == item.QuestionnaireID).FirstOrDefault();
                var request = db.CPTRequests.Find(sq.RequestID);              

            }

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View(model);
        }


        /// <summary>  
        /// The Details action allows the user to view the details of a questionnaire. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Questionnaire, Details view</returns>

        [Authorize(Roles = "CPT Coordinator")]
        // GET: Questionnaire/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Questionnaire questionnaire = db.Questionnaires.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }

            var model = new QuestionnaireViewModel
            {
                Id = questionnaire.QuestionnaireID,
                StartDate = questionnaire.StartDate,
                EndDate = questionnaire.EndDate,
                ResponseQuantityRequired = questionnaire.ResponseQuantityRequired,
                Status = questionnaire.Status,
                QuestionnaireTypeId = questionnaire.QuestionnaireTypeId,
            };

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View(model);
        }

        [Authorize(Roles = "Marketing Director, Brand Manager")]
        // GET: Questionnaire/ReviewDetails/5
        public ActionResult ReviewDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SelectQuestionnaire selectquestionnaire = db.SelectQuestionnaires.Where(s => s.RequestID == id).FirstOrDefault();

            Questionnaire questionnaire = db.Questionnaires.Find(selectquestionnaire.QuestionnaireID);
            if (questionnaire == null)
            {
                return HttpNotFound("There is no approved questionnaire created for this request.");
            }

            var model = new QuestionnaireViewModel
            {
                Id = questionnaire.QuestionnaireID,
                StartDate = questionnaire.StartDate,
                EndDate = questionnaire.EndDate,
                ResponseQuantityRequired = questionnaire.ResponseQuantityRequired,
                Status = questionnaire.Status,
                QuestionnaireTypeId = questionnaire.QuestionnaireTypeId,
                Location = selectquestionnaire.CPTRequest.Location
            };

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View(model);
        }


        [Authorize(Roles = "Marketing Director, Brand Manager")]
        // GET: Questionnaire/ReviewDetailsPartial/5
        public ActionResult ReviewDetailsPartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SelectQuestionnaire selectquestionnaire = db.SelectQuestionnaires.Where(s => s.RequestID == id).FirstOrDefault();

            Questionnaire questionnaire = db.Questionnaires.Find(selectquestionnaire.QuestionnaireID);
            if (questionnaire == null)
            {
                return HttpNotFound("There is no approved questionnaire created for this request.");
            }

            var model = new QuestionnaireViewModel
            {
                Id = questionnaire.QuestionnaireID,
                StartDate = questionnaire.StartDate,
                EndDate = questionnaire.EndDate,
                ResponseQuantityRequired = questionnaire.ResponseQuantityRequired,
                Status = questionnaire.Status,
                QuestionnaireTypeId = questionnaire.QuestionnaireTypeId,
                Location = selectquestionnaire.CPTRequest.Location
            };

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return PartialView(model);
        }


        /// <summary>  
        /// The Create action allows for the creation of a new question by the CPT Coordinator user. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Questionnaire, Create view</returns>

        [Authorize(Roles = "CPT Coordinator")]
        // GET: Questionnaire/Create
        public ActionResult Create(int? id)
        {
            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");

            return View();
        }


        /// <summary>  
        /// The Create action allows for the creation of a new question by the CPT Coordinator user. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Questionnaire, Create view</returns>

        [Authorize(Roles = "CPT Coordinator")]
        // POST: Questionnaire/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CPT Coordinator")]
        public ActionResult Create(int? id, QuestionnaireViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create the location from the model
                var questionnaire = new Questionnaire
                {
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Status = QuestionnaireStatus.BMQuestionnaireApproval,
                    ResponseQuantityRequired = model.ResponseQuantityRequired,
                    QuestionnaireTypeId = model.QuestionnaireTypeId,
                    QuestionnaireTypeName = model.QuestionnaireTypeName
                };

                // Save the created course to the database
                db.Questionnaires.Add(questionnaire);
                db.SaveChanges();
                
                var sq = new SelectQuestionnaire
                {
                    QuestionnaireID = questionnaire.QuestionnaireID,
                    RequestID = id,
                    CPTEmployeeID = User.Identity.GetUserId<int>()
                };
                db.SelectQuestionnaires.Add(sq);
                db.SaveChanges();

                if (questionnaire.Status == QuestionnaireStatus.BMQuestionnaireApproval)
                {
                    var request = db.CPTRequests.Where(r => r.RequestID == id).FirstOrDefault();
                    request.QuestionnaireExist = true;
                    request.RequestStatus = RequestStatus.BMQuestionnaireApproval;
                    db.Entry(request).State = EntityState.Modified;
                    db.SaveChanges();

                    questionnaire.QuestionnaireTitle = request.RequestTitle;
                    db.Entry(questionnaire).State = EntityState.Modified;
                    db.SaveChanges();
                }               

                return RedirectToAction("Details", "Questionnaire", new { id = questionnaire.QuestionnaireID});
                //return RedirectToAction("CreateSurvey", "Survey", new { id = sq.Questionnaire.QuestionnaireTypeId });
                //return RedirectToAction("Index");
            }
            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View();
        }


        /// <summary>  
        /// The Edit action allows the user to edit the questionnaire details. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Questionnaire, Edit view</returns>

        [Authorize(Roles = "CPT Coordinator")]
        // GET: Questionnaire/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Questionnaire questionnaire = db.Questionnaires.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }

            QuestionnaireViewModel model = new QuestionnaireViewModel
            {
                Id = questionnaire.QuestionnaireID,
                StartDate = questionnaire.StartDate,
                EndDate = questionnaire.EndDate,
                ResponseQuantityRequired = questionnaire.ResponseQuantityRequired,
                QuestionnaireTypeId = questionnaire.QuestionnaireTypeId
            };

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");

            return View(model);
        }


        /// <summary>  
        /// The Edit action allows the user to edit the questionnaire details. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Questionnaire, Edit view</returns>

        [Authorize(Roles = "CPT Coordinator")]
        // POST: Questionnaire/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, QuestionnaireViewModel model)
        {
            if (ModelState.IsValid)
            {
                var questionnaire = db.Questionnaires.Find(id);
                if (questionnaire == null)
                {
                    return HttpNotFound();
                }

                // Edit the questionnaire info
                questionnaire.StartDate = model.StartDate;
                questionnaire.EndDate = model.EndDate;
                questionnaire.ResponseQuantityRequired = model.ResponseQuantityRequired;
                questionnaire.QuestionnaireTypeId = model.QuestionnaireTypeId;

                db.Entry(questionnaire).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View();
        }

        /// <summary>  
        /// The Edit action allows the user to edit the questionnaire details. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Questionnaire, Edit view</returns>

        [Authorize(Roles = "CPT Coordinator")]
        // GET: Questionnaire/Edit/5
        public ActionResult BrandManagerReviewQuestionnaire(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Questionnaire questionnaire = db.Questionnaires.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }

            QuestionnaireViewModel model = new QuestionnaireViewModel
            {
                Id = questionnaire.QuestionnaireID,
                StartDate = questionnaire.StartDate,
                EndDate = questionnaire.EndDate,
                ResponseQuantityRequired = questionnaire.ResponseQuantityRequired,
                QuestionnaireTypeId = questionnaire.QuestionnaireTypeId
            };

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");

            return View(model);
        }


        /// <summary>  
        /// The Edit action allows the user to edit the questionnaire details. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Questionnaire, Edit view</returns>

        [Authorize(Roles = "CPT Coordinator")]
        // POST: Questionnaire/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BrandManagerReviewQuestionnaire(int? id, QuestionnaireViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Questionnaire questionnaire = db.Questionnaires.Find(id);
                if (questionnaire == null)
                {
                    return HttpNotFound();
                }
               

                // Edit the questionnaire info
                questionnaire.StartDate = model.StartDate;
                questionnaire.EndDate = model.EndDate;
                questionnaire.ResponseQuantityRequired = model.ResponseQuantityRequired;
                questionnaire.QuestionnaireTypeId = model.QuestionnaireTypeId;

                db.Entry(questionnaire).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View();
        }

       
    }
}
