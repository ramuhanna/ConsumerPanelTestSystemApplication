using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsumerPanelTestSystemApplication.Models
{
    /// <summary>  
    /// This class pertains to the recorded responses inserted during data collection.   
    /// </summary> 

    [Table("Response")]
    public partial class Response
    {
        public Response()
        {
            EnterResults = new HashSet<EnterResult>();
        }

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResponseId { get; set; }

        [StringLength(100)]
        public string ResponseInput { get; set; }

        public int ResponseScore { get; set; }

        public int QuestionnaireId { get; set; }

        public int QuestionId { get; set; }

        public int CRUMemberId { get; set; }

        //public int CMAEmployeeID { get; set; }

        //public int CSEmployeeID { get; set; }

        //public int CPTEmployeeID { get; set; }

        //[StringLength(100)]
        //public string CSComments { get; set; }

        //[StringLength(100)]
        //public string CMAFeedback { get; set; }

        //[StringLength(100)]
        //public string CPTFinalFeedback { get; set; }

        //public virtual CRUManager CRUManager { get; set; }

        //public virtual CPTCoordinator CPTCoordinator { get; set; }

        //public virtual CRUSupervisor CRUSupervisor { get; set; }

        public virtual Questionnaire Questionnaire { get; set; }

        public virtual Question Question { get; set; }

        public virtual CRUMember CRUMember { get; set; }

        public virtual ICollection<EnterResult> EnterResults { get; set; }
    }
}
