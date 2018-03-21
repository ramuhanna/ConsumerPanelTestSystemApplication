/*
* Description: This file contains the LocationViewModel class.
* Author: R.M.
* Due date: 21/03/2018
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsumerPanelTestSystemApplication.ViewModels
{
    /// <summary>
    /// Location view model based on the Location model and used by the Location controller.
    /// </summary>

    public class LocationViewModel
    {
        public int Id { get; set; }

        [Required]
        public string City { get; set; }
    }
}