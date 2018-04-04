/*
* Description: This file contains the Employee class and the EmployeeType, EmployeeCountry and EmployeeCity enums.
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
    /// This class contains all the information of an employee, and is the base class of all the employee subtype classes.
    /// </summary>

    [Table("Employee")]
    public partial class Employee : ApplicationUser
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int EmployeeID { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string UserName { get; set; }

        //[Required]
        //[StringLength(15)]
        //public string Password { get; set; }

        [Required]
        [StringLength(15)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(10)]
        public string Telephone { get; set; }

        [StringLength(4)]
        public string TelExtension { get; set; }

        //[Required]
        //[StringLength(10)]
        //public string Mobile { get; set; }

        //[Required]
        //[StringLength(30)]
        //public string Email { get; set; }

        [Required]
        public EmployeeCountry Country { get; set; }

        [Required]
        public EmployeeCity City { get; set; }

        public EmployeeType Type { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        //public virtual BrandManager BrandManager { get; set; }

        //public virtual CPTCoordinator CPTCoordinator { get; set; }

        //public virtual CRUManager CRUManager { get; set; }

        //public virtual CRUMember CRUMember { get; set; }

        //public virtual CRUSupervisor CRUSupervisor { get; set; }

        //public virtual MarketingDirector MarketingDirector { get; set; }

        //public virtual Requester Requester { get; set; }

        public virtual ICollection<CPTRequest> SubmittedCPTRequests { get; set; }
    }

    /// <summary>  
    /// This enum contains the possible Employee Types an Employee may belong to.
    /// </summary>
    public enum EmployeeType
    {
        [Display(Name = "Marketing Director")]
        MarketingDirector,

        [Display(Name = "CPT Coordinator")]
        CPTCoornidator,

        [Display(Name = "CRU Manager")]
        CRUManager,

        [Display(Name = "CRU Member")]
        CRUMember,

        [Display(Name = "CRU Supervisor")]
        CRUSupervisor,

        [Display(Name = "Brand Manager")]
        BrandManager,

        Requester,
    }

    /// <summary>  
    /// This enum contains the possible Countries an Employee may reside in.
    /// </summary>
    public enum EmployeeCountry
    {
        [Display(Name = "Saudi Arabia")]
        SaudiArabia,

        Lebanon,

        Other
    }

    /// <summary>  
    /// This enum contains the possible Cities an Employee may reside in.
    /// </summary>
    public enum EmployeeCity
    {
        Jeddah,
        Riyadh,
        Dammam,
        Jubail,
        Yanbu,
        Madina,
        Makkah,
        Taif,
        Beirut,
        Other
    }
}
