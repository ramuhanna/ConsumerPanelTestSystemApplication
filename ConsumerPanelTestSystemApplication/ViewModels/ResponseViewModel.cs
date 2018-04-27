/*
* Description: This View Model .
* Author: R.M.
* Due date: 21/03/2018
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsumerPanelTestSystemApplication.ViewModels
{
    public class ResponseViewModel
    {
        [Key]
        public int ResponseId { get; set; }

        [Required]
        [StringLength(100)]
        public string ResponseInput { get; set; }

        [Required]
        public int ResponseScore { get; set; }

        public int QuestionnaireId { get; set; }

        public int QuestionId { get; set; }

        public int CRUMemberId { get; set; }
    }
}