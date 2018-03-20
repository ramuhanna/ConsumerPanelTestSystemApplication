﻿/*
* Description: The Consumer Panel Test System is a Web-Based application utilized for organized and systemized market research process.
* Author: R.M.
* Due date: 20/03/2018
*/

using ConsumerPanelTestSystemApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsumerPanelTestSystemApplication.ViewModels
{
    /// <summary>
    /// CPT Request view model based on the CPT Request model and used by the CPT Request controller.
    /// </summary>

    public class CPTRequestViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Request Title")]
        public string RequestTitle { get; set; }

        //????????????
        [Column(TypeName = "date")]
        [Display(Name = "Request Submission Date")]
        public DateTime RequestDate { get; set; }

        //??????
        [Display(Name = "Request Status")]
        public RequestStatus RequestStatus { get; set; }

        [Display(Name = "Product Division")]
        public BrandManagerProductDivision ProductDivision { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Request Justification")]
        public string Justification { get; set; }

        public int MDecisionID { get; set; }

        [Display(Name = "Marketing Director")]
        public int MReviewRequest { get; set; }

        [Display(Name = "Submitted By")]
        public int? BEmployeeID { get; set; }

        [Display(Name = "Brand Manager")]
        public int BReviewRequest { get; set; }

        public int BDecisionID { get; set; }

        [Display(Name = "Submitted By")]
        public int? REmployeeID { get; set; }

        [Display(Name = "Brand Manager Decision")]
        [StringLength(100)]
        public string BDecisionMade { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BDecisionDate { get; set; }

        public Boolean BReview { get; set; }

        [Display(Name = "Marketing Director Decision")]
        [StringLength(100)]
        public string MDecision { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MDecisionDate { get; set; }

        public Boolean MReview { get; set; }
        
        [Required]
        [Display(Name = "Execution location(s)")]
        public string City { get; set; }
    }
}