using ConsumerPanelTestSystemApplication.Controllers;
using ConsumerPanelTestSystemApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsumerPanelTestSystemApplication.ViewModels
{
    public class QuestionViewModel
    {
        public QuestionViewModel()
        {
            QuestionTypes = new List<SelectListItem>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Question Text")]
        public string QuestionText { get; set; }

        [Display(Name = "Response Type")]
        public ResponseType ResponseType { get; set; }

        // Holds values of radio buttons
        public int SelectedAnswer { get; set; }

        // Holds values of textboxes and textarea
        public string Input { get; set; }

        //[Display(Name = "Questionnaire Type(s)")]
        //public int QuestionnaireTypeID { get; set; }

        //public string QuestionnaireTypeName { get; set; }

        //public int QuestionTypeId { get; set; }

        // Holds radio buttons answers
        public List<AnswerViewModel> PossibleAnswers { get; set; }

        //HACK Holds the list of question types
        public List<SelectListItem> QuestionTypes { get; set; }

    }
}