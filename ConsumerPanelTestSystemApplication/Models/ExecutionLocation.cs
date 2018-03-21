/*
* Description: This file contains the ExecutionLocation class.
* Author: R.M.
* Due date: 21/03/2018
*/

namespace ConsumerPanelTestSystemApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>  
    /// This class contains the specific locations at which a specific request will be executed.
    /// </summary> 
 
    [Table("ExecutionLocation")]
    public partial class ExecutionLocation
    {
        public int ExecutionLocationID { get; set; }

        public int RequestID { get; set; }

        public int LocationID { get; set; }

        public virtual CPTRequest CPTRequest { get; set; }

        public virtual Location Location { get; set; }
    }
}
