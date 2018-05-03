/*
* Description: This controller contains the Actions of the Home page of the application.
* Author: R.M.
* Due date: 05/05/2018
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsumerPanelTestSystemApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AllUsers()
        {
            ViewBag.Message = "";
            return View();
        }

        public ActionResult CPTRequests()
        {
            if (User.IsInRole("Marketing Director"))
            {
                RedirectToAction("MarketingDirectorReviewIndex");
            }
            else if (User.IsInRole("Brand Manager, Requester"))
            {
                RedirectToAction("SubmittedRequests");
            }
            else if (User.IsInRole("CPT Coordinator"))
            {
                RedirectToAction("CPTCoordinatorIndex");
            }
            return View();
        }

        public ActionResult Questions()
        {
            ViewBag.Message = "";
            return View();
        }

    }
}