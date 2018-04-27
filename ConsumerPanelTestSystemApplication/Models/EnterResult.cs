/*
* Description: This file contains the Enter Result.
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
    /// This class pertains to the functionality of the CRU Member inserting the questionnaire results into the system and database. 
    /// </summary> 
 
    [Table("EnterResult")]
    public partial class EnterResult
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CMEEmployeeID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResponseId { get; set; }

        public virtual Response Response { get; set; }

        public virtual CRUMember CRUMember { get; set; }

        public virtual Question Question { get; set; }
    }
}
