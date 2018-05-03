/*
* Description: This controller contains the Index, Create, Delete, Edit and Details Actions for Requester users.
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
    public class RequesterController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public RequesterController()
        {
        }

        public RequesterController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        /// The Index action is utilized in order to generate a list of Requester users. 
        /// </summary>
        /// <returns>MarketingDirectorReviewIndex View</returns>
        // GET: Requester
        public ActionResult Index()
        {
            var users = db.Requesters.ToList();
            var model = new List<RequesterViewModel>();
            foreach (var user in users)
            {
                model.Add(new RequesterViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Department = user.Department,
                    Position = user.Position
                });
            }
            return View(model);
        }

        /// <summary>  
        /// The Details action displays the details of a specific Requester user.
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <returns>Details View</returns>
        // GET: Requester/Details/5
        public ActionResult Details(int id)
        {
            // find the user in the database
            var user = UserManager.FindById(id);

            // Check if the user exists
            if (user != null)
            {
                var requester = (Requester)user;

                RequesterViewModel model = new RequesterViewModel()
                {
                    Id = requester.Id,
                    UserName = requester.UserName,
                    Email = requester.Email,
                    FirstName = requester.FirstName,
                    LastName = requester.LastName,
                    Telephone = requester.Telephone,
                    TelExtension = requester.TelExtension,
                    Mobile = requester.PhoneNumber,
                    Country = requester.Country,
                    City = requester.City,
                    Department = requester.Department,
                    Position = requester.Position,
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
        /// The Create action allows the creation of a new Requester user.
        /// </summary>
        /// <param></param>
        /// <returns>Create View</returns>
        // GET: Requester/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>  
        /// The Create action allows the creation of a new Requester user.
        /// </summary>
        /// <param name="model">RequesterViewModel as a parameter</param>
        /// <returns>Create View</returns>
        // POST: Requester/Create
        [HttpPost]
        public ActionResult Create(RequesterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var requester = new Requester
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
                    Department = model.Department,
                    Position = model.Position
                };

                var result = UserManager.Create(requester, model.Password);

                if (result.Succeeded)
                {
                    var roleResult = UserManager.AddToRoles(requester.Id, "Requester");

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
        /// The Edit action permits updating a Requester user's details.
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <returns>Edit View</returns>
        // GET: Requester/Edit/5
        public ActionResult Edit(int id)
        {
            var requester = (Requester)UserManager.FindById(id);
            if (requester == null)
            {
                //return HttpNotFound();
                return View("Error");
            }

            RequesterViewModel model = new RequesterViewModel
            {
                Id = requester.Id,
                UserName = requester.UserName,
                Email = requester.Email,
                FirstName = requester.FirstName,
                LastName = requester.LastName,
                Telephone = requester.Telephone,
                TelExtension = requester.TelExtension,
                Mobile = requester.PhoneNumber,
                Country = requester.Country,
                City = requester.City,
                Department = requester.Department,
                Position = requester.Position,                
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
            };

            return View(model);
        }

        /// <summary>  
        /// The Edit action permits updating a Requester user's details.
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <param name="model">RequesterViewModel as a parameter</param>
        /// <returns>Edit View</returns>
        // POST: Requester/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RequesterViewModel model)
        {
            // Exclude Password and ConfirmPassword properties from the model otherwise modelstate is false
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                var requester = (Requester)UserManager.FindById(id);
                if (requester == null)
                {
                    return HttpNotFound();
                }

                // Edit the requester info
                requester.UserName = model.UserName;
                requester.Email = model.Email;
                requester.FirstName = model.FirstName;
                requester.LastName = model.LastName;
                requester.Telephone = model.Telephone;
                requester.TelExtension = model.TelExtension;
                requester.PhoneNumber = model.Mobile;
                requester.Country = model.Country;
                requester.City = model.City;
                requester.Department = model.Department;
                requester.Position = model.Position;

                var userResult = UserManager.Update(requester);

                if (userResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        /// <summary>  
        /// The Delete action removes a Requester user from the database.
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <returns>Delete View</returns>
        // GET: Requester/Delete/5
        public ActionResult Delete(int id)
        {
            var requester = (Requester)UserManager.FindById(id);
            if (requester == null)
            {
                return HttpNotFound();
            }

            RequesterViewModel model = new RequesterViewModel
            {
                Id = requester.Id,
                UserName = requester.UserName,
                Email = requester.Email,
                FirstName = requester.FirstName,
                LastName = requester.LastName,
                Telephone = requester.Telephone,
                TelExtension = requester.TelExtension,
                Mobile = requester.PhoneNumber,
                Country = requester.Country,
                City = requester.City,
                Department = requester.Department,
                Position = requester.Position,                
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
            };

            return View(model);
        }

        /// <summary>  
        /// The Delete action removes a Requester user from the database.
        /// </summary>
        /// <param name="id">Employee Id as a parameter</param>
        /// <returns>Delete View</returns>
        // POST: Requester/Delete/5
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
