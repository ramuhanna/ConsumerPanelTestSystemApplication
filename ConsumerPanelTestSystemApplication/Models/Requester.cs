/*
* Description: This file contains the Requester class and the Department enum.
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
    /// This class describes authorized external requesters from any departments within the organization aside from the Marketing Division.
    /// </summary>

    [Table("Requester")]
    public partial class Requester : Employee
    {
        public Requester()
        {
            CPTRequests = new HashSet<CPTRequest>();
        }

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int EmployeeID { get; set; }

        public Department Department { get; set; }

        [Required]
        [StringLength(50)]
        public string Position { get; set; }

        public virtual ICollection<CPTRequest> CPTRequests { get; set; }

        //public virtual Employee Employee { get; set; }
    }

    /// <summary>  
    /// This enum contains the possible Departments a Requester user may belong to.
    /// </summary> 
    /// 
    public enum Department
    {
        Marketing,

        [Display(Name = "Research and Development")]
        RD,

        [Display(Name = "Quality Assurance and Control")]
        QAQC,

        Production,

        Sales,

        Other
    }
}
