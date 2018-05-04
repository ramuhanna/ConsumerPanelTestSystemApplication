/*
* Description: This file contains the Location class.
* Author: R.M.
* Due date: 05/05/2018
*/

namespace ConsumerPanelTestSystemApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>  
    /// This class contains the numerous locations at which Questionnaires may be executed.
    /// </summary> 

    [Table("Location")]
    public partial class Location
    {
        public Location()
        {
            CPTRequests = new HashSet<CPTRequest>();
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationID { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public SupervisorRegion Region { get; set; }

        public virtual ICollection<CPTRequest> CPTRequests { get; set; }
    }

}
