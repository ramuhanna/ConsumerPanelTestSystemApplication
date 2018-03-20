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
    /// This class describes the Marketing Director user responsible for reviewing all requests and final decision making. 
    /// </summary>

    [Table("MarketingDirector")]
    public partial class MarketingDirector : Employee
    {
        public MarketingDirector()
        {
            MFinalizedRequests = new HashSet<CPTRequest>();
            MReveiewedRequests = new HashSet<CPTRequest>();
            Questionnaires = new HashSet<Questionnaire>();
        }

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int EmployeeID { get; set; }

        public virtual ICollection<CPTRequest> MFinalizedRequests { get; set; }

        public virtual ICollection<CPTRequest> MReveiewedRequests { get; set; }

        //public virtual Employee Employee { get; set; }

        public virtual ICollection<Questionnaire> Questionnaires { get; set; }
    }
}
