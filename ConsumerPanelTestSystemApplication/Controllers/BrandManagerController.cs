/*
* Description: This controller contains the Index, Create, Delete, Edit and Details Actions for Brand Manager users.
* Author: R.M.
* Due date: 21/03/2018
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
    public class BrandManagerController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public BrandManagerController()
        {
        }

        public BrandManagerController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // This action is utilized in order to generate a list of Brand Manager users.
        // GET: BrandManager
        public ActionResult Index()
        {
            var users = db.BrandManagers.ToList();
            var model = new List<BrandManagerViewModel>();
            foreach (var user in users)
            {
                model.Add(new BrandManagerViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ProductDivision = user.ProductDivision
                });
            }
            return View(model);
        }

        // This action displays the details of a specific Brand Manager user.
        // GET: BrandManager/Details/5
        public ActionResult Details(int id)
        {
            // find the user in the database
            var user = UserManager.FindById(id);

            // Check if the user exists
            if (user != null)
            {
                var brandmanager = (BrandManager)user;

                BrandManagerViewModel model = new BrandManagerViewModel()
                {
                    Id = brandmanager.Id,
                    UserName = brandmanager.UserName,
                    Email = brandmanager.Email,
                    FirstName = brandmanager.FirstName,
                    LastName = brandmanager.LastName,
                    Telephone = brandmanager.Telephone,
                    TelExtension = brandmanager.TelExtension,
                    Mobile = brandmanager.PhoneNumber,
                    Country = brandmanager.Country,
                    City = brandmanager.City,
                    ProductDivision = brandmanager.ProductDivision,
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

        // GET: BrandManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // This action allows the creation of a new Brand Manager user.
        // POST: BrandManager/Create
        [HttpPost]
        public ActionResult Create(BrandManagerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var brandmanager = new BrandManager
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
                    ProductDivision = model.ProductDivision
                };

                var result = UserManager.Create(brandmanager, model.Password);

                if (result.Succeeded)
                {
                    var roleResult = UserManager.AddToRoles(brandmanager.Id, "Brand Manager");

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

        // GET: BrandManager/Edit/5
        public ActionResult Edit(int id)
        {
            var brandmanager = (BrandManager)UserManager.FindById(id);
            if (brandmanager == null)
            {
                //return HttpNotFound();
                return View("Error");
            }

            BrandManagerViewModel model = new BrandManagerViewModel
            {
                Id = brandmanager.Id,
                UserName = brandmanager.UserName,
                Email = brandmanager.Email,
                FirstName = brandmanager.FirstName,
                LastName = brandmanager.LastName,
                Telephone = brandmanager.Telephone,
                TelExtension = brandmanager.TelExtension,
                Mobile = brandmanager.PhoneNumber,
                Country = brandmanager.Country,
                City = brandmanager.City,
                ProductDivision = brandmanager.ProductDivision,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
            };

            return View(model);
        }

        // This action permits updating a Brand Manager user's details.
        // POST: BrandManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BrandManagerViewModel model)
        {
            // Exclude Password and ConfirmPassword properties from the model otherwise modelstate is false
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                var brandmanager = (BrandManager)UserManager.FindById(id);
                if (brandmanager == null)
                {
                    return HttpNotFound();
                }

                // Edit the brandmanager info
                brandmanager.UserName = model.UserName;
                brandmanager.Email = model.Email;
                brandmanager.FirstName = model.FirstName;
                brandmanager.LastName = model.LastName;
                brandmanager.Telephone = model.Telephone;
                brandmanager.TelExtension = model.TelExtension;
                brandmanager.PhoneNumber = model.Mobile;
                brandmanager.Country = model.Country;
                brandmanager.City = model.City;
                brandmanager.ProductDivision = model.ProductDivision;

                var userResult = UserManager.Update(brandmanager);

                if (userResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        // GET: BrandManager/Delete/5
        public ActionResult Delete(int id)
        {
            var brandmanager = (BrandManager)UserManager.FindById(id);
            if (brandmanager == null)
            {
                return HttpNotFound();
            }

            BrandManagerViewModel model = new BrandManagerViewModel
            {
                Id = brandmanager.Id,
                UserName = brandmanager.UserName,
                Email = brandmanager.Email,
                FirstName = brandmanager.FirstName,
                LastName = brandmanager.LastName,
                Telephone = brandmanager.Telephone,
                TelExtension = brandmanager.TelExtension,
                Mobile = brandmanager.PhoneNumber,
                Country = brandmanager.Country,
                City = brandmanager.City,
                ProductDivision = brandmanager.ProductDivision,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
            };

            return View(model);
        }

        // This action removes a Brand Manager user from the database.
        // POST: BrandManager/Delete/5
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
