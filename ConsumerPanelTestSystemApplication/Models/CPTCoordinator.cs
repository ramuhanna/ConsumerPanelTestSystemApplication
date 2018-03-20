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
    /// This class describes the CPT Coordinator user who is responsible for the creation of questionnaires for CPT requests. 
    /// </summary> 

    [Table("CPTCoordinator")]
    public partial class CPTCoordinator : Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CPTCoordinator()
        {
            Answers = new HashSet<Answer>();
            SelectQuestionnaires = new HashSet<SelectQuestionnaire>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SelectQuestionnaire> SelectQuestionnaires { get; set; }
    }
}
