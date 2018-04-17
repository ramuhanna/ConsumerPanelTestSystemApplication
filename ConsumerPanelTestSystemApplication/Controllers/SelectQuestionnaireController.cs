using ConsumerPanelTestSystemApplication.Models;
using ConsumerPanelTestSystemApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsumerPanelTestSystemApplication.Controllers
{
    public class SelectQuestionnaireController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SelectQuestionnaire
        public ActionResult Index()
        {
            return View();
        }

        // GET: SelectQuestionnaire/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SelectQuestionnaire/Create
        public ActionResult Create()
        {
            ViewBag.CPTRequestId = new SelectList(db.CPTRequests, "RequestId", "RequestTitle");
            ViewBag.QuestionnaireId = new SelectList(db.Questionnaires, "RequestId", "RequestTitle");
            return View();
        }

        // POST: SelectQuestionnaire/Create
        [HttpPost]
        public ActionResult Create(SelectQuestionnaireViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create the location from the model
                var selectQuestionnaire = new SelectQuestionnaire
                {
                    CPTEmployeeID = 3,
                    RequestID = model.RequestID,
                    QuestionnaireID = model.QuestionnaireID
                };

                // Save the created course to the database
                db.SelectQuestionnaires.Add(selectQuestionnaire);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CPTRequestId = new SelectList(db.CPTRequests, "RequestId", "RequestTitle");
            return View();
        }

        // GET: SelectQuestionnaire/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SelectQuestionnaire/Edit/5
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

        // GET: SelectQuestionnaire/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SelectQuestionnaire/Delete/5
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
