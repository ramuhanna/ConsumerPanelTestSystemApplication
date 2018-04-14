using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumerPanelTestSystemApplication.Models
{
    public class QuestionType
    {
        public int QuestionTypeId { get; set; }

        public int QuestionID { get; set; }

        public int QuestionnaireTypeID { get; set; }


        public virtual Question Question { get; set; }

        public virtual QuestionnaireType QuestionnaireType { get; set; }

    }
}