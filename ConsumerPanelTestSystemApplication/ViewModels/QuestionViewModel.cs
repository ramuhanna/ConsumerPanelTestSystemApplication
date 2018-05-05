/*
* Description: This file contains the QuestionViewModel class.
* Author: R.M.
* Due date: 05/05/2018
*/

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
    /// <summary>
    /// Question view model based on the Question model and used by the Question controller.
    /// </summary>

    public class QuestionViewModel
    {
        public QuestionViewModel()
        {
            QuestionTypes = new List<SelectListItem>();
            Surveys = new HashSet<Survey>();
            PossibleAnswers = new List<AnswerViewModel>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Question Text")]
        public string QuestionText { get; set; }

        // Holds values of radio buttons
        public int? SelectedAnswer { get; set; }

        // Holds radio buttons answers
        public List<AnswerViewModel> PossibleAnswers { get; set; }

        public virtual ICollection<Survey> Surveys { get; set; }

        //HACK Holds the list of question types
        public List<SelectListItem> QuestionTypes { get; set; }

    }
}