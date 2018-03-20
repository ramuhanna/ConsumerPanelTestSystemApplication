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
    /// This class pertains to the individual answers inserted during data collection.   
    /// </summary> 

    [Table("Answer")]
    public partial class Answer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Answer()
        {
            EnterResults = new HashSet<EnterResult>();
        }

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AnswerID { get; set; }

        [Required]
        [StringLength(100)]
        public string AnswerText { get; set; }

        public int CMAEmployeeID { get; set; }

        public int CSEmployeeID { get; set; }

        public int CPTEmployeeID { get; set; }

        [StringLength(100)]
        public string CSComments { get; set; }

        [StringLength(100)]
        public string CMAFeedback { get; set; }

        [StringLength(100)]
        public string CPTFinalFeedback { get; set; }

        public virtual CRUManager CRUManager { get; set; }

        public virtual CPTCoordinator CPTCoordinator { get; set; }

        public virtual CRUSupervisor CRUSupervisor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnterResult> EnterResults { get; set; }
    }
}