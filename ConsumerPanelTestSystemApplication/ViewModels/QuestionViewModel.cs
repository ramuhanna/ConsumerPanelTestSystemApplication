using ConsumerPanelTestSystemApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsumerPanelTestSystemApplication.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Question Text")]
        public string QuestionText { get; set; }

        [Display(Name = "Questionnaire Type(s)")]
        public int QuestionnaireTypeID { get; set; }

        public string QuestionnaireTypeName { get; set; }

        public int QuestionTypeId { get; set; }

    }
}