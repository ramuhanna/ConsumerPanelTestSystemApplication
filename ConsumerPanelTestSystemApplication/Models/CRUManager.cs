/*
* Description: This file contains the CRU Manager class.
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
    /// This class describes the CRU Manager user who assigns work to CRU Supervisors and verifies results, as well as reviews progress reports. 
    /// </summary>

    [Table("CRUManager")]
    public partial class CRUManager : Employee
    {
        public CRUManager()
        {
            Answers = new HashSet<Answer>();
            ProgressReports = new HashSet<ProgressReport>();
            ResponsibleFors = new HashSet<ResponsibleFor>();
        }

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int EmployeeID { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        //public virtual Employee Employee { get; set; }

        public virtual ICollection<ProgressReport> ProgressReports { get; set; }

        public virtual ICollection<ResponsibleFor> ResponsibleFors { get; set; }
    }
}
