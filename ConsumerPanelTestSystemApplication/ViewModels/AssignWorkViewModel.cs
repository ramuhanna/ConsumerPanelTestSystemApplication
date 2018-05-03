/*
* Description: This file contains the AssignWorkViewModel class.
* Author: R.M.
* Due date: 05/05/2018
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumerPanelTestSystemApplication.ViewModels
{
    /// <summary>
    /// Assign Work view model based on the AssignWork model.
    /// </summary>

    public class AssignWorkViewModel
    {
        public int Id { get; set; }

        public int? CSEmployeeID { get; set; }
        
        public int? CMEEmployeeID { get; set; }

        public int? QuestionnaireID { get; set; }
 
        public DateTime? AssignmentDate { get; set; }
    }
}