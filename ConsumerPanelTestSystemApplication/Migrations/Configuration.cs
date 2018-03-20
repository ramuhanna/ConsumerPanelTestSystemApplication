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
                  new Location { City = "Jeddah" },
                  new Location { City = "Madina" },
                  new Location { City = "Riyadh" },
                  new Location { City = "Dammam" },
                  new Location { City = "Taif" },
                  new Location { City = "Khobar" },
                  new Location { City = "Jubail" },
                  new Location { City = "Qassim" }
            };

            locations.ForEach(s => context.Locations.AddOrUpdate(p => p.City, s));
            context.SaveChanges();

            // Add Employees to test the CPTRequest
            // MDecisionId = 2 => MarketingDirector(Id)

            var mdUser = new MarketingDirector
            {
                UserName = "MDUN",
                Email = "MD@G.COM",
                FirstName = "MDFN",
                LastName = "MDLN",
                Country = EmployeeCountry.SaudiArabia,
                City = EmployeeCity.Jeddah,
                Type = EmployeeType.MarketingDirector,
            };

            userManager.Create(mdUser, "123456");

            // MReviewRequest = 2 => MarketingDirector(Id)
            // mdUser

            //REmployeeId = 3 => Requester(Id)
            var requesterUser = new Requester
            {
                UserName = "REQUN",
                Email = "REQ@G.COM",
                FirstName = "REQFN",
                LastName = "REQLN",
                Country = EmployeeCountry.SaudiArabia,
                City = EmployeeCity.Jeddah,
                Type = EmployeeType.MarketingDirector,
                Department = Department.Marketing,
                Position = "Boss",
            };

            userManager.Create(requesterUser, "123456");

            // BDecisionId = 4 => BrandManager(Id)
            var bmUser = new BrandManager
            {
                UserName = "BMUN",
                Email = "BM@G.COM",
                FirstName = "BMFN",
                LastName = "BMLN",
                Country = EmployeeCountry.SaudiArabia,
                City = EmployeeCity.Jeddah,
                Type = EmployeeType.MarketingDirector,
                ProductDivision = BrandManagerProductDivision.BabyCare,
            };

            userManager.Create(bmUser, "123456");

            // BReviewRequest = 4 => BrandManager(Id)
            // bmUser

            // BEmployeeId = 4 => BrandManager(Id)
            // bmUser
        }
    }
}
