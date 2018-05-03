/*
* Description: This controller contains the Index, Create, Delete, Edit and Details Actions for CRU Supervisor users.
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
    public class CRUSupervisorController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public CRUSupervisorController()
        {
        }

        public CRUSupervisorController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        /// The Index action is utilized in order to generate a list of CRU Supervisor users. 
        /// </summary>
        /// <returns>MarketingDirectorReviewIndex View</returns>
        // GET: CRUSupervisor
        public ActionResult Index()
        {
            var users = db.CRUSupervisors.ToList();
            var model = new List<CRUSupervisorViewModel>();
            foreach (var user in users)
            {
                model.Add(new CRUSupervisorViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Region = user.Region
                });
            }
            return View(model);
        }

        /// <summary>  
        /// The Details action displays the details of a specific CRU Supervisor user.
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <returns>Details View</returns>
        // GET: CRUSupervisor/Details/5
        public ActionResult Details(int id)
        {
            // find the user in the database
            var user = UserManager.FindById(id);

            // Check if the user exists
            if (user != null)
            {
                var crusupervisor = (CRUSupervisor)user;

                CRUSupervisorViewModel model = new CRUSupervisorViewModel()
                {
                    Id = crusupervisor.Id,
                    UserName = crusupervisor.UserName,
                    Email = crusupervisor.Email,
                    FirstName = crusupervisor.FirstName,
                    LastName = crusupervisor.LastName,
                    Telephone = crusupervisor.Telephone,
                    TelExtension = crusupervisor.TelExtension,
                    Mobile = crusupervisor.PhoneNumber,
                    Country = crusupervisor.Country,
                    City = crusupervisor.City,
                    Region = crusupervisor.Region,
                    Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
                };

                return View(model);
            }
            else
            {
                // Customize the error view: /Views/Shared/Error.cshtml
                return View("Error");
            }
        }

        /// <summary>  
        /// The Create action allows the creation of a new CRU Supervisor user.
        /// </summary>
        /// <param></param>
        /// <returns>Create View</returns>
        // GET: CRUSupervisor/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>  
        /// The Create action allows the creation of a new CRU Supervisor user.
        /// </summary>
        /// <param name="model">CRUSupervisorViewModel as a parameter</param>
        /// <returns>Create View</returns>
        // POST: CRUSupervisor/Create
        [HttpPost]
        public ActionResult Create(CRUSupervisorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var crusupervisor = new CRUSupervisor
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
                    Region = model.Region
                };

                var result = UserManager.Create(crusupervisor, model.Password);

                if (result.Succeeded)
                {
                    var roleResult = UserManager.AddToRoles(crusupervisor.Id, "CRU Supervisor");

                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        /// <summary>  
        /// The Edit action permits updating a CRU Supervisor user's details.
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <returns>Edit View</returns>
        // GET: CRUSupervisor/Edit/5
        public ActionResult Edit(int id)
        {
            var crusupervisor = (CRUSupervisor)UserManager.FindById(id);
            if (crusupervisor == null)
            {
                //return HttpNotFound();
                return View("Error");
            }

            CRUSupervisorViewModel model = new CRUSupervisorViewModel
            {
                Id = crusupervisor.Id,
                UserName = crusupervisor.UserName,
                Email = crusupervisor.Email,
                FirstName = crusupervisor.FirstName,
                LastName = crusupervisor.LastName,
                Telephone = crusupervisor.Telephone,
                TelExtension = crusupervisor.TelExtension,
                Mobile = crusupervisor.PhoneNumber,
                Country = crusupervisor.Country,
                City = crusupervisor.City,
                Region = crusupervisor.Region,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
            };

            return View(model);
        }

        /// <summary>  
        /// The Edit action permits updating a CRU Supervisor user's details.
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <param name="model">CRUSupervisorViewModel as a parameter</param>
        /// <returns>Edit View</returns>
        // POST: CRUSupervisor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CRUSupervisorViewModel model)
        {
            // Exclude Password and ConfirmPassword properties from the model otherwise modelstate is false
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                var crusupervisor = (CRUSupervisor)UserManager.FindById(id);
                if (crusupervisor == null)
                {
                    return HttpNotFound();
                }

                // Edit the crusupervisor info
                crusupervisor.UserName = model.UserName;
                crusupervisor.Email = model.Email;
                crusupervisor.FirstName = model.FirstName;
                crusupervisor.LastName = model.LastName;
                crusupervisor.Telephone = model.Telephone;
                crusupervisor.TelExtension = model.TelExtension;
                crusupervisor.PhoneNumber = model.Mobile;
                crusupervisor.Country = model.Country;
                crusupervisor.City = model.City;
                crusupervisor.Region = model.Region;

                var userResult = UserManager.Update(crusupervisor);

                if (userResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        /// <summary>  
        /// The Delete action removes a CRU Supervisor user from the database.
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <returns>Delete View</returns>
        // GET: CRUSupervisor/Delete/5
        public ActionResult Delete(int id)
        {
            var crusupervisor = (CRUSupervisor)UserManager.FindById(id);
            if (crusupervisor == null)
            {
                return HttpNotFound();
            }

           CRUSupervisorViewModel model = new CRUSupervisorViewModel
            {
                Id = crusupervisor.Id,
                UserName = crusupervisor.UserName,
                Email = crusupervisor.Email,
                FirstName = crusupervisor.FirstName,
                LastName = crusupervisor.LastName,
                Telephone = crusupervisor.Telephone,
                TelExtension = crusupervisor.TelExtension,
                Mobile = crusupervisor.PhoneNumber,
                Country = crusupervisor.Country,
                City = crusupervisor.City,
                Region = crusupervisor.Region,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
            };

            return View(model);
        }

        /// <summary>  
        /// The Delete action removes a CRU Supervisor user from the database.
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <returns>Delete View</returns>
        // POST: CRUSupervisor/Delete/5
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
