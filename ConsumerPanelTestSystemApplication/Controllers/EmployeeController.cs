/*
* Description: This controller contains the Index, Create, Delete, Edit and Details Actions for Employee users.
* Author: R.M.
* Due date: 21/03/2018
*/


using AutoMapper;
using ConsumerPanelTestSystemApplication.Models;
using ConsumerPanelTestSystemApplication.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsumerPanelTestSystemApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public EmployeeController()
        {
        }

        public EmployeeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        /// <summary>  
        /// The Index action is utilized in order to generate a list of Employee users. 
        /// </summary>
     
        public ActionResult Index()
        {
            var users = db.Employees.ToList();
            var model = new List<EmployeeViewModel>();

            foreach (var user in users)
            {
                model.Add(new EmployeeViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = string.Join(" ", UserManager.GetRoles(user.Id).ToArray())
                });              
            }

            return View(model);
        }

        /// <summary>  
        /// The Details action displays the details of a specific Employee user.
        /// </summary>
    
        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            // find the user in the database
            var user = UserManager.FindById(id);

            // Check if the user exists
            if (user != null)
            {
                var employee = (Employee)user;

                EmployeeViewModel model = new EmployeeViewModel()
                {
                    Id = employee.Id,
                    UserName = employee.UserName,
                    Email = employee.Email,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Telephone = employee.Telephone,
                    TelExtension = employee.TelExtension,
                    Mobile = employee.PhoneNumber,
                    Country = employee.Country,
                    City = employee.City,
                    Type = employee.Type,
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

        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();
        }

        /// <summary>  
        /// The Create action allows the creation of a new Employee user.
        /// </summary>

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel model, params string[] roles)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
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
                    Type = model.Type          
                };

                var result = UserManager.Create(employee, model.Password);

                if (result.Succeeded)
                {
                    if (roles != null)
                    {
                        // Add user to selected roles
                        var roleResult = UserManager.AddToRoles(employee.Id, roles);

                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            // Display error messages in the view @Html.ValidationSummary()
                            ModelState.AddModelError(string.Empty, roleResult.Errors.First());

                            // Create a check list object
                            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name");

                            // Return a view if you want to see error message saved in ModelState
                            // Redirect() and RedirectToAction() clear the messages
                            return View();
                        }
                    }

                    return RedirectToAction("Index");
                }

                else
                {
                    // See above comment for ModelState errors
                    ModelState.AddModelError(string.Empty, result.Errors.First());
                    ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name");
                    return View();
                }           
            }

            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();
            
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = (Employee)UserManager.FindById(id);

            if (employee == null)
            {
                //return HttpNotFound();
                return View("Error");
            }

            EmployeeViewModel model = new EmployeeViewModel
            {
                Id = employee.Id,
                UserName = employee.UserName,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Telephone = employee.Telephone,
                TelExtension = employee.TelExtension,
                Mobile = employee.PhoneNumber,
                Country = employee.Country,
                City = employee.City,
                Type = employee.Type,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
            };

            var userRoles = UserManager.GetRoles(employee.Id);
            var rolesSelectList = db.Roles.ToList().Select(r => new SelectListItem()
            {
                Selected = userRoles.Contains(r.Name),
                Text = r.Name,
                Value = r.Name
            });

            ViewBag.RolesSelectList = rolesSelectList;

            return View(model);

        }

        /// <summary>  
        /// The Edit action permits updating an Employee user's details.
        /// </summary>
       
        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, EmployeeViewModel model, params string[] roles)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid && id != null)
            {
                // Convert id to non-nullable int
                var userId = id ?? default(int);

                var employee = (Employee)UserManager.FindById(userId);
                if (employee == null)
                {
                    return HttpNotFound();
                }

                // Edit the employee info
                employee.UserName = model.UserName;
                employee.Email = model.Email;
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.Telephone = model.Telephone;
                employee.TelExtension = model.TelExtension;
                employee.PhoneNumber = model.Mobile;
                employee.Country = model.Country;
                employee.City = model.City;
                employee.Type = model.Type;

                var userResult = UserManager.Update(employee);

                if (userResult.Succeeded)
                {
                    var userRoles = UserManager.GetRoles(employee.Id);
                    roles = roles ?? new string[] { };
                    var roleResult = UserManager.AddToRoles(employee.Id, roles.Except(userRoles).ToArray<string>());

                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                        return View();
                    }

                    roleResult = UserManager.RemoveFromRoles(employee.Id, userRoles.Except(roles).ToArray<string>());

                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                        return View();
                    }

                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            var employee = (Employee)UserManager.FindById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            EmployeeViewModel model = new EmployeeViewModel
            {
                Id = employee.Id,
                UserName = employee.UserName,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Telephone = employee.Telephone,
                TelExtension = employee.TelExtension,
                Mobile = employee.PhoneNumber,
                Country = employee.Country,
                City = employee.City,
                Type = employee.Type,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray()),
            };

            return View(model);
        }

        /// <summary>  
        /// The Delete action removes an Employee user from the database.
        /// </summary>

        // POST: Employee/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
