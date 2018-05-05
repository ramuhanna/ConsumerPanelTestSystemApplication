/*
* Description: This file contains the Question class.
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
    /// This class pertains to the individual questions contained within the questionnaires.
    /// </summary> 

    [Table("Question")]
    public partial class Question
    {
        public Question()
        {
            ContainQuestions = new HashSet<ContainQuestion>();
            EnterResults = new HashSet<EnterResult>();
            QuestionTypes = new HashSet<QuestionType>();
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionID { get; set; }

        [Required]
        [StringLength(150)]
        public string QuestionText { get; set; }

        //// Holds values of radio buttons
        //public int? SelectedAnswer { get; set; }

        // Holds values of textboxes and textarea
        public string Input { get; set; }

        //public QuestionnaireTypeEnum QuestionnaireType { get; set; }

        public virtual ICollection<ContainQuestion> ContainQuestions { get; set; }

        public virtual ICollection<EnterResult> EnterResults { get; set; }

        public virtual ICollection<QuestionType> QuestionTypes { get; set; }


    }
 
}
