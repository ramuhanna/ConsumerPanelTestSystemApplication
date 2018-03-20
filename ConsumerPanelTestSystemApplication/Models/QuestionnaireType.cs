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
    /// This class pertains to the selection of the questionnaire types to which a single question belongs.
    /// </summary> 

    [Table("QuestionnaireType")]
    public partial class QuestionnaireType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionnaireTypeID { get; set; }

        public int QuestionID { get; set; }

        [Column("QuestionnaireType")]
        public int QuestionnaireType1 { get; set; }

        public virtual Question Question { get; set; }
    }

    public enum QuestionnaireTypeEnum
    {
        [Display(Name = "Performance Tracking")]
        PerformanceTracking,

        [Display(Name = "Product Development")]
        ProductDevelopment,

        [Display(Name = "Tracking Tests")]
        TrackingTests,

        [Display(Name = "Product Assessment")]
        ProductAssessment
    }
}