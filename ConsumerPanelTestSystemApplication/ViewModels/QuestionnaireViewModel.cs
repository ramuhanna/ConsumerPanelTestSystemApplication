using ConsumerPanelTestSystemApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsumerPanelTestSystemApplication.ViewModels
{
    public class QuestionnaireViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Start Date")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Response Quantity Required")]
        public int ResponseQuantityRequired { get; set; }

        public QuestionnaireStatus Status { get; set; }
       
        public int? QuestionnaireTypeId { get; set; }
         
        [Display(Name = "Questionnaire Type")]
        public string QuestionnaireTypeName { get; set; }

        public int? MEmployeeID { get; set; }

        public int? BEmployeeID { get; set; }

        public int? CRUSEmployeeID { get; set; }

        [Display(Name = "CRU Supervisor Responsible")]     
        public string CRUSEmployeeName { get; set; }

        public int? CRUMEmployeeID { get; set; }

        [Display(Name = "CRU Member Responsible")]
        public string CRUMEmployeeName { get; set; }

        [Display(Name = "Brand Manager Feedback")]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string BComment { get; set; }

        [Display(Name = "Marketing Director Feedback")]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string MComment { get; set; }

        [Display(Name = "Brand Manager Decision")]
        public Boolean? BReviewQuestionnaire { get; set; }

        [Display(Name = "Marketing Director Decision")]
        public Boolean? MReviewQuestionnaire { get; set; }

        public Location Location { get; set; }

        public string QuestionnaireTitle { get; set; }

    }
}