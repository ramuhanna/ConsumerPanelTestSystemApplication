/*
* Description: This file contains the CPT Coordinator class.
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
    /// This class describes the CPT Coordinator user who is responsible for the creation of questionnaires for CPT requests. 
    /// </summary> 

    [Table("CPTCoordinator")]
    public partial class CPTCoordinator : Employee
    {
        public CPTCoordinator()
        {
            Answers = new HashSet<Answer>();
            SelectQuestionnaires = new HashSet<SelectQuestionnaire>();
        }

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int EmployeeID { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        //public virtual Employee Employee { get; set; }

        public virtual ICollection<SelectQuestionnaire> SelectQuestionnaires { get; set; }
    }
}
