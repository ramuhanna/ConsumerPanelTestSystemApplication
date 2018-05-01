namespace ConsumerPanelTestSystemApplication.Migrations
{
    using Microsoft.AspNet.Identity;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ConsumerPanelTestSystemApplication.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ConsumerPanelTestSystemApplication.Models.ApplicationDbContext context)
        {
            //TODO Define roles to add to your app, keep the Admin role first
            string[] roles = { "Admin", "Brand Manager", "Marketing Director", "Requester", "CPT Coordinator", "CRU Manager", "CRU Supervisor", "CRU Member" };

            //TODO Change admin user login information
            string adminEmail = "admin@npg.com";
            string adminUserName = "admin";
            string adminPassword = "admin123";


            // Create roles
            var roleStore = new CustomRoleStore(context);
            var roleManager = new RoleManager<CustomRole, int>(roleStore);

            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new CustomRole { Name = role });
                }
            }

            // Define admin user
            var userStore = new CustomUserStore(context);
            var userManager = new ApplicationUserManager(userStore);

            //TODO Change the type of the admin user
            var admin = new ApplicationUser
            {
                UserName = adminUserName,
                Email = adminEmail,
                EmailConfirmed = true,
                LockoutEnabled = false
            };

            // Create admin user
            if (userManager.FindByName(admin.UserName) == null)
            {
                userManager.Create(admin, adminPassword);
            }

            // Add admin user to admin role
            // roles[0] is "Admin"
            var user = userManager.FindByName(admin.UserName);
            if (!userManager.IsInRole(user.Id, roles[0]))
            {
                userManager.AddToRole(admin.Id, roles[0]);
            }


            // Add examples of locations.
            var locations = new List<Location>
            {
                  new Location { City = "Jeddah", Region = SupervisorRegion.WesternRegion },
                  new Location { City = "Madina", Region = SupervisorRegion.WesternRegion },
                  new Location { City = "Riyadh", Region = SupervisorRegion.CentralRegion },
                  new Location { City = "Dammam", Region = SupervisorRegion.EasternRegion },
                  new Location { City = "Taif", Region = SupervisorRegion.WesternRegion },
                  new Location { City = "Khobar", Region = SupervisorRegion.EasternRegion },
                  new Location { City = "Jubail", Region = SupervisorRegion.EasternRegion },
                  new Location { City = "Qassim", Region = SupervisorRegion.CentralRegion }
            };

            locations.ForEach(s => context.Locations.AddOrUpdate(p => p.City, s));
            context.SaveChanges();


            // Add Marketing Director user.
            var mdUser = new MarketingDirector
            {
                UserName = "MDUN",
                Email = "MD@G.COM",
                FirstName = "MDFN",
                LastName = "MDLN",
                PhoneNumber = "0512345678",
                Country = EmployeeCountry.SaudiArabia,
                City = EmployeeCity.Dammam,
                Type = EmployeeType.MarketingDirector,
            };
            userManager.Create(mdUser, "123456");

            var userMD = userManager.FindByName(mdUser.UserName);
            if (!userManager.IsInRole(userMD.Id, roles[2]))
            {
                userManager.AddToRole(mdUser.Id, roles[2]);
            }

            // Add CPT Coordinator user.
            var cptUser = new CPTCoordinator
            {
                UserName = "CPTUN",
                Email = "CPT@G.COM",
                FirstName = "CPTFN",
                LastName = "CPTLN",
                PhoneNumber = "0512345678",
                Country = EmployeeCountry.Lebanon,
                City = EmployeeCity.Beirut,
                Type = EmployeeType.CPTCoordinator,
            };
            userManager.Create(cptUser, "123456");

            var userCPT = userManager.FindByName(cptUser.UserName);
            if (!userManager.IsInRole(userCPT.Id, roles[4]))
            {
                userManager.AddToRole(cptUser.Id, roles[4]);
            }

            // Add CRU Manager user.
            var crumUser = new CRUManager
            {
                UserName = "CRUMTUN",
                Email = "CRUM@G.COM",
                FirstName = "CRUMFN",
                LastName = "CRUMLN",
                PhoneNumber = "0512345698",
                Country = EmployeeCountry.SaudiArabia,
                City = EmployeeCity.Dammam,
                Type = EmployeeType.CRUManager,
            };
            userManager.Create(crumUser, "123456");

            var userCRUM = userManager.FindByName(crumUser.UserName);
            if (!userManager.IsInRole(userCRUM.Id, roles[5]))
            {
                userManager.AddToRole(crumUser.Id, roles[5]);
            }


            // Add Brand Managers to the database.          
            var brandmanagers = new List<BrandManager>
            {
                new BrandManager { UserName = "BMUN1", Email ="BM1@g.com", FirstName ="BM1", LastName ="BM11",
                    PhoneNumber = "0513146789", Country = EmployeeCountry.SaudiArabia, City = EmployeeCity.Dammam,
                    Type = EmployeeType.BrandManager, ProductDivision = BrandManagerProductDivision.FamilyCare},
                new BrandManager { UserName = "BMUN2", Email ="BM2@g.com", FirstName ="BM2", LastName ="BM22",
                    PhoneNumber = "0518946789", Country = EmployeeCountry.SaudiArabia, City = EmployeeCity.Dammam,
                    Type = EmployeeType.BrandManager, ProductDivision = BrandManagerProductDivision.FeminineCare},
                new BrandManager { UserName = "BMUN3", Email ="BM3@g.com", FirstName ="BM3", LastName ="BM33",
                    PhoneNumber = "0574146789", Country = EmployeeCountry.SaudiArabia, City = EmployeeCity.Dammam,
                    Type = EmployeeType.BrandManager, ProductDivision = BrandManagerProductDivision.HouseholdItems},
                new BrandManager { UserName = "BMUN4", Email ="BM4@g.com", FirstName ="BM4", LastName ="BM44",
                    PhoneNumber = "0574145789", Country = EmployeeCountry.SaudiArabia, City = EmployeeCity.Dammam,
                    Type = EmployeeType.BrandManager, ProductDivision = BrandManagerProductDivision.BabyCare}
            };

            foreach (var brandmanager in brandmanagers)
            {
                if (userManager.FindByName(brandmanager.UserName) == null)
                {
                    userManager.Create(brandmanager, "crus123");
                }

                var usertemp = userManager.FindByName(brandmanager.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[1]))
                {
                    userManager.AddToRole(usertemp.Id, roles[1]);
                }
            }


            // Add CRU Supervisors to the database.
            var crusupervisors = new List<CRUSupervisor>
            {
                new CRUSupervisor { UserName = "crus1", Email ="crus1@g.com", FirstName ="CRUS1", LastName ="Supervisor11",
                    PhoneNumber = "0513456789", Country = EmployeeCountry.SaudiArabia, City = EmployeeCity.Dammam,
                    Region = SupervisorRegion.EasternRegion, Type = EmployeeType.CRUSupervisor},
                new CRUSupervisor { UserName = "crus2", Email ="crus2@g.com", FirstName ="CRUS2", LastName ="Supervisor22",
                    PhoneNumber = "0523456654", Country = EmployeeCountry.SaudiArabia, City = EmployeeCity.Jeddah,
                    Region = SupervisorRegion.WesternRegion, Type = EmployeeType.CRUSupervisor},
                new CRUSupervisor { UserName = "crus3", Email ="crus3@g.com", FirstName ="CRUS3", LastName ="Supervisor33",
                    PhoneNumber = "0533452589", Country = EmployeeCountry.SaudiArabia, City = EmployeeCity.Riyadh,
                    Region = SupervisorRegion.CentralRegion, Type = EmployeeType.CRUSupervisor}
            };

            foreach (var crusupervisor in crusupervisors)
            {
                if (userManager.FindByName(crusupervisor.UserName) == null)
                {
                    userManager.Create(crusupervisor, "crus123");
                }

                var usertemp = userManager.FindByName(crusupervisor.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[6]))
                {
                    userManager.AddToRole(usertemp.Id, roles[6]);
                }
            }


            // Add CRU Members to the database.
            var crumembers = new List<CRUMember>
            {
                new CRUMember { UserName = "crum1", Email ="crum1@g.com", FirstName ="CRUM1", LastName ="Member11",
                    PhoneNumber = "0509637789", Country = EmployeeCountry.SaudiArabia, City = EmployeeCity.Dammam,
                    Region = SupervisorRegion.WesternRegion, Type = EmployeeType.CRUMember, CRUSupervisorId = 10},
                new CRUMember { UserName = "crum2", Email ="crum2@g.com", FirstName ="CRUM2", LastName ="Member22",
                    PhoneNumber = "0508547789", Country = EmployeeCountry.SaudiArabia, City = EmployeeCity.Jeddah,
                    Region = SupervisorRegion.EasternRegion, Type = EmployeeType.CRUMember, CRUSupervisorId = 9},
                new CRUMember { UserName = "crum3", Email ="crum3@g.com", FirstName ="CRUM3", LastName ="Member33",
                    PhoneNumber = "0589047789", Country = EmployeeCountry.SaudiArabia, City = EmployeeCity.Riyadh,
                    Region = SupervisorRegion.CentralRegion, Type = EmployeeType.CRUMember, CRUSupervisorId = 11},
            };

            foreach (var crumember in crumembers)
            {
                if (userManager.FindByName(crumember.UserName) == null)
                {
                    userManager.Create(crumember, "crum123");
                }

                var usertemp = userManager.FindByName(crumember.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[7]))
                {
                    userManager.AddToRole(usertemp.Id, roles[7]);
                }
            }


            // Add Requesters to the database.
            var requesters = new List<Requester>
            {
                new Requester { UserName = "REQUN", Email = "REQ@G.COM", FirstName = "REQFN", LastName = "REQLN",
                    PhoneNumber = "0541236985", Country = EmployeeCountry.SaudiArabia, City = EmployeeCity.Madina,
                    Department = Department.RD, Position = "Vice President of Research and Development", Type = EmployeeType.Requester},
                new Requester { UserName = "REQUN1", Email ="REQ1@g.com", FirstName ="REQ1", LastName ="REQ11",
                    PhoneNumber = "0509689789", Country = EmployeeCountry.SaudiArabia, City = EmployeeCity.Jeddah,
                    Department = Department.Production, Position = "Assisstant Production Supervisor", Type = EmployeeType.Requester},
                new Requester { UserName = "REQUN2", Email ="REQ2@g.com", FirstName ="REQ2", LastName ="REQ22",
                    PhoneNumber = "0509637019", Country = EmployeeCountry.SaudiArabia, City = EmployeeCity.Riyadh,
                    Department = Department.QAQC, Position = "Quality Assurance Head", Type = EmployeeType.Requester},
                new Requester { UserName = "REQUN3", Email ="REQ3@g.com", FirstName ="REQ3", LastName ="REQ33",
                    PhoneNumber = "0509685789", Country = EmployeeCountry.SaudiArabia, City = EmployeeCity.Dammam,
                    Department = Department.Sales, Position = "Regional Sales Supervisor", Type = EmployeeType.Requester},
            };

            foreach (var requester in requesters)
            {
                if (userManager.FindByName(requester.UserName) == null)
                {
                    userManager.Create(requester, "req123");
                }

                var usertemp = userManager.FindByName(requester.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[3]))
                {
                    userManager.AddToRole(usertemp.Id, roles[3]);
                }
            }

            // Add CPT Requests from Requesters to the database.
            var requests = new List<CPTRequest>
            {
                new CPTRequest { RequestTitle = "Feminine Care Product Campaign",  RequestStatus = RequestStatus.BMRequestApproval,
                    Justification = "We need some market research in the western regions on the perfomance of feminine care products.",
                    ProductDivision = BrandManagerProductDivision.FeminineCare, RequestDate = new DateTime(2018,1,5), SubmittedById = 15, LocationId = 1, BReviewRequest = 6},

                new CPTRequest { RequestTitle = "Bambi Jumbo Launch",  RequestStatus = RequestStatus.BMRequestApproval, SubmittedById = 15,
                    Justification = "We need to get national feedback on the regular Bambi performance to evaluate any neccessary changes.",
                    ProductDivision = BrandManagerProductDivision.BabyCare, RequestDate = new DateTime(2018,02,10), LocationId = 2, BReviewRequest = 8},

                new CPTRequest { RequestTitle = "Competition Analysis",  RequestStatus = RequestStatus.BMRequestApproval,
                    Justification = "We want to see how Orenex is performaing in the market, particularly within the Eastern region.",
                    ProductDivision = BrandManagerProductDivision.HouseholdItems, RequestDate = new DateTime(2018,01,21), SubmittedById = 17, LocationId = 3, BReviewRequest = 7},

                new CPTRequest { RequestTitle = "Sanita Tissue Annual Assessment",  RequestStatus = RequestStatus.BMRequestApproval,
                    Justification = "The annual performance on the Sanita tissue product needs to be assessed for quality purposes.",
                    ProductDivision = BrandManagerProductDivision.FamilyCare, RequestDate = new DateTime(2018,03,15), SubmittedById = 16, LocationId = 4, BReviewRequest = 5}
            };

            requests.ForEach(s => context.CPTRequests.AddOrUpdate(p => p.RequestTitle, s));
            context.SaveChanges();


            // Add CPT Requests from Brand Managers to the database.
            var BMrequests = new List<CPTRequest>
            {
                new CPTRequest { RequestTitle = "Feminine Care Market Positioning",  RequestStatus = RequestStatus.MDRequestApproval,
                    Justification = "We need some market research in the western regions on the perfomance of feminine care products.",
                    ProductDivision = BrandManagerProductDivision.FeminineCare, RequestDate = new DateTime(2018,1,5), SubmittedById = 6, LocationId = 3, BReview = Review.Approved, BReviewRequest = 6},

                new CPTRequest { RequestTitle = "Bambi Market Analysis",  RequestStatus = RequestStatus.MDRequestApproval,
                    Justification = "We need to get national feedback on the regular Bambi performance to evaluate any neccessary changes.",
                    ProductDivision = BrandManagerProductDivision.BabyCare, RequestDate = new DateTime(2018,2,10), SubmittedById = 8, LocationId = 1, BReview = Review.Approved, BReviewRequest = 8},

                new CPTRequest { RequestTitle = "Tracking Tests for new Sanita Aluminum",  RequestStatus = RequestStatus.MDRequestApproval,
                    Justification = "We want to see how Orenex is performaing in the market, particularly within the Eastern region.",
                    ProductDivision = BrandManagerProductDivision.HouseholdItems, RequestDate = new DateTime(2018,1,21), SubmittedById = 7, LocationId = 7, BReview = Review.Approved, BReviewRequest = 7},

                new CPTRequest { RequestTitle = "Sanita Market Positioning",  RequestStatus = RequestStatus.MDRequestApproval,
                    Justification = "The annual performance on the Sanita tissue product needs to be assessed for quality purposes.",
                    ProductDivision = BrandManagerProductDivision.FamilyCare, RequestDate = new DateTime(2018,3,15), SubmittedById = 5, LocationId = 5, BReview = Review.Approved, BReviewRequest = 5},

                // Add CPT Requests for CPT Coordinator Index.
                new CPTRequest { RequestTitle = "Sanita Giant Tissues Performance",  RequestStatus = RequestStatus.QuestionnaireCreation, Justification = "We need to assess the performance and likability of the Giant Tissues in order to adjust product accordingly.", ProductDivision = BrandManagerProductDivision.FamilyCare, RequestDate = new DateTime(2018,3,28), SubmittedById = 15, LocationId = 5, BReview = Review.Approved, BReviewRequest = 5, MReviewRequest = 2, MReview = Review.Approved},

                new CPTRequest { RequestTitle = "Bi-Annual Private Assessment",  RequestStatus = RequestStatus.QuestionnaireCreation, Justification = "We need updated information on the product line's performance.", ProductDivision = BrandManagerProductDivision.FeminineCare, RequestDate = new DateTime(2018,2,28), SubmittedById = 17, LocationId = 1, BReview = Review.Approved, BReviewRequest = 6, MReviewRequest = 2, MReview = Review.Approved},

                 new CPTRequest { RequestTitle = "New Product Market Research",  RequestStatus = RequestStatus.QuestionnaireCreation, Justification = "We need some preliminary research on the market's acceptance of the new product line.", ProductDivision = BrandManagerProductDivision.FeminineCare, RequestDate = new DateTime(2018,4,28), SubmittedById = 15, LocationId = 3, BReview = Review.Approved, BReviewRequest = 6, MReviewRequest = 2, MReview = Review.Approved},
            };

            BMrequests.ForEach(s => context.CPTRequests.AddOrUpdate(p => p.RequestTitle, s));
            context.SaveChanges();


            // Add questionnaire types.
            var questionnairetypes = new List<QuestionnaireType>
            {
                new QuestionnaireType { QuestionnaireTypeName = "Performance Tracking"},
                new QuestionnaireType { QuestionnaireTypeName = "Product Development"},
                new QuestionnaireType { QuestionnaireTypeName = "Tracking Tests"},
                new QuestionnaireType { QuestionnaireTypeName = "Product Assessment"}
            };            
            questionnairetypes.ForEach(s => context.QuestionnaireTypes.AddOrUpdate(p => p.QuestionnaireTypeName, s));
            context.SaveChanges();

            // Add questions.
            var questions = new List<Question>
            {
                new Question { QuestionText = "Did the product cause any rashes or discomforts?", ResponseType = ResponseType.RadioButton },
                new Question { QuestionText = "How much are you willing to pay for this product?", ResponseType = ResponseType.TextBox },
                new Question { QuestionText = "What might make you purchase a competing product?", ResponseType = ResponseType.TextBox },
                new Question { QuestionText = "Who goes and makes the purchase?", ResponseType = ResponseType.TextBox },
                new Question { QuestionText = "For how many years have you been purchasing this product?", ResponseType = ResponseType.TextBox },

                new Question { QuestionText = "Do you use NAPCO products often? " , ResponseType = ResponseType.RadioButton},
                new Question { QuestionText = "What is the most important factor for you when purchasing a product like this?", ResponseType = ResponseType.TextBox },
                new Question { QuestionText = "What is your name?", ResponseType = ResponseType.TextBox},
                new Question { QuestionText = "How old are you?", ResponseType = ResponseType.TextBox },
                new Question { QuestionText = "What was your impression of the product after the trial period?", ResponseType = ResponseType.TextBox },

                new Question { QuestionText = "When choosing baby care products, is price an important factor for you?", ResponseType = ResponseType.RadioButton },
                new Question { QuestionText = "How willing would you be to try a new product?" , ResponseType = ResponseType.RadioButton},
                new Question { QuestionText = "Do sales and discounts affect your purchase of a particular product?", ResponseType = ResponseType.RadioButton },
                new Question { QuestionText = "How strongly do you like this product?", ResponseType = ResponseType.RadioButton },
                new Question { QuestionText = "How many children do you have?", ResponseType = ResponseType.TextBox },

                new Question { QuestionText = "What is the age of your youngest child?", ResponseType = ResponseType.TextBox },
                new Question { QuestionText = "Where are you most likely to purchase this product?", ResponseType = ResponseType.TextBox },
                new Question { QuestionText = "Are you willing to pay more for higher quality products?", ResponseType = ResponseType.RadioButton },
                new Question { QuestionText = "What do you know about this product?", ResponseType = ResponseType.TextBox },
                new Question { QuestionText = "Where do you live?", ResponseType = ResponseType.TextBox },
            };
            questions.ForEach(s => context.Questions.AddOrUpdate(p => p.QuestionText, s));
            context.SaveChanges();


            // Add questions to question types.
            var questiontypes = new List<QuestionType>
            {
                new QuestionType { QuestionID = 1, QuestionnaireTypeID = 3 },
                new QuestionType { QuestionID = 1, QuestionnaireTypeID = 4 },
                new QuestionType { QuestionID = 2, QuestionnaireTypeID = 2 },
                new QuestionType { QuestionID = 2, QuestionnaireTypeID = 4 },
                new QuestionType { QuestionID = 3, QuestionnaireTypeID = 1 },
                new QuestionType { QuestionID = 3, QuestionnaireTypeID = 2 },

                new QuestionType { QuestionID = 4, QuestionnaireTypeID = 1 },
                new QuestionType { QuestionID = 4, QuestionnaireTypeID = 4 },
                new QuestionType { QuestionID = 5, QuestionnaireTypeID = 1 },
                new QuestionType { QuestionID = 5, QuestionnaireTypeID = 4 },

                new QuestionType { QuestionID = 6, QuestionnaireTypeID = 2 },
                new QuestionType { QuestionID = 6, QuestionnaireTypeID = 4 },
                new QuestionType { QuestionID = 7, QuestionnaireTypeID = 1 },
                new QuestionType { QuestionID = 7, QuestionnaireTypeID = 2 },
                new QuestionType { QuestionID = 7, QuestionnaireTypeID = 3 },
                new QuestionType { QuestionID = 7, QuestionnaireTypeID = 4 },

                new QuestionType { QuestionID = 8, QuestionnaireTypeID = 1 },
                new QuestionType { QuestionID = 8, QuestionnaireTypeID = 2 },
                new QuestionType { QuestionID = 8, QuestionnaireTypeID = 3 },
                new QuestionType { QuestionID = 8, QuestionnaireTypeID = 4 },

                new QuestionType { QuestionID = 9, QuestionnaireTypeID = 1 },
                new QuestionType { QuestionID = 9, QuestionnaireTypeID = 2 },
                new QuestionType { QuestionID = 9, QuestionnaireTypeID = 3 },
                new QuestionType { QuestionID = 9, QuestionnaireTypeID = 4 },
                new QuestionType { QuestionID = 10, QuestionnaireTypeID = 3 }
            };
            questiontypes.ForEach(s => context.QuestionTypes.AddOrUpdate(p => new { p.QuestionID, p.QuestionnaireTypeID}, s));
            context.SaveChanges();


            // Add questionnaires for CPT Requests.
            var questionnaires = new List<Questionnaire>
            {
                new Questionnaire { StartDate = new DateTime (2018,4,30), EndDate = new DateTime(2018,6,20) , ResponseQuantityRequired = 250, QuestionnaireTypeId = 3, Status = QuestionnaireStatus.BMQuestionnaireApproval},

                new Questionnaire { StartDate = new DateTime (2018,5,10), EndDate = new DateTime(2018,5,30) , ResponseQuantityRequired = 150, QuestionnaireTypeId = 1, Status = QuestionnaireStatus.BMQuestionnaireApproval},

            };
            questionnaires.ForEach(p => context.Questionnaires.AddOrUpdate(q => q.StartDate, p));
            context.SaveChanges();

            ////Add select questionnaires to link questionnaires to requests.

            //var selectquestionnaires = new List<SelectQuestionnaire>
            //{
            //     new SelectQuestionnaire { RequestID = 11, QuestionnaireID = 1, CPTEmployeeID = 3},

            //     new SelectQuestionnaire { RequestID = 9, QuestionnaireID = 2, CPTEmployeeID = 3}
            //};

            //selectquestionnaires.ForEach(p => context.SelectQuestionnaires.AddOrUpdate(q => q.RequestID, p));
            //context.SaveChanges();

            //// Update requests with questionnaire information.
            //var request9 = requests.Where(r => r.RequestID == 9).First();
            //request9.QuestionnaireId = 2;
            //request9.QuestionnaireExist = true;
            //request9.RequestStatus = RequestStatus.BMQuestionnaireApproval;
            //context.SaveChanges();

            //var request11 = requests.Where(r => r.RequestID == 11).First();
            //request11.QuestionnaireId = 1;
            //request11.QuestionnaireExist = true;
            //request11.RequestStatus = RequestStatus.BMQuestionnaireApproval;
            //context.SaveChanges();

        }
    }
}
