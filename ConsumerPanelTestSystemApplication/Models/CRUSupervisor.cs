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
    /// This class describes the CRU Supervisor user who assigns work to CRU Members and verifies results. 
    /// </summary>

    [Table("CRUSupervisor")]
    public partial class CRUSupervisor : Employee
    {
        public CRUSupervisor()
        {
            Answers = new HashSet<Answer>();
            AssignWorks = new HashSet<AssignWork>();
            CRUMembers = new HashSet<CRUMember>();
            ProgressReports = new HashSet<ProgressReport>();
            ResponsibleFors = new HashSet<ResponsibleFor>();
        }

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int EmployeeID { get; set; }

        public SupervisorRegion Region { get; set; }
        

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<AssignWork> AssignWorks { get; set; }

        public virtual ICollection<CRUMember> CRUMembers { get; set; }

        //public virtual Employee Employee { get; set; }

        public virtual ICollection<ProgressReport> ProgressReports { get; set; }

        public virtual ICollection<ResponsibleFor> ResponsibleFors { get; set; }
        
    }

    public enum SupervisorRegion
    {
        Jeddah,
        Riyadh,
        Sharqiyah
    }
}
