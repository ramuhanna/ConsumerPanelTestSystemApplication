/*
* Description: This file contains the CPT Request class and the RequestStatus enum.
* Author: R.M.
* Due date: 21/03/2018
*/

namespace ConsumerPanelTestSystemApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>  
    /// This class contains the information regarding a CPT request - who submitted it, the approvals regarding it as well as the final decision.
    /// </summary> 

    [Table("CPTRequest")]
    public partial class CPTRequest
    {
        public CPTRequest()
        {
            ExecutionLocations = new HashSet<ExecutionLocation>();
            ProgressReports = new HashSet<ProgressReport>();
            SelectQuestionnaires = new HashSet<SelectQuestionnaire>();
            //Locations = new HashSet<Location>();
        }

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequestID { get; set; }

        [StringLength(100)]
        public string RequestTitle { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RequestDate { get; set; }

        public RequestStatus? RequestStatus { get; set; }

        //notnull
        public BrandManagerProductDivision? ProductDivision { get; set; }

        //[Required]
        [StringLength(200)]
        public string Justification { get; set; }

        public int? MDecisionId { get; set; }

        public int? MReviewRequest { get; set; }

        public int? BEmployeeId { get; set; }

        public int? BReviewRequest { get; set; }

        public int? BDecisionId { get; set; }

        public int? REmployeeId { get; set; }

        [StringLength(100)]
        public string BDecisionMade { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BDecisionDate { get; set; }

        public Review? BReview { get; set; }

        [StringLength(100)]
        public string MDecision { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MDecisionDate { get; set; }

        public Review? MReview { get; set; }

        public int? SubmittedById { get; set; }

        //public string SubmittedBy { get; set; }

        //notnull
        public int? LocationId { get; set; }

        public string SubmittedByName { get; set; }

        public string BrandManagerName { get; set; }

        public Boolean QuestionnaireExist { get; set; }

        public int? QuestionnaireId { get; set; }

        public virtual BrandManager BrandManagerDecision { get; set; }

        public virtual BrandManager BrandManagerSubmitRequest { get; set; }

        public virtual BrandManager BrandManagerReviewRequest { get; set; }

        public virtual MarketingDirector MarketingDirectorDecision { get; set; }

        public virtual MarketingDirector MarketingDirectorReviewRequest { get; set; }

        public virtual Requester Requester { get; set; }

        public virtual Questionnaire Questionnaire { get; set; }

        public virtual ICollection<ExecutionLocation> ExecutionLocations { get; set; }

        public virtual ICollection<ProgressReport> ProgressReports { get; set; }

        public virtual ICollection<SelectQuestionnaire> SelectQuestionnaires { get; set; }

        public virtual Employee Employee { get; set; }

        //public virtual ICollection<Location> Locations { get; set; }
        public virtual Location Location { get; set; }
    }

    /// <summary>  
    /// This enum contains all the possible values of the RequestStatus property.
    /// </summary> 
  
    public enum RequestStatus
    {
        [Display(Name = "Pending Brand Manager Request Approval")]
        BMRequestApproval = 0,

        [Display(Name = "Pending Marketing Director Request Approval")]
        MDRequestApproval,

        [Display(Name = "Pending Questionnaire Creation")]
        QuestionnaireCreation,

        [Display(Name = "Pending Brand Manager Questionnaire Approval")]
        BMQuestionnaireApproval,

        [Display(Name = "Pending Marketing Director Questionnaire Approval")]
        MDQuestionnaireApproval,

        [Display(Name = "Pending Questionnaire Execution")]
        QuestionnaireExecution,

        [Display(Name = "CRU Supervisor Result Verification")]
        CRUSVerification,

        [Display(Name = "CRU Manager Result Verification")]
        CRUMVerification,

        [Display(Name = "CPT Coordinator Result Verification")]
        CPTCVerification,

        [Display(Name = "Pending Brand Manager Decision")]
        BMDecision,

        [Display(Name = "Pending Marketing Director Decision")]
        MDDecision,

        [Display(Name = "Request Completed")]
        Completed,

        [Display(Name = "Request Rejected")]
        Rejected
    }

    public enum Review
    {
        Approved, Rejected
    }
}
