﻿/*
* Description: This file contains the CRUMemberViewModel class.
* Author: R.M.
* Due date: 05/05/2018
*/

using ConsumerPanelTestSystemApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsumerPanelTestSystemApplication.ViewModels
{
    /// <summary>
    /// CRU Member view model based on the CRU Member model and used by the CRU Member controller.
    /// </summary>

    public class CRUMemberViewModel
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Display(Name = "Telephone Extension")]
        public string TelExtension { get; set; }

        [Required]
        [Display(Name = "Mobile Phone")]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "Country")]
        public EmployeeCountry Country { get; set; }

        [Required]
        [Display(Name = "City")]
        public EmployeeCity City { get; set; }
        
        public int? CRUSupervisorId { get; set; }

        public SupervisorRegion Region { get; set; }

        public string Roles { get; set; }

        [Display(Name = "CRU Supervisor")]
        public string CRUSupervisorName { get; set; }
    }
}