/*
* Description: This controller contains the Index, Create, Delete, Edit and Details Actions for CRU Member users.
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

        // This action is utilized in order to generate a list of CRU Member users.
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
                    //CRUSupervisorId = user.CRUSupervisorId
                });
            }
            return View(model);
        }

        // This action displays the details of a specific CRU Member user.
        // GET: CRUMember/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CRUMember/Create
        public ActionResult Create()
        {
            return View();
        }

        // This action allows the creation of a new CRU Member user.
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
                    //CRUSupervisorId = model.CRUSupervisorId
                };

                var result = UserManager.Create(crumember, model.Password);

                if (result.Succeeded)
                {
                    var roleResult = UserManager.AddToRoles(crumember.Id, "CRU Member");

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
                CRUSupervisorId = crumember.CRUSupervisorId,                
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
            };

            return View(model);
        }

        // This action permits updating a CRU Member user's details.
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

                // Edit the requester info
                crumember.UserName = model.UserName;
                crumember.Email = model.Email;
                crumember.FirstName = model.FirstName;
                crumember.LastName = model.LastName;
                crumember.Telephone = model.Telephone;
                crumember.TelExtension = model.TelExtension;
                crumember.PhoneNumber = model.Mobile;
                crumember.Country = model.Country;
                crumember.City = model.City;
                crumember.CRUSupervisorId = model.CRUSupervisorId;

                var userResult = UserManager.Update(crumember);

                if (userResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        // GET: CRUMember/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // This action removes a CRU Member user from the database.
        // POST: CRUMember/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
