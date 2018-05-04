/*
* Description: This file contains the Questionnaire class and the QuestionnaireStatus enum.
* Author: R.M.
* Due date: 05/05/2018
*/

namespace ConsumerPanelTestSystemApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>  
    /// This class pertains to the questionnaire or survey to be conducted for market research purposes based on an approved CPT Request.
    /// </summary> 

    [Table("Questionnaire")]
    public partial class Questionnaire
    {
        public Questionnaire()
        {
            AssignWorks = new HashSet<AssignWork>();
            ContainQuestions = new HashSet<ContainQuestion>();
            ResponsibleFors = new HashSet<ResponsibleFor>();
            SelectQuestionnaires = new HashSet<SelectQuestionnaire>();
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionnaireID { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public int ResponseQuantityRequired { get; set; }

        public QuestionnaireStatus Status { get; set; }

        public int? MEmployeeID { get; set; }

        public int? BEmployeeID { get; set; }

        public int? CRUSEmployeeID { get; set; }

        public int? CRUMEmployeeID { get; set; }

        public string CRUSEmployeeName { get; set; }

        public string CRUMEmployeeName { get; set; }

        [StringLength(200)]
        public string BComment { get; set; }

        [StringLength(200)]
        public string MComment { get; set; }

        public Review? BReviewQuestionnaire { get; set; }

        public Review? MReviewQuestionnaire { get; set; }

        public int? QuestionnaireTypeId { get; set; }

        public string QuestionnaireTypeName { get; set; }

        public string QuestionnaireTitle { get; set; }

        public virtual ICollection<AssignWork> AssignWorks { get; set; }

        public virtual BrandManager BrandManager { get; set; }

        public virtual ICollection<ContainQuestion> ContainQuestions { get; set; }

        public virtual MarketingDirector MarketingDirector { get; set; }

        public virtual CRUSupervisor CRUSupervisor { get; set; }

        public virtual CRUMember CRUMember { get; set; }

        public virtual ICollection<ResponsibleFor> ResponsibleFors { get; set; }

        public virtual ICollection<SelectQuestionnaire> SelectQuestionnaires { get; set; }

        public virtual Employee Employee { get; set; }

    }

    /// <summary>  
    /// This enum contains all the possible values of the Questionnaire class Status property.
    /// </summary> 

    public enum QuestionnaireStatus
    {
        [Display(Name = "Pending Questionnaire Creation")]
        QuestionnaireCreation,

        [Display(Name = "Pending Brand Manager Questionnaire Approval")]
        BMQuestionnaireApproval,

        [Display(Name = "Pending Marketing Director Questionnaire Approval")]
        MDQuestionnaireApproval,

        [Display(Name = "Pending Questionnaire Modification")]
        QuestionnaireModification,

        [Display(Name = "Pending Questionnaire Execution")]
        QuestionnaireExecution,

        [Display(Name = "CRU Supervisor Result Verification")]
        CRUSVerification,

        [Display(Name = "CRU Manager Result Verification")]
        CRUMVerification,

        [Display(Name = "CPT Coordinator Result Verification")]
        CPTCVerification,
    }
}
