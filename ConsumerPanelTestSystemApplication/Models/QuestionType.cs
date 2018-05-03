/*
* Description: This file contains the QuestionType class.
* Author: R.M.
* Due date: 05/05/2018
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsumerPanelTestSystemApplication.Models
{
    /// <summary>  
    /// This class includes the question added to the database as well as all the questionnairetypes it is included in.
    /// </summary>

    [Table("QuestionType")]
    public class QuestionType
    {
        public int QuestionTypeId { get; set; }

        public int QuestionID { get; set; }

        public int QuestionnaireTypeID { get; set; }

        public virtual Question Question { get; set; }

        public virtual QuestionnaireType QuestionnaireType { get; set; }

    }
}