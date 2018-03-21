/*
* Description: This file contains the Question class.
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
    /// This class pertains to the individual questions contained within the questionnaires.
    /// </summary> 

    [Table("Question")]
    public partial class Question
    {
        public Question()
        {
            ContainQuestions = new HashSet<ContainQuestion>();
            EnterResults = new HashSet<EnterResult>();
            QuestionnaireTypes = new HashSet<QuestionnaireType>();
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionID { get; set; }

        [Required]
        [StringLength(100)]
        public string QuestionText { get; set; }

        public virtual ICollection<ContainQuestion> ContainQuestions { get; set; }

        public virtual ICollection<EnterResult> EnterResults { get; set; }

        public virtual ICollection<QuestionnaireType> QuestionnaireTypes { get; set; }
    }
}
