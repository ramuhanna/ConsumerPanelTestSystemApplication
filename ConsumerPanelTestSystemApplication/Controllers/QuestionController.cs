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
    public class QuestionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>  
        /// The Index action is utilized in order to generate a list of Questions. 
        /// </summary>

        // GET: Question
        public ActionResult Index()
        {
            var questions = db.Questions.ToList();

            var model = new List<QuestionViewModel>();
            foreach (var item in questions)
            {
                model.Add(new QuestionViewModel
                {
                    Id = item.QuestionID,
                    QuestionText = item.QuestionText
                });
            }
            return View(model);
        }

        /// <summary>  
        /// The Details action is utilized in order to view the details of a specific question. 
        /// </summary>

        // GET: Question/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }

            var model = new QuestionViewModel
            {
                Id = question.QuestionID,
                QuestionText = question.QuestionText,
                //QuestionnaireTypeName = question.questio
            };

            return View(model);
        }

        // GET: Question/Create
        public ActionResult Create()
        {
            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes.ToList().OrderBy(n => n.QuestionnaireTypeName), "QuestionnaireTypeID", "QuestionnaireTypeName");
            return View();
        }

        /// <summary>  
        /// The Create action allows for the addition of a new Question. 
        /// </summary>

        // POST: Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create the question from the model
                var question = new Question
                {
                    QuestionText = model.QuestionText
                };
                
                // Save the created question to the database
                db.Questions.Add(question);
                db.SaveChanges();

                var questiontypes = new List<QuestionViewModel>
                {

                };

                foreach (var item in questiontypes)
                {
                    var questiontype = new QuestionType
                    {
                        QuestionID = model.Id,
                        QuestionnaireTypeID = model.QuestionnaireTypeID
                    };

                    db.QuestionTypes.Add(questiontype);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes.ToList().OrderBy(n => n.QuestionnaireTypeName), "QuestionnaireTypeID", "QuestionnaireTypeName");
            return View();

        }

        // GET: Question/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }

            QuestionViewModel model = new QuestionViewModel
            {
                Id = question.QuestionID,
                QuestionText = question.QuestionText
            };

            return View(model);
        }

        /// <summary>  
        /// The Edit action allows the editing of a specific Question. 
        /// </summary>

        // POST: Question/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var question = db.Questions.Find(id);
                if (question == null)
                {
                    return HttpNotFound();
                }

                // Edit the question info
                question.QuestionText = model.QuestionText;

                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Question/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            var model = new QuestionViewModel
            {
                Id = question.QuestionID,
                QuestionText = question.QuestionText
            };

            return View(model);
        }

        /// <summary>  
        /// The Delete action removes a specific Question from the database. 
        /// </summary>

        // POST: Question/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
     
    }
}
