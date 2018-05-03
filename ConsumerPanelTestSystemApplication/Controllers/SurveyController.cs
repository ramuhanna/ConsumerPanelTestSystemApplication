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
        /// <param name="qid">QuestionnaireId from the Questionnaire is the parameter in order to link the saved response to its questionnaire.</param> 
        /// <returns>Survey, ConductSurvey view</returns>
        //    // GET: ConductSurvey
        //    public ActionResult ConductSurvey(int? id, int qid)
        //    {
        //        // Create the list of possible answers for questions
        //        // Each questions may have a different number of answers
        //        // In this case all questions have 5 possible answers

        //        var possibleAnswers = new List<AnswerViewModel>
        //        {
        //            new AnswerViewModel { Id = 1, Text= "Strongly Agree"},
        //            new AnswerViewModel { Id = 2, Text= "Agree"},
        //            new AnswerViewModel { Id = 3, Text= "Neutral"},
        //            new AnswerViewModel { Id = 4, Text= "Disagree"},
        //            new AnswerViewModel { Id = 5, Text= "Strongly Disagree"},
        //        };        

        //        var questions = db.QuestionTypes.Where(p => p.QuestionnaireTypeID == id).Select(p => p.Question).ToList();

        //        //var questions = new List<QuestionViewModel>

        //        var model = new List<QuestionViewModel>();

        //        //Loop questions to retrieve relative response type.
        //        foreach (var item in questions)
        //        {

        //            if (item.ResponseType == ResponseType.RadioButton)
        //            {               
        //                model.Add(new QuestionViewModel
        //                {
        //                    Id = item.QuestionID,
        //                    QuestionText = item.QuestionText,
        //                    ResponseType = ResponseType.RadioButton,
        //                    PossibleAnswers = possibleAnswers,                       
        //                });
        //            }

        //            else if (item.ResponseType == ResponseType.TextBox)
        //            {
        //                model.Add(new QuestionViewModel
        //                {
        //                    Id = item.QuestionID,
        //                    QuestionText = item.QuestionText,
        //                    ResponseType = ResponseType.TextBox,
        //                });
        //            };
        //        };              

        //        var model1 = new SurveyViewModel();

        //        model1.Questions = model;

        //        return View(model1);
        //    }


        /// <summary>  
        /// The ConductSurvey action returns a list of questions from the database based on the selected QuestionnaireTypeId, which is the parameter used, and saves the entered response.
        /// </summary> 
        /// <param name="qid">QuestionnaireId from the Questionnaire is the parameter in order to link the saved response to its questionnaire.</param> 
        /// <param name="model">SurveyViewModel as the parameter.</param> 
        /// <returns>Survey, ConductSurvey view</returns>
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult ConductSurvey(int qid, SurveyViewModel model)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            // Save the questions with selected answers from the ViewModel to your database.
        //            foreach (var question in model.Questions)
        //            {
        //                if (question.ResponseType == ResponseType.RadioButton)
        //                {
        //                    var survey = new Survey
        //                    {
        //                        QuestionnaireId = qid,
        //                        QuestionId = model.QuestionId,
        //                        ResponseScore = model.ResponseScore,
        //                    };
        //                    db.Surveys.Add(survey);
        //                    db.SaveChanges();
        //                }
        //                else if (question.ResponseType == ResponseType.TextBox)
        //                {
        //                    var survey1 = new Survey
        //                    {
        //                        QuestionnaireId = qid,
        //                        QuestionId = model.Id,
        //                        ResponseInput = model.ResponseInput,
        //                    };
        //                    db.Surveys.Add(survey1);
        //                    db.SaveChanges();
        //                }

        //            }                     

        //            return View(model);
        //        }

        //        return RedirectToAction("CRUMemberIndex", "Questionnaire");

        //    }


        /// <summary>  
        /// The CreateSurvey action creates a survey from a list of questions from the database based on the selected QuestionnaireTypeId, which is the parameter used.
        /// </summary> 
        /// <param name="id">QuestionnaireTypeId from the Questionnaire is the parameter, in order to sort through the questions in the database.</param> 
        /// <returns>Survey, CreateSurvey view</returns>
        //public ActionResult CreateSurvey(int? id)
        //    {
        //        // Create the list of possible answers for questions
        //        // Each questions may have a different number of answers
        //        // In this case all questions have 5 possible answers

        //        var possibleAnswers = new List<AnswerViewModel>
        //        {
        //            new AnswerViewModel { Id = 1, Text= "Strongly Agree"},
        //            new AnswerViewModel { Id = 2, Text= "Agree"},
        //            new AnswerViewModel { Id = 3, Text= "Neutral"},
        //            new AnswerViewModel { Id = 4, Text= "Disagree"},
        //            new AnswerViewModel { Id = 5, Text= "Strongly Disagree"},
        //        };

        //        var questions = db.QuestionTypes.Where(p => p.QuestionnaireTypeID == id).Select(p => p.Question).ToList();

        //        //var questions = new List<QuestionViewModel>

        //        var model = new List<QuestionViewModel>();

        //        //Loop questions to retrieve relative response type.
        //        foreach (var item in questions)
        //        {

        //            if (item.ResponseType == ResponseType.RadioButton)
        //            {
        //                model.Add(new QuestionViewModel
        //                {
        //                    Id = item.QuestionID,
        //                    QuestionText = item.QuestionText,
        //                    ResponseType = ResponseType.RadioButton,
        //                    PossibleAnswers = possibleAnswers,
        //                });
        //            }

        //            else if (item.ResponseType == ResponseType.TextBox)
        //            {
        //                model.Add(new QuestionViewModel
        //                {
        //                    Id = item.QuestionID,
        //                    QuestionText = item.QuestionText,
        //                    ResponseType = ResponseType.TextBox,
        //                });
        //            };
        //        };

        //        var model1 = new SurveyViewModel();

        //        model1.Questions = model;

        //        return View(model1);
        //    }
    }

    public class AnswerViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class SurveyViewModel
    {
        public SurveyViewModel()
        {
            Questions = new List<QuestionViewModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public virtual Questionnaire Questionnaire { get; set; }

        public string ResponseInput { get; set; }

        public int? ResponseScore { get; set; }

        public int? QuestionnaireId { get; set; }

        public int QuestionId { get; set; }

        public List<QuestionViewModel> Questions { get; set; }
    }
}
