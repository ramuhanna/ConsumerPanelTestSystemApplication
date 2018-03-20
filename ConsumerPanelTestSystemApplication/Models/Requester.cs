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
    /// This class describes authorized external requesters from any departments within the organization aside from the Marketing Division.
    /// </summary>

    [Table("Requester")]
    public partial class Requester : Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CPTRequest> CPTRequests { get; set; }

        //public virtual Employee Employee { get; set; }
    }

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
