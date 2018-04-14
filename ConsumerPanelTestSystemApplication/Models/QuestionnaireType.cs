/*
* Description: This file contains the Questionnaire Type class and the QuestionnaireType enum.
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
    /// This class pertains to the selection of the questionnaire types to which a single question belongs.
    /// </summary> 

    [Table("QuestionnaireType")]
    public partial class QuestionnaireType
    {
        public QuestionnaireType()
        {
            QuestionTypes = new HashSet<QuestionType>();
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionnaireTypeID { get; set; }

        public string QuestionnaireTypeName { get; set; }

        public virtual ICollection<QuestionType> QuestionTypes { get; set; }
    }





    //}
}
