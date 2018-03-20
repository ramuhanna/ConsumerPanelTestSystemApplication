/*
* Description: The Consumer Panel Test System is a Web-Based application utilized for organized and systemized market research process.
* Author: R.M.
* Due date: 20/03/2018
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CPTRequest()
        {
            ExecutionLocations = new HashSet<ExecutionLocation>();
            ProgressReports = new HashSet<ProgressReport>();
            SelectQuestionnaires = new HashSet<SelectQuestionnaire>();
            Locations = new HashSet<Location>();
        }

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequestID { get; set; }

        [StringLength(100)]
        public string RequestTitle { get; set; }

        [Column(TypeName = "date")]
        public DateTime RequestDate { get; set; }

        public RequestStatus RequestStatus { get; set; }

        public BrandManagerProductDivision ProductDivision { get; set; }

        [Required]
        [StringLength(200)]
        public string Justification { get; set; }

        public int MDecisionId { get; set; }

        public int MReviewRequest { get; set; }

        public int? BEmployeeID { get; set; }

        public int BReviewRequest { get; set; }

        public int BDecisionId { get; set; }

        public int? REmployeeID { get; set; }

        [StringLength(100)]
        public string BDecisionMade { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BDecisionDate { get; set; }

        public Boolean BReview { get; set; }

        [StringLength(100)]
        public string MDecision { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MDecisionDate { get; set; }

        public Boolean MReview { get; set; }

        [Required]
        public int LocationId { get; set; }

        public virtual BrandManager BrandManagerDecision { get; set; }

        public virtual BrandManager BrandManagerSubmitRequest { get; set; }

        public virtual BrandManager BrandManagerReviewRequest { get; set; }

        public virtual MarketingDirector MarketingDirectorDecision { get; set; }

        public virtual MarketingDirector MarketingDirectorReviewRequest { get; set; }

        public virtual Requester Requester { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExecutionLocation> ExecutionLocations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProgressReport> ProgressReports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SelectQuestionnaire> SelectQuestionnaires { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }

    public enum RequestStatus
    {
        [Display(Name = "Pending Brand Manager Request Approval")]
        BMRequestApproval,

        [Display(Name = "Pending Marketing Director Request Approval")]
        MDRequestApproval,

        [Display(Name = "Pending Brand Manager Decision")]
        BMDecision,

        [Display(Name = "Pending Marketing Director Decision")]
        MDDecision,

        [Display(Name = "Request Completed")]
        Completed,

        [Display(Name = "Request Rejected")]
        Rejected
    }
}
