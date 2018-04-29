using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsumerPanelTestSystemApplication.ViewModels
{
    public class SelectQuestionnaireViewModel
    {
        public int Id { get; set; }

        public int? RequestID { get; set; }

        public int? QuestionnaireID { get; set; }

        public int? CPTEmployeeID { get; set; }
    }
}