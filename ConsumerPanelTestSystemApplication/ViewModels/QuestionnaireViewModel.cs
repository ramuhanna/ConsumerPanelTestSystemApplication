﻿using ConsumerPanelTestSystemApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsumerPanelTestSystemApplication.ViewModels
{
    public class QuestionnaireViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Start Date")]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Response Quantity Required")]
        public int ResponseQuantityRequired { get; set; }

        public QuestionnaireStatus Status { get; set; }

        public int? MEmployeeID { get; set; }

        public int? BEmployeeID { get; set; }

        [StringLength(200)]
        public string BComment { get; set; }

        [StringLength(200)]
        public string MComment { get; set; }

    }
}