/*
* Description: This file contains the Select Questionnaire class.
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
    /// This class pertains to the functionality of the CPT Coordinator selecting a Questionnaire to be executed based on a CPT Request. 
    /// </summary> 

    [Table("SelectQuestionnaire")]
    public partial class SelectQuestionnaire
    {
        [Key]
        public int SelectQuestionnaireId { get; set; }

        public int? RequestID { get; set; }

        public int? QuestionnaireID { get; set; }

        public int? CPTEmployeeID { get; set; }

        public virtual CPTCoordinator CPTCoordinator { get; set; }

        public virtual CPTRequest CPTRequest { get; set; }

        public virtual Questionnaire Questionnaire { get; set; }
    }
}
