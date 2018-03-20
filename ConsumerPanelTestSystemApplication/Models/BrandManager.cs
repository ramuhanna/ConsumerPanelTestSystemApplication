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
    /// This class describes the Brand Manager user who can submit internal requests and review external requests. 
    /// </summary> 

    [Table("BrandManager")]
    public partial class BrandManager : Employee
    {
        public BrandManager()
        {
            BFinalizedRequests = new HashSet<CPTRequest>();
            SubmittedRequests = new HashSet<CPTRequest>();
            BReveiewedRequests = new HashSet<CPTRequest>();
            Questionnaires = new HashSet<Questionnaire>();
        }

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int EmployeeID { get; set; }

        public BrandManagerProductDivision ProductDivision { get; set; }

        //public virtual Employee Employee { get; set; }

        public virtual ICollection<CPTRequest> BFinalizedRequests { get; set; }

        public virtual ICollection<CPTRequest> SubmittedRequests { get; set; }

        public virtual ICollection<CPTRequest> BReveiewedRequests { get; set; }

        public virtual ICollection<Questionnaire> Questionnaires { get; set; }
    }

    public enum BrandManagerProductDivision
    {
        [Display(Name = "Family Care")]
        FamilyCare,

        [Display(Name = "Feminine Care")]
        FeminineCare,

        [Display(Name = "Baby Care")]
        BabyCare,

        [Display(Name = "Household Items")]
        HouseholdItems
    }
}
