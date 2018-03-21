/*
* Description: This file contains the Contain Question class.
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
    /// This class contains the questions contained in each questionnaire.
    /// </summary>


    [Table("ContainQuestion")]
    public partial class ContainQuestion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ContainQuestionID { get; set; }

        public int QuestionID { get; set; }

        public int QuestionnaireID { get; set; }

        public virtual Questionnaire Questionnaire { get; set; }

        public virtual Question Question { get; set; }
    }
}
