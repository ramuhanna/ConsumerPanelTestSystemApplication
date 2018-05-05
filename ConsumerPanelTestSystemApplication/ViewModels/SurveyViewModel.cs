using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumerPanelTestSystemApplication.ViewModels
{
    public class SurveyViewModel
    {
        public SurveyViewModel()
        {
            Questions = new List<QuestionViewModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public int? SelectedAnswer { get; set; }

        public int? QuestionnaireId { get; set; }

        public int? QuestionId { get; set; }

        public decimal? Grade { get; set; }

        public List<QuestionViewModel> Questions { get; set; }
    }
}