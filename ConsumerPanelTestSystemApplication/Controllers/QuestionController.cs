/*
* Description: This controller contains the Index, Create, QuestionCreate and Details Actions for Questions.
* Author: R.M.
* Due date: 05/05/2018
*/

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
        /// The Index action is utilized in order to generate a list of the questions. 
        /// </summary>
        /// <returns>Question, Index view</returns>

        [Authorize(Roles = "CPT Coordinator")]
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
                    QuestionText = item.QuestionText,
                });
            }
            return View(model);
        }


        /// <summary>  
        /// The Create action allows for the creation of a new question by the CPT Coordinator user. 
        /// </summary>
        /// <returns>Question, Create view</returns>

        [Authorize(Roles = "CPT Coordinator")]
        // GET: Question/Create
        public ActionResult Create()
        {
            var model = new QuestionViewModel();

            // Select all question types
            var questionTypes = db.QuestionnaireTypes.ToList().OrderBy(n => n.QuestionnaireTypeName);

            // Fill in the model with the question types
            foreach (var item in questionTypes)
            {
                model.QuestionTypes.Add(new SelectListItem { Value = item.QuestionnaireTypeID.ToString(), Text = item.QuestionnaireTypeName });
            }

            return View(model);
        }

        /// <summary>  
        /// The Create action allows for the creation of a new question by the CPT Coordinator user. 
        /// </summary>
        /// <param name="model">QuestionViewModel as the parameter.</param>
        /// <returns>Question, Create view</returns>

        [Authorize(Roles = "CPT Coordinator")]
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
                    QuestionText = model.QuestionText,
                };

                // Save the created question to the database
                db.Questions.Add(question);
                db.SaveChanges();

                // Check if no type is selected
                if (model.QuestionTypes.All(x => x.Selected == false))
                {
                    ModelState.AddModelError("QuestionTypes", "Question should belong to at least one type.");
                    return View(model);
                }

                // Save data into QuestionType table
                QuestionType questionType;
                foreach (var item in model.QuestionTypes)
                {
                    if (item.Selected)
                    {
                        questionType = new QuestionType
                        {
                            QuestionID = question.QuestionID, // from question above
                            QuestionnaireTypeID = int.Parse(item.Value) // from the model
                        };

                        db.QuestionTypes.Add(questionType);
                    }
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // Something wrong if reached
            return View(model);

        }

        /// <summary>  
        /// The QuestionCreate action allows for the creation of a new question by the CPT Coordinator user through a survey. 
        /// </summary>
        /// <returns>Question, QuestionCreate view</returns>

        [Authorize(Roles = "CPT Coordinator")]
        // GET: Question/QuestionCreate
        public ActionResult QuestionCreate()
        {
            
            var model = new QuestionViewModel();

            // Select all question types
            var questionTypes = db.QuestionnaireTypes.ToList().OrderBy(n => n.QuestionnaireTypeName);

            // Fill in the model with the question types
            foreach (var item in questionTypes)
            {
                model.QuestionTypes.Add(new SelectListItem { Value = item.QuestionnaireTypeID.ToString(), Text = item.QuestionnaireTypeName });
            }

            return View(model);
        }

        /// <summary>  
        /// The QuestionCreate action allows for the creation of a new question by the CPT Coordinator user through a survey. 
        /// </summary>
        /// <param name="model">QuestionViewModel as the parameter.</param>
        /// <returns>Question, QuestionCreate view</returns>

        [Authorize(Roles = "CPT Coordinator")]
        // POST: Question/QuestionCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuestionCreate(QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create the question from the model
                var question = new Question
                {
                    QuestionText = model.QuestionText,
                };

                // Save the created question to the database
                db.Questions.Add(question);
                db.SaveChanges();

                // Check if no type is selected
                if (model.QuestionTypes.All(x => x.Selected == false))
                {
                    ModelState.AddModelError("QuestionTypes", "Question should belong to at least one type.");
                    return View(model);
                }

                // Save data into QuestionType table
                QuestionType questionType;
                foreach (var item in model.QuestionTypes)
                {
                    if (item.Selected)
                    {
                        questionType = new QuestionType
                        {
                            QuestionID = question.QuestionID, // from question above
                            QuestionnaireTypeID = int.Parse(item.Value) // from the model
                        };

                        db.QuestionTypes.Add(questionType);
                    }
                }
                db.SaveChanges();

                return RedirectToAction("CPTCoordinatorIndex", "CPTRequest");
            }

            // Something wrong if reached
            return View(model);

        }


        /// <summary>  
        /// The Delete action is utilized in order to delete a question. 
        /// </summary>
        /// <param name="id">QuestionId as the parameter</param>
        /// <returns>Question, Delete view</returns>
        [Authorize(Roles = "CPT Coordinator")]
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
                QuestionText = question.QuestionText,
            };

            return View(model);
        }

        /// <summary>  
        /// The Delete action is utilized in order to delete a question. 
        /// </summary>
        /// <param name="id">QuestionId as the parameter</param>
        /// <returns>Question, Delete view</returns>

        [Authorize(Roles = "CPT Coordinator")]
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
