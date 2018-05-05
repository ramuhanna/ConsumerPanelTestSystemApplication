/*
* Description: This controller contains the CreateSurvey and ConductSurvey actions for the Survey model.
* Author: R.M.
* Due date: 05/05/2018
*/

using ConsumerPanelTestSystemApplication.Models;
using ConsumerPanelTestSystemApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsumerPanelTestSystemApplication.Controllers
{
    public class SurveyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>  
        /// The ConductSurvey action returns a list of questions from the database based on the selected QuestionnaireTypeId, which is the parameter used, and saves the entered response.
        /// </summary>
        /// <param name="id">QuestionnaireTypeId from the Questionnaire is the parameter, in order to sort through the questions in the database.</param> 
        /// <returns>Survey, ConductSurvey view</returns>
            // GET: ConductSurvey
        public ActionResult ConductSurvey(int? id)
        {
            // Create the list of possible answers for questions
            // Each questions may have a different number of answers
            // In this case all questions have 5 possible answers

            var possibleAnswers = new List<AnswerViewModel>
            {
                new AnswerViewModel { Id = 1, Text= "Strongly Agree"},
                new AnswerViewModel { Id = 2, Text= "Agree"},
                new AnswerViewModel { Id = 3, Text= "Neutral"},
                new AnswerViewModel { Id = 4, Text= "Disagree"},
                new AnswerViewModel { Id = 5, Text= "Strongly Disagree"},

            };


            var questions = db.QuestionTypes.Where(p => p.QuestionnaireTypeID == id).Select(p => p.Question).ToList();

            var model = new List<QuestionViewModel>();

            foreach (var item in questions)
            {
                model.Add(new QuestionViewModel
                {
                    Id = item.QuestionID,
                    QuestionText = item.QuestionText,
                    PossibleAnswers = possibleAnswers
                });
            }

            var modelx = new SurveyViewModel();

            modelx.Questions = model;

            return View(modelx);

        }


        /// <summary>  
        /// The ConductSurvey action returns a list of questions from the database based on the selected QuestionnaireTypeId, which is the parameter used, and saves the entered response.
        /// </summary> 
        /// <param name="model">SurveyViewModel as the parameter.</param> 
        /// <returns>Survey, ConductSurvey view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConductSurvey(SurveyViewModel model)
        {
            decimal sum = 0;
            int count = 0;
            if (ModelState.IsValid)
            {
                //Questionnaire questionnaire = db.Questionnaires.Find(qid);
                //if (questionnaire == null)
                //{
                //    return HttpNotFound();
                //}

                foreach (var question in model.Questions)
                {
                    if (question.SelectedAnswer.HasValue)
                    {
                        count++;
                        sum = sum + question.SelectedAnswer.Value;
                    }
                }

                var survey = new Survey
                {
                    SurveyId = model.Id,
                    Grade = sum,
                };

                foreach (var question in model.Questions)
                {
                    model.Id = question.Id;
                    model.SelectedAnswer = question.SelectedAnswer;
                }

                db.Surveys.Add(survey);
                db.SaveChanges();
                return RedirectToAction("CRUMemberIndex", "Questionnaire");
            }

            return View(model);
            //return RedirectToAction("CRUMemberIndex", "Questionnaire");
        }



        /// <summary>  
        /// The CreateSurvey action creates a survey from a list of questions from the database based on the selected QuestionnaireTypeId, which is the parameter used.
        /// </summary> 
        /// <param name="id">QuestionnaireTypeId from the Questionnaire is the parameter, in order to sort through the questions in the database.</param> 
        /// <returns>Survey, CreateSurvey view</returns>
        public ActionResult CreateSurvey(int? id)
        {
            // Create the list of possible answers for questions
            // Each questions may have a different number of answers
            // In this case all questions have 5 possible answers

            var possibleAnswers = new List<AnswerViewModel>
                {
                    new AnswerViewModel { Id = 1, Text= "Strongly Agree"},
                    new AnswerViewModel { Id = 2, Text= "Agree"},
                    new AnswerViewModel { Id = 3, Text= "Neutral"},
                    new AnswerViewModel { Id = 4, Text= "Disagree"},
                    new AnswerViewModel { Id = 5, Text= "Strongly Disagree"},
                };

            var questions = db.QuestionTypes.Where(p => p.QuestionnaireTypeID == id).Select(p => p.Question).ToList();
            var model = new List<QuestionViewModel>();

            foreach (var item in questions)
            {
                model.Add(new QuestionViewModel
                {
                    Id = item.QuestionID,
                    QuestionText = item.QuestionText,
                    PossibleAnswers = possibleAnswers,
                });
            }

            var modelx = new SurveyViewModel();

            modelx.Questions = model;

            return View(modelx);
        }
    }
}

    

