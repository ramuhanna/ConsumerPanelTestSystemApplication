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

        // GET: Survey
        public ActionResult Index(int? id)
        {
            // Create the list of possible answers for questions
            // Each questions may have a different number of answers
            // In this case all questions have 5 possible answers

            var possibleAnswers = new List<AnswerViewModel>
            {
                new AnswerViewModel { Id = 1, Text= "Strongly Agree"},
                new AnswerViewModel { Id = 2, Text= "Agree"},
                new AnswerViewModel { Id = 3, Text= "Neutral"},
                new AnswerViewModel { Id = 1, Text= "Disagree"},
                new AnswerViewModel { Id = 1, Text= "Strongly Disagree"},
            };

            var questions = from q in db.QuestionTypes
                                where q.QuestionnaireTypeID == id
                                select q.QuestionID;

            //foreach (var item in questions)
            //{
            //    if (item.ResponseType == ResponseType.RadioButton)
            //    {
            //        var model = new QuestionViewModel()
            //        {
            //            Id = item.QuestionID,
            //            QuestionText = item.QuestionText,
            //            ResponseType = ResponseType.RadioButton,
            //            PossibleAnswers = possibleAnswers
            //        };
            //    }
            //    else if (item.ResponseType == ResponseType.RadioButton)
            //    {
            //        var model = new QuestionViewModel()
            //        {
            //            Id = item.QuestionID,
            //            QuestionText = item.QuestionText,
            //            ResponseType = ResponseType.TextBox,
            //        };
            //    };
            //}

            var modell = new SurveyViewModel();

            return View();
        }
        
        // GET: Survey/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Survey/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Survey/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Survey/Edit/5
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
