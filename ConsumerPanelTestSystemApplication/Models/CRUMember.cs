/*
* Description: This file contains the CRU Member class.
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
    /// This class describes the CRU Member user who is responsible for the entry and modification(when necessary) of questionnaire results. 
    /// </summary>  

    [Table("CRUMember")]
    public partial class CRUMember : Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CRUMember()
        {
            AssignWorks = new HashSet<AssignWork>();
            EnterResults = new HashSet<EnterResult>();
        }

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int EmployeeID { get; set; }

        public int? CRUSupervisorId { get; set; }

        public SupervisorRegion Region { get; set; }

        public virtual ICollection<AssignWork> AssignWorks { get; set; }

        public virtual CRUSupervisor AssignedCRUSupervisor { get; set; }

        //public virtual Employee Employee { get; set; }

        public virtual ICollection<EnterResult> EnterResults { get; set; }
    }
}
