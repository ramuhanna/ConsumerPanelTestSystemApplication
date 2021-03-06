/*
* Description: This file contains the AssignWork class.
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
    /// This class pertains to the relationship between the CRU Supervisor, CRU Member and the CPT Request assigned to the CRU Member.
    /// </summary> 


    [Table("AssignWork")]
    public partial class AssignWork
    {
        public int AssignWorkId { get; set; }
      
        public int? CSEmployeeID { get; set; }

        public int? CMEEmployeeID { get; set; }
       
        public int? QuestionnaireID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AssignmentDate { get; set; }

        public virtual CRUMember CRUMember { get; set; }

        public virtual CRUSupervisor CRUSupervisor { get; set; }

        public virtual Questionnaire Questionnaire { get; set; }
    }
}
