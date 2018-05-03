/*
* Description: This file contains the CRU Supervisor class and the SupervisorRegion enum.
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
            Questionnaires = new HashSet<Questionnaire>();
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

        public virtual ICollection<Questionnaire> Questionnaires { get; set; }

        public virtual ICollection<ResponsibleFor> ResponsibleFors { get; set; }
        
    }

    /// <summary>  
    /// This enum contains all the possible Regions a CRU Supervisor may be responsible for.
    /// </summary>
    
    public enum SupervisorRegion
    {
        [Display(Name = "Eastern Region")]
        EasternRegion,

        [Display(Name = "Central Region")]
        CentralRegion,

        [Display(Name = "Western Region")]
        WesternRegion,

        Other
    }
}
