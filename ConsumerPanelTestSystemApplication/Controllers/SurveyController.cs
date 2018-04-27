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
        /// The Index action returns a list of questions from the database based on the selected QuestionnaireTypeId, which is the parameter used.
        /// </summary>
        /// <param name="id"></param> // The id used in the method is the QuestionnaireTypeId from the Questionnaire, in order to sort through the questions in the database.
        /// <returns>Survey, Index view</returns>
        // GET: Survey
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

            //var questions = new List<QuestionViewModel>

            var model = new List<QuestionViewModel>();

            //Loop questions to retrieve relative response type.
            foreach (var item in questions)
            {

                if (item.ResponseType == ResponseType.RadioButton)
                {               
                    model.Add(new QuestionViewModel
                    {
                        Id = item.QuestionID,
                        QuestionText = item.QuestionText,
                        ResponseType = ResponseType.RadioButton,
                        PossibleAnswers = possibleAnswers,                       
                    });
                }

                else if (item.ResponseType == ResponseType.TextBox)
                {
                    model.Add(new QuestionViewModel
                    {
                        Id = item.QuestionID,
                        QuestionText = item.QuestionText,
                        ResponseType = ResponseType.TextBox,
                    });
                };
            };              

            var model1 = new SurveyViewModel();

            model1.Questions = model;

            return View(model1);
        }


        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult ConductSurvey(SurveyViewModel model)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            // Save the questions with selected answers from the ViewModel to your database
        //            foreach (var question in model.Questions)
        //            {
        //                if (question.ResponseType == ResponseType.RadioButton)
        //                {
        //                    // Create the response from the model
        //                    var response = new ResponseViewModel
        //                    {
        //                        QuestionId = question.Id,
        //                        ResponseScore = question.SelectedAnswer,
        //                        //CRUMemberId = User.Identity.GetUserId(),
        //                    };

        //                    //model.Add(new ResponseViewModel
        //                    //{
        //                    //    Id = item.QuestionID,
        //                    //    QuestionText = item.QuestionText,
        //                    //    ResponseType = ResponseType.RadioButton,
        //                    //    PossibleAnswers = possibleAnswers
        //                    //});
        //                }

        //                else if (item.ResponseType == ResponseType.TextBox)
        //                {
        //                    model.Add(new QuestionViewModel
        //                    {
        //                        Id = item.QuestionID,
        //                        QuestionText = item.QuestionText,
        //                        ResponseType = ResponseType.TextBox,
        //                    });
        //                };
        //            };

        //            var model1 = new SurveyViewModel();

        //            model1.Questions = model;

        //            return View(model1);
        //        }

        //        // Return to to your home
        //        return RedirectToAction("Index", "Home");
        //    }

        //        // Issue with the model
        //        return View(model);
        //}


        /// <summary>  
        /// The Index action returns a list of questions from the database based on the selected QuestionnaireTypeId, which is the parameter used.
        /// </summary>
        /// <param name="id"></param> // The id used in the method is the QuestionnaireTypeId from the Questionnaire, in order to sort through the questions in the database.
        /// <returns>Survey, Index view</returns>
        // GET: Survey
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

            //var questions = new List<QuestionViewModel>

            var model = new List<QuestionViewModel>();

            //Loop questions to retrieve relative response type.
            foreach (var item in questions)
            {

                if (item.ResponseType == ResponseType.RadioButton)
                {
                    model.Add(new QuestionViewModel
                    {
                        Id = item.QuestionID,
                        QuestionText = item.QuestionText,
                        ResponseType = ResponseType.RadioButton,
                        PossibleAnswers = possibleAnswers,
                    });
                }

                else if (item.ResponseType == ResponseType.TextBox)
                {
                    model.Add(new QuestionViewModel
                    {
                        Id = item.QuestionID,
                        QuestionText = item.QuestionText,
                        ResponseType = ResponseType.TextBox,
                    });
                };
            };

            var model1 = new SurveyViewModel();

            model1.Questions = model;

            return View(model1);
        }
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
        public List<QuestionViewModel> Questions { get; set; }
    }
}
