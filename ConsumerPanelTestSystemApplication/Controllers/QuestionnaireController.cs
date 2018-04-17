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
                    Status = item.Status
                });
            }
            return View(model);
        }

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
                Status = questionnaire.Status
            };

            return View(model);
        }

        // GET: Questionnaire/Create
        public ActionResult Create(int? id)
        {
            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");

            return View();
        }

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
                    QuestionnaireTypeId = model.QuestionnaireTypeId
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

                return RedirectToAction("Create", "Survey", questionnaire.QuestionnaireTypeId);
            }

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View();

        }

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
                ResponseQuantityRequired = questionnaire.ResponseQuantityRequired
            };

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");

            return View(model);
        }

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

                // Edit the location info
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

        // GET: Questionnaire/Delete/5
        public ActionResult Delete(int? id)
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
                Status = questionnaire.Status
            };

            return View(model);
        }

        // POST: Questionnaire/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Questionnaire questionnaire = db.Questionnaires.Find(id);
            db.Questionnaires.Remove(questionnaire);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult Survey (int? id)
        //{
        //    var q = db.Questionnaires.Find(id); // questionnaire id
        //    q.questio                       // question type id
        //                                    // questions
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Survey(int? id)
        //{
        //    return View();
        //}
    }
}
