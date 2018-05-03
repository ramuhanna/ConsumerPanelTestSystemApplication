/*
* Description: This file contains the Survey class.
* Author: R.M.
* Due date: 05/05/2018
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumerPanelTestSystemApplication.Models
{
    public class Survey
    {
        /// <summary>  
        /// This class describes the survey generated through according to the Questionnaire Type selected for a questionnaire.
        /// </summary>

        public Survey()
        {
            EnterResults = new HashSet<EnterResult>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string ResponseInput { get; set; }

        public int? ResponseScore { get; set; }

        public int? QuestionnaireId { get; set; }

        public int QuestionId { get; set; }

        public virtual Questionnaire Questionnaire { get; set; }

        public virtual Question Question { get; set; }

        public virtual ICollection<EnterResult> EnterResults { get; set; }


    }
}