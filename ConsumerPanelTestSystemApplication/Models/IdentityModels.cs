/*
* Description: The Consumer Panel Test System is a Web-Based application utilized for organized and systemized market research process.
* Author: R.M.
* Due date: 20/03/2018
*/

using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ConsumerPanelTestSystemApplication.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole,
    CustomUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole,
    int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<AdditionalQuestion> AdditionalQuestions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AssignWork> AssignWorks { get; set; }
        public virtual DbSet<BrandManager> BrandManagers { get; set; }
        public virtual DbSet<ContainQuestion> ContainQuestions { get; set; }
        public virtual DbSet<CPTCoordinator> CPTCoordinators { get; set; }
        public virtual DbSet<CPTRequest> CPTRequests { get; set; }
        public virtual DbSet<CRUManager> CRUManagers { get; set; }
        public virtual DbSet<CRUMember> CRUMembers { get; set; }
        public virtual DbSet<CRUSupervisor> CRUSupervisors { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EnterResult> EnterResults { get; set; }
        public virtual DbSet<ExecutionLocation> ExecutionLocations { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<MarketingDirector> MarketingDirectors { get; set; }
        public virtual DbSet<ProgressReport> ProgressReports { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }
        public virtual DbSet<QuestionnaireType> QuestionnaireTypes { get; set; }
        public virtual DbSet<Requester> Requesters { get; set; }
        public virtual DbSet<ResponsibleFor> ResponsibleFors { get; set; }
        public virtual DbSet<SelectQuestionnaire> SelectQuestionnaires { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Answer>()
                .HasMany(e => e.EnterResults)
                .WithRequired(e => e.Answer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BrandManager>()
                .HasMany(e => e.BFinalizedRequests)
                .WithRequired(e => e.BrandManagerDecision)
                .HasForeignKey(e => e.BDecisionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BrandManager>()
                .HasMany(e => e.SubmittedRequests)
                .WithOptional(e => e.BrandManagerSubmitRequest)
                .HasForeignKey(e => e.BEmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BrandManager>()
                .HasMany(e => e.BReveiewedRequests)
                .WithRequired(e => e.BrandManagerReviewRequest)
                .HasForeignKey(e => e.BReviewRequest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BrandManager>()
                .HasMany(e => e.Questionnaires)
                .WithRequired(e => e.BrandManager)
                .HasForeignKey(e => e.BEmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CPTCoordinator>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.CPTCoordinator)
                .HasForeignKey(e => e.CPTEmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CPTCoordinator>()
                .HasMany(e => e.SelectQuestionnaires)
                .WithRequired(e => e.CPTCoordinator)
                .HasForeignKey(e => e.CPTEmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CPTRequest>()
                .HasMany(e => e.ExecutionLocations)
                .WithRequired(e => e.CPTRequest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CPTRequest>()
                .HasMany(e => e.ProgressReports)
                .WithRequired(e => e.CPTRequest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CPTRequest>()
                .HasMany(e => e.SelectQuestionnaires)
                .WithRequired(e => e.CPTRequest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CRUManager>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.CRUManager)
                .HasForeignKey(e => e.CMAEmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CRUManager>()
                .HasMany(e => e.ProgressReports)
                .WithRequired(e => e.CRUManager)
                .HasForeignKey(e => e.CMAEmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CRUManager>()
                .HasMany(e => e.ResponsibleFors)
                .WithRequired(e => e.CRUManager)
                .HasForeignKey(e => e.CMAEmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CRUMember>()
                .HasMany(e => e.AssignWorks)
                .WithRequired(e => e.CRUMember)
                .HasForeignKey(e => e.CMEEmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CRUMember>()
                .HasMany(e => e.EnterResults)
                .WithRequired(e => e.CRUMember)
                .HasForeignKey(e => e.CMEEmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CRUSupervisor>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.CRUSupervisor)
                .HasForeignKey(e => e.CSEmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CRUSupervisor>()
                .HasMany(e => e.AssignWorks)
                .WithRequired(e => e.CRUSupervisor)
                .HasForeignKey(e => e.CSEmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CRUSupervisor>()
                .HasMany(e => e.CRUMembers)
                .WithRequired(e => e.AssignedCRUSupervisor)
                .HasForeignKey(e => e.CRUSupervisorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CRUSupervisor>()
                .HasMany(e => e.ProgressReports)
                .WithRequired(e => e.CRUSupervisor)
                .HasForeignKey(e => e.CSEmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CRUSupervisor>()
                .HasMany(e => e.ResponsibleFors)
                .WithRequired(e => e.CRUSupervisor)
                .HasForeignKey(e => e.CSEmployeeID)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Employee>()
            //    .HasOptional(e => e.BrandManager)
            //    .WithRequired(e => e.Employee);

            //modelBuilder.Entity<Employee>()
            //    .HasOptional(e => e.CPTCoordinator)
            //    .WithRequired(e => e.Employee);

            //modelBuilder.Entity<Employee>()
            //    .HasOptional(e => e.CRUManager)
            //    .WithRequired(e => e.Employee);

            //modelBuilder.Entity<Employee>()
            //    .HasOptional(e => e.CRUMember)
            //    .WithRequired(e => e.Employee);

            //modelBuilder.Entity<Employee>()
            //    .HasOptional(e => e.CRUSupervisor)
            //    .WithRequired(e => e.Employee);

            //modelBuilder.Entity<Employee>()
            //    .HasOptional(e => e.MarketingDirector)
            //    .WithRequired(e => e.Employee);

            //modelBuilder.Entity<Employee>()
            //    .HasOptional(e => e.Requester)
            //    .WithRequired(e => e.Employee);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.ExecutionLocations)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MarketingDirector>()
                .HasMany(e => e.MFinalizedRequests)
                .WithRequired(e => e.MarketingDirectorDecision)
                .HasForeignKey(e => e.MDecisionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MarketingDirector>()
                .HasMany(e => e.MReveiewedRequests)
                .WithRequired(e => e.MarketingDirectorReviewRequest)
                .HasForeignKey(e => e.MReviewRequest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MarketingDirector>()
                .HasMany(e => e.Questionnaires)
                .WithRequired(e => e.MarketingDirector)
                .HasForeignKey(e => e.MEmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.ContainQuestions)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.EnterResults)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.QuestionnaireTypes)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Questionnaire>()
                .HasMany(e => e.AdditionalQuestions)
                .WithRequired(e => e.Questionnaire)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Questionnaire>()
                .HasMany(e => e.AssignWorks)
                .WithRequired(e => e.Questionnaire)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Questionnaire>()
                .HasMany(e => e.ContainQuestions)
                .WithRequired(e => e.Questionnaire)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Questionnaire>()
                .HasMany(e => e.ResponsibleFors)
                .WithRequired(e => e.Questionnaire)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Questionnaire>()
                .HasMany(e => e.SelectQuestionnaires)
                .WithRequired(e => e.Questionnaire)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Requester>()
                .HasMany(e => e.CPTRequests)
                .WithOptional (e => e.Requester)
                .HasForeignKey(e => e.REmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CPTRequest>()
                .HasMany(c => c.Locations)
                .WithMany(f => f.CPTRequests)
                .Map(m =>
                {
                    m.ToTable("RequestLocation");
                    m.MapLeftKey("RequestID");
                    m.MapRightKey("LocationID");
                });
        }

        public System.Data.Entity.DbSet<ConsumerPanelTestSystemApplication.ViewModels.EmployeeViewModel> EmployeeViewModels { get; set; }

        public System.Data.Entity.DbSet<ConsumerPanelTestSystemApplication.ViewModels.CPTRequestViewModel> CPTRequestViewModels { get; set; }

        public System.Data.Entity.DbSet<ConsumerPanelTestSystemApplication.ViewModels.BrandManagerViewModel> BrandManagerViewModels { get; set; }

        public System.Data.Entity.DbSet<ConsumerPanelTestSystemApplication.ViewModels.RequesterViewModel> RequesterViewModels { get; set; }

        public System.Data.Entity.DbSet<ConsumerPanelTestSystemApplication.ViewModels.CRUMemberViewModel> CRUMemberViewModels { get; set; }

        public System.Data.Entity.DbSet<ConsumerPanelTestSystemApplication.ViewModels.CRUSupervisorViewModel> CRUSupervisorViewModels { get; set; }
    }

    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}