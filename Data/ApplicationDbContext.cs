using CPMS_AUTO.Helpers;
using CPMS_AUTO.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CPMS_AUTO.Repository.RepositoryHrms;

namespace CPMS_AUTO.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ClientEntity>().HasKey(m => m.ClientId);
            builder.Entity<TokenLog>().HasKey(m => m.Id);
            base.OnModelCreating(builder);

            //builder.Entity<UnitCountAndStatus>(
            //eb =>
            //{
            //    eb.HasNoKey();
            //    eb.ToView("View_BlogPostCounts");
            //    eb.Property(v => v.Unit).HasColumnName("Unit");
            //    eb.Property(v => v.Hrms).HasColumnName("Hrms");
            //    eb.Property(v => v.Iqmp).HasColumnName("Iqmp");
            //    eb.Property(v => v.Count).HasColumnName("Count");
            //});
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<InhClient> InhClient { get; set; }
        public DbSet<TokenLog> tokenLogs { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<Login> logins { get; set; }
        public DbSet<MComponent> Components { get; set; }
        public DbSet<Officer> Officers { get; set; }
        public DbSet<MSponsor> Sponsors { get; set; }
        public DbSet<MStatus> Statuss { get; set; }
        public DbSet<MVetting> MVettings { get; set; }
        public DbSet<MProject> Projects { get; set; }
        public DbSet<MRemarks> remarks { get; set; }
        public DbSet<MProjectSupport> support { get; set; }
        public DbSet<MSUSNo> mSUs { get; set; }
        public DbSet<MHrms> mHrms { get; set; }
        public DbSet<MUnitStatus> mUnitStatus { get; set; }
        public DbSet<MRank> mRank { get; set; }
        public DbSet<MVendor> mVendors { get; set; }
        public DbSet<MGrant> mGrants { get; set; }
        //public DbSet<UnitCountAndStatus> ucs { get; set; }
    }
}
