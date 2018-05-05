/*
* Description: This controller contains the actions to create, index, detail and review questionnaires. 
* Author: R.M.
* Due date: 05/05/2018
*/

using ConsumerPanelTestSystemApplication.Models;
using ConsumerPanelTestSystemApplication.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ConsumerPanelTestSystemApplication.Controllers
{
    public class QuestionnaireController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>  
        /// The Index action is utilized in order to generate a list of the questionnaires. 
        /// </summary>
        /// <returns>Questionnaire, Index view</returns>
        [Authorize(Roles = "CPT Coordinator")]
        // GET: Questionnaire
        public ActionResult Index()
        {

            var questionnaires = db.Questionnaires.ToList();

            var model = new List<QuestionnaireViewModel>();
            foreach (var item in questionnaires)
            {
                model.Add(new QuestionnaireViewModel
                {
                    Id = item.QuestionnaireID,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    ResponseQuantityRequired = item.ResponseQuantityRequired,
                    Status = item.Status,
                    QuestionnaireTypeName = db.QuestionnaireTypes.Where(q => q.QuestionnaireTypeID == item.QuestionnaireTypeId).Select(q => q.QuestionnaireTypeName).First()
                });
            };

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View(model);
        }

        /// <summary>  
        /// The CRUSupervisorIndex action is utilized in order to generate a list of the approved questionnaires to be managed by the CRU Supervisor. 
        /// </summary>
        /// <returns>CRUSupervisorIndex View</returns>
        [Authorize(Roles = "CRU Supervisor")]
        // GET: CRUSupervisorIndex
        public ActionResult CRUSupervisorIndex()
        {
            var user = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id;
            var questionnaires = db.Questionnaires.Where(q => q.Status == QuestionnaireStatus.QuestionnaireExecution && q.CRUSEmployeeID == user).ToList();
       
            var model = new List<QuestionnaireViewModel>();
            foreach (var item in questionnaires)
            {
                model.Add(new QuestionnaireViewModel
                {
                    Id = item.QuestionnaireID,
                    Status = item.Status,
                    QuestionnaireTypeId = item.QuestionnaireTypeId,
                    QuestionnaireTypeName = db.QuestionnaireTypes.Where(q => q.QuestionnaireTypeID == item.QuestionnaireTypeId).Select(q => q.QuestionnaireTypeName).First(),
                    QuestionnaireTitle = item.QuestionnaireTitle,
                    CRUMEmployeeID = item.CRUMEmployeeID
                });

                var sq = db.SelectQuestionnaires.Where(s => s.QuestionnaireID == item.QuestionnaireID).FirstOrDefault();
                var request = db.CPTRequests.Find(sq.RequestID);              

            }

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View(model);
        }


        /// <summary>  
        /// The CRUMemberIndex action is utilized in order to generate a list of the approved questionnaires assigned to the CRU Member. 
        /// </summary>
        /// <returns>CRUSupervisorIndex View</returns>
        [Authorize(Roles = "CRU Member")]
        // GET: CRUMemberIndex
        public ActionResult CRUMemberIndex()
        {
            var questionnaires = db.Questionnaires.Where(q => q.Status == QuestionnaireStatus.QuestionnaireExecution).ToList();

            var model = new List<QuestionnaireViewModel>();
            foreach (var item in questionnaires)
            {
                model.Add(new QuestionnaireViewModel
                {
                    Id = item.QuestionnaireID,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    ResponseQuantityRequired = item.ResponseQuantityRequired,
                    Status = item.Status,
                    QuestionnaireTypeId = item.QuestionnaireTypeId,
                    QuestionnaireTypeName = db.QuestionnaireTypes.Where(q => q.QuestionnaireTypeID == item.QuestionnaireTypeId).Select(q => q.QuestionnaireTypeName).First(),
                    QuestionnaireTitle = item.QuestionnaireTitle
                });

                var sq = db.SelectQuestionnaires.Where(s => s.QuestionnaireID == item.QuestionnaireID).FirstOrDefault();
                var request = db.CPTRequests.Find(sq.RequestID);

            }

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View(model);
        }



        /// <summary>  
        /// The Details action allows the CPT Coordinator to view the details of a specific questionnaire. 
        /// </summary>
        /// <param name="id">QuestionnaireId as parameter</param>
        /// <returns>Questionnaire, Details view</returns>
        [Authorize(Roles = "CPT Coordinator")]
        // GET: Questionnaire/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Questionnaire questionnaire = db.Questionnaires.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }

            var model = new QuestionnaireViewModel
            {
                Id = questionnaire.QuestionnaireID,
                StartDate = questionnaire.StartDate,
                EndDate = questionnaire.EndDate,
                ResponseQuantityRequired = questionnaire.ResponseQuantityRequired,
                Status = questionnaire.Status,
                QuestionnaireTypeId = questionnaire.QuestionnaireTypeId,
                QuestionnaireTypeName = db.QuestionnaireTypes.Where(q => q.QuestionnaireTypeID == questionnaire.QuestionnaireTypeId).Select(q => q.QuestionnaireTypeName).First(),
                MComment = questionnaire.MComment,
                BComment = questionnaire.BComment
            };

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View(model);
        }


        /// <summary>  
        /// The ReviewDetails action allows the Brand Manager and Marketing Director to view the details of a questionnaire for approval. 
        /// </summary>
        /// <param name="id">QuestionnaireId as parameter</param>
        /// <returns>Questionnaire, ReviewDetails view</returns>
        [Authorize(Roles = "Marketing Director, Brand Manager")]
        // GET: Questionnaire/ReviewDetails/5
        public ActionResult ReviewDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //SelectQuestionnaire selectquestionnaire = db.SelectQuestionnaires.Where(s => s.RequestID == id).FirstOrDefault();

            Questionnaire questionnaire = db.Questionnaires.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound("There is no approved questionnaire created for this request.");
            }

            var model = new QuestionnaireViewModel
            {
                Id = questionnaire.QuestionnaireID,
                StartDate = questionnaire.StartDate,
                EndDate = questionnaire.EndDate,
                ResponseQuantityRequired = questionnaire.ResponseQuantityRequired,
                Status = questionnaire.Status,
                QuestionnaireTypeId = questionnaire.QuestionnaireTypeId,
                QuestionnaireTypeName = db.QuestionnaireTypes.Where(q => q.QuestionnaireTypeID == questionnaire.QuestionnaireTypeId).Select(q => q.QuestionnaireTypeName).First(),
                MComment = questionnaire.MComment,
                BComment = questionnaire.BComment
            };

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View(model);
        }


        /// <summary>  
        /// The ExecutionDetails action allows the CRU Supervisor and CRU Member to view the details of a questionnaire for execution. 
        /// </summary>
        /// <param name="id">QuestionnaireId as parameter</param>
        /// <returns>Questionnaire, ExecutionDetails view</returns>
        [Authorize(Roles = "CRU Supervisor, CRU Member")]
        // GET: Questionnaire/ExecutionDetails/5
        public ActionResult ExecutionDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Questionnaire questionnaire = db.Questionnaires.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }

            var model = new QuestionnaireViewModel
            {
                Id = questionnaire.QuestionnaireID,
                QuestionnaireTitle = questionnaire.QuestionnaireTitle,
                StartDate = questionnaire.StartDate,
                EndDate = questionnaire.EndDate,
                ResponseQuantityRequired = questionnaire.ResponseQuantityRequired,
                Status = questionnaire.Status,
                QuestionnaireTypeId = questionnaire.QuestionnaireTypeId,
                QuestionnaireTypeName = db.QuestionnaireTypes.Where(q => q.QuestionnaireTypeID == questionnaire.QuestionnaireTypeId).Select(q => q.QuestionnaireTypeName).First(),
                CRUSEmployeeID = questionnaire.CRUSEmployeeID,
                CRUSEmployeeName = db.CRUSupervisors.Find(questionnaire.CRUSEmployeeID).FullName,
            };

            if (questionnaire.CRUMEmployeeID != null)
            {
                model.CRUMEmployeeID = questionnaire.CRUMEmployeeID;
                model.CRUMEmployeeName = db.CRUMembers.Find(questionnaire.CRUMEmployeeID).FullName;
            }

            var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View(model);    
        }


        /// <summary>  
        /// The Create action allows for the creation of a new questionnaire by the CPT Coordinator user. 
        /// </summary>
        /// <param name="id">RequestId as parameter to link questionnaire to request.</param>
        /// <returns>Questionnaire, Create view</returns>

        [Authorize(Roles = "CPT Coordinator")]
        // GET: Questionnaire/Create
        public ActionResult Create(int? id)
        {
            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View();
        }


        /// <summary>  
        /// The Create action allows for the creation of a new questionnaire by the CPT Coordinator user. 
        /// </summary>
        /// <param name="id">RequestId as parameter to link questionnaire to request.</param>
        /// <param name="model">QuestionnaireViewModel as the parameter</param>
        /// <returns>Questionnaire, Create view</returns>
        [Authorize(Roles = "CPT Coordinator")]
        // POST: Questionnaire/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? id, QuestionnaireViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create the location from the model
                var questionnaire = new Questionnaire
                {
                    StartDate = model.StartDate, 
                    EndDate = model.EndDate,
                    Status = QuestionnaireStatus.BMQuestionnaireApproval,
                    ResponseQuantityRequired = model.ResponseQuantityRequired,
                    QuestionnaireTypeId = model.QuestionnaireTypeId
                };

                // Save the created course to the database
                db.Questionnaires.Add(questionnaire);
                db.SaveChanges();
                
                var sq = new SelectQuestionnaire
                {
                    QuestionnaireID = questionnaire.QuestionnaireID,
                    RequestID = id,
                    CPTEmployeeID = User.Identity.GetUserId<int>()
                };
                db.SelectQuestionnaires.Add(sq);
                db.SaveChanges();

                if (questionnaire.Status == QuestionnaireStatus.BMQuestionnaireApproval)
                {
                    var request = db.CPTRequests.Where(r => r.RequestID == id).FirstOrDefault();
                    request.QuestionnaireExist = true;
                    request.QuestionnaireId = db.SelectQuestionnaires.Where(s => s.RequestID == request.RequestID).Select(s => s.QuestionnaireID).FirstOrDefault();
                    request.RequestStatus = RequestStatus.BMQuestionnaireApproval;

                    db.Entry(request).State = EntityState.Modified;
                    db.SaveChanges();

                    questionnaire.QuestionnaireTitle = request.RequestTitle;
                    questionnaire.BEmployeeID = request.BReviewRequest;
                    questionnaire.MEmployeeID = request.MReviewRequest;
                    questionnaire.CRUSEmployeeID = db.CRUSupervisors.Where(s => s.Region == request.Location.Region).Select(s => s.Id).First();
                    db.Entry(questionnaire).State = EntityState.Modified;
                    db.SaveChanges();
                }               

                return RedirectToAction("Details", "Questionnaire", new { id = questionnaire.QuestionnaireID});
                //return RedirectToAction("CreateSurvey", "Survey", new { id = sq.Questionnaire.QuestionnaireTypeId });
                //return RedirectToAction("Index");
            }
            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View();
        }


        /// <summary>  
        /// The Edit action allows the CPT Coordinator to edit the questionnaire details. 
        /// </summary>
        /// <param name="id">QuestionnaireId as the parameter.</param>
        /// <returns>Questionnaire, Edit view</returns>
        [Authorize(Roles = "CPT Coordinator")]
        // GET: Questionnaire/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Questionnaire questionnaire = db.Questionnaires.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }

            QuestionnaireViewModel model = new QuestionnaireViewModel
            {
                Id = questionnaire.QuestionnaireID,
                StartDate = questionnaire.StartDate,
                EndDate = questionnaire.EndDate,
                ResponseQuantityRequired = questionnaire.ResponseQuantityRequired,
                QuestionnaireTypeId = questionnaire.QuestionnaireTypeId,
                QuestionnaireTypeName = questionnaire.QuestionnaireTypeName
            };

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View(model);
        }


        /// <summary>  
        /// The Edit action allows the user to edit the questionnaire details. 
        /// </summary>
        /// <param name="id">QuestionnaireId as the parameter.</param>
        /// <param name="model">QuestionnaireViewModel as the parameter.</param>
        /// <returns>Questionnaire, Edit view</returns>
        [Authorize(Roles = "CPT Coordinator")]
        // POST: Questionnaire/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, QuestionnaireViewModel model)
        {
            if (ModelState.IsValid)
            {
                var questionnaire = db.Questionnaires.Find(id);
                if (questionnaire == null)
                {
                    return HttpNotFound();
                }

                // Edit the questionnaire info
                questionnaire.StartDate = model.StartDate;
                questionnaire.EndDate = model.EndDate;
                questionnaire.ResponseQuantityRequired = model.ResponseQuantityRequired;
                questionnaire.QuestionnaireTypeId = model.QuestionnaireTypeId;
                questionnaire.QuestionnaireTypeName = model.QuestionnaireTypeName;

                db.Entry(questionnaire).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details", new { id = questionnaire.QuestionnaireID } );
            }
            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View();
        }

        /// <summary>  
        /// The ReviewQuestionnaire action allows the Brand Manager and Marketing Director users to review questionnaires.  
        /// </summary>
        /// <param name="id">QuestionnaireId as the parameter.</param>
        /// <returns>Questionnaire, ReviewQuestionnaire view</returns>
        [Authorize(Roles = "Brand Manager, Marketing Director")]
        // GET: Questionnaire/ReviewQuestionnaire/5
        public ActionResult ReviewQuestionnaire(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Questionnaire questionnaire = db.Questionnaires.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }

            QuestionnaireViewModel model = new QuestionnaireViewModel
            {
                Id = questionnaire.QuestionnaireID,
            };

            return View(model);
        }


        /// <summary>  
        /// The ReviewQuestionnaire action allows the Brand Manager and Marketing Director users to review questionnaires.  
        /// </summary>
        /// <param name="id">QuestionnaireId as the parameter.</param>
        /// <param name="model">QuestionnaireViewModel as the parameter.</param>
        /// <returns>Questionnaire, ReviewQuestionnaire view</returns>
        [Authorize(Roles = "Brand Manager, Marketing Director")]
        // POST: Questionnaire/ReviewQuestionnaire/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReviewQuestionnaire(int? id, QuestionnaireViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Questionnaire questionnaire = db.Questionnaires.Find(id);
                if (questionnaire == null)
                {
                    return HttpNotFound();
                }
               
                // Review the questionnaire.
                questionnaire.BComment = model.BComment;
                questionnaire.MComment = model.MComment;
                questionnaire.MReviewQuestionnaire = model.MReviewQuestionnaire;
                questionnaire.BReviewQuestionnaire = model.BReviewQuestionnaire;
                               
                db.Entry(questionnaire).State = EntityState.Modified;
                db.SaveChanges();

                // Change questionaire and request status.
                // If Brand Manager approves questionnaire.
                if (questionnaire.BReviewQuestionnaire == Review.Approved)
                {
                    questionnaire.Status = QuestionnaireStatus.MDQuestionnaireApproval;
                }
                db.Entry(questionnaire).State = EntityState.Modified;
                db.SaveChanges();

                var request = db.CPTRequests.Where(r => r.QuestionnaireId == questionnaire.QuestionnaireID).First();
                if (questionnaire.Status == QuestionnaireStatus.MDQuestionnaireApproval)
                {
                    request.RequestStatus = RequestStatus.MDQuestionnaireApproval;
                    db.Entry(request).State = EntityState.Modified;
                    db.SaveChanges();
                }
                
                // If Brand Manager rejects questionniare.
                else if (questionnaire.BReviewQuestionnaire == Review.Rejected)
                {
                    questionnaire.Status = QuestionnaireStatus.QuestionnaireModification;
                    db.Entry(questionnaire).State = EntityState.Modified;
                    db.SaveChanges();

                    if (questionnaire.Status == QuestionnaireStatus.QuestionnaireModification)
                    {
                        request.RequestStatus = RequestStatus.QuestionnaireModification;
                        db.Entry(request).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                };

                // If Marketing Director approves questionnaire.
                if (questionnaire.MReviewQuestionnaire == Review.Approved)
                {
                    questionnaire.Status = QuestionnaireStatus.QuestionnaireExecution;
                    db.Entry(questionnaire).State = EntityState.Modified;
                    db.SaveChanges();

                    if (questionnaire.Status == QuestionnaireStatus.QuestionnaireExecution)
                    {
                        request.RequestStatus = RequestStatus.QuestionnaireExecution;
                        db.Entry(request).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                // If Marketing Director rejects questionnaire.
                else if (questionnaire.MReviewQuestionnaire == Review.Rejected)
                {
                    questionnaire.Status = QuestionnaireStatus.QuestionnaireModification;
                    db.Entry(questionnaire).State = EntityState.Modified;
                    db.SaveChanges();

                    if (questionnaire.Status == QuestionnaireStatus.QuestionnaireModification)
                    {
                        request.RequestStatus = RequestStatus.QuestionnaireModification;
                        db.Entry(request).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
 
                // Return to Pending Requests 
                if (User.IsInRole("Brand Manager"))
                {
                    return RedirectToAction("BrandManagerReviewIndex", "CPTRequest");
                }
                else if (User.IsInRole("Marketing Director"))
                {
                    return RedirectToAction("MarketingDirectorReviewIndex", "CPTRequest");
                }
            }
            return View();
        }


        // <summary>  
        /// The ReviewQuestionnaire action allows the Brand Manager and Marketing Director users to review questionnaires.  
        /// </summary>
        /// <param name="id">QuestionnaireId as the parameter.</param>
        /// <returns>Questionnaire, ReviewQuestionnaire view</returns>
        [Authorize(Roles = "CPT Coordinator")]
        // GET: Questionnaire/ReviewQuestionnaire/5
        public ActionResult ConfirmResubmission(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Questionnaire questionnaire = db.Questionnaires.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }

            QuestionnaireViewModel model = new QuestionnaireViewModel
            {
                Id = questionnaire.QuestionnaireID,
            };

            return View(model);
        }


        /// <summary>  
        /// The ReviewQuestionnaire action allows the Brand Manager and Marketing Director users to review questionnaires.  
        /// </summary>
        /// <param name="id">QuestionnaireId as the parameter.</param>
        /// <param name="model">QuestionnaireViewModel as the parameter.</param>
        /// <returns>Questionnaire, ReviewQuestionnaire view</returns>
        [Authorize(Roles = "CPT Coordinator")]
        // POST: Questionnaire/ReviewQuestionnaire/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmResubmission(int? id, QuestionnaireViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Questionnaire questionnaire = db.Questionnaires.Find(id);
                if (questionnaire == null)
                {
                    return HttpNotFound();
                }

                // Review the questionnaire.
                questionnaire.ConfirmResubmission = model.ConfirmResubmission;

                db.Entry(questionnaire).State = EntityState.Modified;
                db.SaveChanges();

                // Change questionaire and request status.
                // If Brand Manager approves questionnaire.
                if (questionnaire.ConfirmResubmission == Review.Approved)
                {
                    questionnaire.Status = QuestionnaireStatus.BMQuestionnaireApproval;
                }
                db.Entry(questionnaire).State = EntityState.Modified;
                db.SaveChanges();

                var request = db.CPTRequests.Where(r => r.QuestionnaireId == questionnaire.QuestionnaireID).First();
                if (questionnaire.Status == QuestionnaireStatus.BMQuestionnaireApproval)
                {
                    request.RequestStatus = RequestStatus.BMQuestionnaireApproval;
                    db.Entry(request).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Details", "Questionnaire", new { id = questionnaire.QuestionnaireID });

            }
            return View();
        }

        /// <summary>  
        /// The AssignQuestionnaire action allows the CRU Supervisor to assign a questionnaire's execution to a CRU Member.   
        /// </summary>
        /// <param name="id">QuestionnaireId as the parameter.</param>
        /// <returns>Questionnaire, AssignQuestionnaire view</returns>
        [Authorize(Roles = "CRU Supervisor")]
        // GET: Questionnaire/AssignQuestionnaire/5
        public ActionResult AssignQuestionnaire(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Questionnaire questionnaire = db.Questionnaires.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }

            AssignWorkViewModel model = new AssignWorkViewModel
            {
                QuestionnaireID = questionnaire.QuestionnaireID,
                CMEEmployeeID = questionnaire.CRUMEmployeeID,
                CSEmployeeID = questionnaire.CRUSEmployeeID
            };

            //var Supervisor = db.CRUSupervisors.Where(s => s.Id == model.CRUSEmployeeID).First();
            //var list = db.CRUMembers.ToList().Where(m => m.Region == Supervisor.Region);
            //ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");

            var list = db.CRUMembers.ToList().Where(m => m.Region == questionnaire.CRUSupervisor.Region);
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View(model);
        }


        /// <summary>  
        /// The AssignQuestionnaire action allows the CRU Supervisor to assign a questionnaire's execution to a CRU Member.   
        /// </summary>
        /// <param name="id">QuestionnaireId as the parameter.</param>
        /// <param name="model">QuestionnaireViewModel as the parameter.</param>
        /// <returns>Questionnaire, AssignQuestionnaire view</returns>
        [Authorize(Roles = "CRU Supervisor")]
        // POST: Questionnaire/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignQuestionnaire(int id, AssignWorkViewModel model)
        {
            if (ModelState.IsValid)
            {
                var questionnaire = db.Questionnaires.Find(id);
                if (questionnaire != null)
                {
                    questionnaire.CRUMEmployeeID = model.CMEEmployeeID;
                }

                db.Entry(questionnaire).State = EntityState.Modified;
                db.SaveChanges();

                AssignWork assignwork = new AssignWork
                {
                    QuestionnaireID = questionnaire.QuestionnaireID,
                    CMEEmployeeID = questionnaire.CRUMEmployeeID,
                    CSEmployeeID = questionnaire.CRUSEmployeeID,
                    AssignmentDate = DateTime.Now
                };

                db.AssignWorks.Add(assignwork);
                db.SaveChanges();

                return RedirectToAction("CRUSupervisorIndex");
            }

            var Supervisor = db.CRUSupervisors.Where(s => s.Id == model.CSEmployeeID).First();
            var list = db.CRUMembers.ToList().Where(m => m.Region == Supervisor.Region);
            ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");

            ViewBag.QuestionnaireTypeId = new SelectList(db.QuestionnaireTypes, "QuestionnaireTypeId", "QuestionnaireTypeName");
            return View();
        }
    
    }
}
