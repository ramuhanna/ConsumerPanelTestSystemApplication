/*
* Description: This controller contains the Index, Create, Delete, Edit and Details Actions for CRU Member users.
* Author: R.M.
* Due date: 05/05/2018
*/


using ConsumerPanelTestSystemApplication.Models;
using ConsumerPanelTestSystemApplication.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsumerPanelTestSystemApplication.Controllers
{
    public class CRUMemberController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public CRUMemberController()
        {
        }

        public CRUMemberController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public object UserManeger { get; private set; }

        /// <summary>  
        /// The Index action is utilized in order to generate a list of CRU Member users. 
        /// </summary>
        /// <returns>Index View</returns>
        // GET: CRUMember
        public ActionResult Index()
        {
            var users = db.CRUMembers.ToList();
            var model = new List<CRUMemberViewModel>();
            foreach (var user in users)
            {
                model.Add(new CRUMemberViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Region = user.Region,
                    CRUSupervisorName = db.CRUSupervisors.Find(user.CRUSupervisorId).FullName,
                });
            }
            return View(model);
        }

        /// <summary>  
        /// The Details action displays the details of a specific CRU Member user.
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <returns>Details View</returns>
        // GET: CRUMember/Details/5
        public ActionResult Details(int id)
        {
            // find the user in the database
            var user = UserManager.FindById(id);

            // Check if the user exists
            if (user != null)
            {
                var crumember = (CRUMember)user;

                CRUMemberViewModel model = new CRUMemberViewModel()
                {
                    Id = crumember.Id,
                    UserName = crumember.UserName,
                    Email = crumember.Email,
                    FirstName = crumember.FirstName,
                    LastName = crumember.LastName,
                    Telephone = crumember.Telephone,
                    TelExtension = crumember.TelExtension,
                    Mobile = crumember.PhoneNumber,
                    Country = crumember.Country,
                    City = crumember.City,
                    Region = crumember.Region,
                    CRUSupervisorId = crumember.CRUSupervisorId,
                    CRUSupervisorName = db.CRUSupervisors.Find(crumember.CRUSupervisorId).FullName,

                    Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
                };

                var result = db.Employees.Where(s => s.Id == 1);

                return View(model);
            }
            else
            {
                // Customize the error view: /Views/Shared/Error.cshtml
                return View("Error");
            }
        }

        /// <summary>  
        /// The Create action allows the creation of a new CRU Member user.
        /// </summary>
        /// <param></param>
        /// <returns>Create View</returns>
        // GET: CRUMember/Create
        public ActionResult Create()
        {          
            return View();
        }

        /// <summary>  
        /// The Create action allows the creation of a new CRU Member user.
        /// </summary>
        /// <param name="model">CRUMemberViewModel as a parameter</param>
        /// <returns>Create View</returns>
        // POST: CRUMember/Create
        [HttpPost]
        public ActionResult Create(CRUMemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                var crumember = new CRUMember
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Telephone = model.Telephone,
                    TelExtension = model.TelExtension,
                    PhoneNumber = model.Mobile,
                    Country = model.Country,
                    City = model.City,
                    Region = model.Region,
                };

                var result = UserManager.Create(crumember, model.Password);

                if (result.Succeeded)
                {
                    var roleResult = UserManager.AddToRoles(crumember.Id, "CRU Member");

                    if (roleResult.Succeeded)
                    {                        
                        return RedirectToAction("Index");
                    }
                }

                var supervisorid = db.CRUSupervisors.Where(s => s.Region == crumember.Region).Select(s => s.Id).First();
                crumember.CRUSupervisorId = supervisorid;

                var userResult = UserManager.Update(crumember);
                db.SaveChanges();
            }
            else
            {
                return View("Error");
            }

            return View();
        }

        /// <summary>  
        /// The Edit action permits updating a CRU Member user's details.
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <returns>Edit View</returns>
        // GET: CRUMember/Edit/5
        public ActionResult Edit(int id)
        {
            var crumember = (CRUMember)UserManager.FindById(id);
            if (crumember == null)
            {
                //return HttpNotFound();
                return View("Error");
            }

            CRUMemberViewModel model = new CRUMemberViewModel
            {
                Id = crumember.Id,
                UserName = crumember.UserName,
                Email = crumember.Email,
                FirstName = crumember.FirstName,
                LastName = crumember.LastName,
                Telephone = crumember.Telephone,
                TelExtension = crumember.TelExtension,
                Mobile = crumember.PhoneNumber,
                Country = crumember.Country,
                City = crumember.City,
                Region = crumember.Region,
                CRUSupervisorId = crumember.CRUSupervisorId,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
            };

            ViewBag.CRUSupervisorId = new SelectList(db.CRUSupervisors, "Id", "UserName", "Region");
            return View(model);
        }

        /// <summary>  
        /// The Edit action permits updating a CRU Member user's details.
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <param name="model">CRUMemberViewModel as a parameter</param>
        /// <returns>Edit View</returns>
        // POST: CRUMember/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CRUMemberViewModel model)
        {
            // Exclude Password and ConfirmPassword properties from the model otherwise modelstate is false
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                var crumember = (CRUMember)UserManager.FindById(id);
                if (crumember == null)
                {
                    return HttpNotFound();
                }

                // Edit the crusupervisor info
                crumember.UserName = model.UserName;
                crumember.Email = model.Email;
                crumember.FirstName = model.FirstName;
                crumember.LastName = model.LastName;
                crumember.Telephone = model.Telephone;
                crumember.TelExtension = model.TelExtension;
                crumember.PhoneNumber = model.Mobile;
                crumember.Country = model.Country;
                crumember.City = model.City;
                crumember.Region = model.Region;
                crumember.CRUSupervisorId = model.CRUSupervisorId;

                var userResult = UserManager.Update(crumember);

                if (userResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.CRUSupervisorId = new SelectList(db.CRUSupervisors, "Id", "UserName", "Region");
            return View();
        }

        /// <summary>  
        /// The Delete action removes a CRU Member user from the database.
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <returns>Delete View</returns>
        // GET: CRUMember/Delete/5
        public ActionResult Delete(int id)
        {
            var crumember = (CRUMember)UserManager.FindById(id);
            if (crumember == null)
            {
                return HttpNotFound();
            }

            CRUMemberViewModel model = new CRUMemberViewModel
            {
                Id = crumember.Id,
                UserName = crumember.UserName,
                Email = crumember.Email,
                FirstName = crumember.FirstName,
                LastName = crumember.LastName,
                Telephone = crumember.Telephone,
                TelExtension = crumember.TelExtension,
                Mobile = crumember.PhoneNumber,
                Country = crumember.Country,
                City = crumember.City,
                Region = crumember.Region,
                //CRUSupervisor = crumember.AssignedCRUSupervisor.UserName,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
            };

            return View(model);
        }

        /// <summary>  
        /// The Delete action removes a CRU Member user from the database.
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <returns>Delete View</returns>
        // POST: CRUMember/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                var user = UserManager.FindById(id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                var result = UserManager.Delete(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        
        }
    }
}
