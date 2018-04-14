using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsumerPanelTestSystemApplication.Models
{
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