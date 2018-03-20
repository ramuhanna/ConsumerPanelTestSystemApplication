/*
* Description: The Consumer Panel Test System is a Web-Based application utilized for organized and systemized market research process.
* Author: R.M.
* Due date: 20/03/2018
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
            ExecutionLocations = new HashSet<ExecutionLocation>();
            CPTRequests = new HashSet<CPTRequest>();
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationID { get; set; }

        [Required]
        public string City { get; set; }

        public virtual ICollection<ExecutionLocation> ExecutionLocations { get; set; }

        public virtual ICollection<CPTRequest> CPTRequests { get; set; }
    }

    //public enum ExecutionCities
    //{
    //    Jeddah,
    //    Riyadh,
    //    Dammam,
    //    Madina,
    //    Taif,
    //    Khobar,
    //    Jubail,
    //    Qassim
    //}
}
