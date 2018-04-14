using ConsumerPanelTestSystemApplication.Models;
using ConsumerPanelTestSystemApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsumerPanelTestSystemApplication.Controllers
{
    public class QuestionnaireController : Controller
    {
        // GET: Questionnaire
        public ActionResult Index()
        {
            return View();
        }

        // GET: Questionnaire/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Questionnaire/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questionnaire/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionnaireViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create the location from the model
                var questionnaire = new Questionnaire
                {
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Status = QuestionnaireStatus.BMQuestionnaireApproval,
                    ResponseQuantityRequired = model.ResponseQuantityRequired
                };

                // Save the created course to the database
                db.Questionnaires.Add(questionnaire);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // GET: Questionnaire/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Questionnaire/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Questionnaire/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Questionnaire/Delete/5
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
