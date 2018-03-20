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
    /// This class describes the weekly reports submitted by a CRU Supervisor to the CRU Manager to relay the progress of a questionnaire's execution.
    /// </summary>

    [Table("ProgressReport")]
    public partial class ProgressReport
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PReportID { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public int CMAEmployeeID { get; set; }

        public int CSEmployeeID { get; set; }

        public int RequestID { get; set; }

        [StringLength(200)]
        public string ReportFeedback { get; set; }

        [Column(TypeName = "date")]
        public DateTime SubmissionDate { get; set; }

        public virtual CPTRequest CPTRequest { get; set; }

        public virtual CRUManager CRUManager { get; set; }

        public virtual CRUSupervisor CRUSupervisor { get; set; }
    }
}
