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
                Type = EmployeeType.CPTCoornidator,
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
                    ProductDivision = BrandManagerProductDivision.FeminineCare, RequestDate = DateTime.Parse("05/01/2018"), SubmittedById = 15, LocationId = 1, BReviewRequest = 6},
                new CPTRequest { RequestTitle = "Bambi Jumbo Launch",  RequestStatus = RequestStatus.BMRequestApproval, SubmittedById = 15,
                    Justification = "We need to get national feedback on the regular Bambi performance to evaluate any neccessary changes.",
                    ProductDivision = BrandManagerProductDivision.BabyCare, RequestDate = DateTime.Parse("10/02/2018"), LocationId = 2, BReviewRequest = 8},
                new CPTRequest { RequestTitle = "Competition Analysis",  RequestStatus = RequestStatus.BMRequestApproval,
                    Justification = "We want to see how Orenex is performaing in the market, particularly within the Eastern region.",
                    ProductDivision = BrandManagerProductDivision.HouseholdItems, RequestDate = DateTime.Parse("21/01/2018"), SubmittedById = 17, LocationId = 3, BReviewRequest = 7},
                new CPTRequest { RequestTitle = "Sanita Tissue Annual Assessment",  RequestStatus = RequestStatus.BMRequestApproval,
                    Justification = "The annual performance on the Sanita tissue product needs to be assessed for quality purposes.",
                    ProductDivision = BrandManagerProductDivision.FamilyCare, RequestDate = DateTime.Parse("15/03/2018"), SubmittedById = 16, LocationId = 4, BReviewRequest = 5}
            };

            requests.ForEach(s => context.CPTRequests.AddOrUpdate(p => p.RequestTitle, s));
            context.SaveChanges();


            // Add CPT Requests from Brand Managers to the database.
            var BMrequests = new List<CPTRequest>
            {
                new CPTRequest { RequestTitle = "Feminine Care Market Positioning",  RequestStatus = RequestStatus.MDRequestApproval,
                    Justification = "We need some market research in the western regions on the perfomance of feminine care products.",
                    ProductDivision = BrandManagerProductDivision.FeminineCare, RequestDate = DateTime.Parse("05/01/2018"), SubmittedById = 6, LocationId = 3, BReview = true, BReviewRequest = 6},
                new CPTRequest { RequestTitle = "Bambi Market Analysis",  RequestStatus = RequestStatus.MDRequestApproval,
                    Justification = "We need to get national feedback on the regular Bambi performance to evaluate any neccessary changes.",
                    ProductDivision = BrandManagerProductDivision.BabyCare, RequestDate = DateTime.Parse("10/02/2018"), SubmittedById = 8, LocationId = 1, BReview = true, BReviewRequest = 8},
                new CPTRequest { RequestTitle = "Tracking Tests for new Sanita Aluminum",  RequestStatus = RequestStatus.MDRequestApproval,
                    Justification = "We want to see how Orenex is performaing in the market, particularly within the Eastern region.",
                    ProductDivision = BrandManagerProductDivision.HouseholdItems, RequestDate = DateTime.Parse("21/01/2018"), SubmittedById = 7, LocationId = 7, BReview = true, BReviewRequest = 7},
                new CPTRequest { RequestTitle = "Sanita Market Positioning",  RequestStatus = RequestStatus.MDRequestApproval,
                    Justification = "The annual performance on the Sanita tissue product needs to be assessed for quality purposes.",
                    ProductDivision = BrandManagerProductDivision.FamilyCare, RequestDate = DateTime.Parse("15/03/2018"), SubmittedById = 5, LocationId = 5, BReview = true, BReviewRequest = 5}
            };

            BMrequests.ForEach(s => context.CPTRequests.AddOrUpdate(p => p.RequestTitle, s));
            context.SaveChanges();


            // Add examples of questins.
            var questionnairetypes = new List<QuestionnaireType>
            {
                new QuestionnaireType { QuestionnaireTypeName = "Performance Tracking"},
                new QuestionnaireType { QuestionnaireTypeName = "Product Development"},
                new QuestionnaireType { QuestionnaireTypeName = "Tracking Tests"},
                new QuestionnaireType { QuestionnaireTypeName = "Product Assessment"}
            };            
            questionnairetypes.ForEach(s => context.QuestionnaireTypes.AddOrUpdate(p => p.QuestionnaireTypeName, s));
            context.SaveChanges();

             //new QuestionnaireType { QuestionText = "Would you purchase this product again?"},
             //    new Question { QuestionText = "Did the product cause any rashes or discomforts?"},
             //    new Question { QuestionText = "How much are you willing to pay for this product?"},
             //    new Question { QuestionText = "What might make you purchase a competing product?"},
             //    new Question { QuestionText = "Who goes and makes the purchase?"},
             //    new Question { QuestionText = "For how long have you been purchasing this product?"},

        }
    }
}
