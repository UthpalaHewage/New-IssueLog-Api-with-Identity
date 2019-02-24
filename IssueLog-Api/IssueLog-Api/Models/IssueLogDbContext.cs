using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IssueLog_Api.Models
{
    public class IssueLogDbContext : IdentityDbContext<ApplicationUser>
    {
        public IssueLogDbContext():base("IssueLogDbContext")
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Issue> Issues { get; set; }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //AspNetUsers -> User
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("User");
            //AspNetRoles -> Role
            modelBuilder.Entity<IdentityRole>()
                .ToTable("Role");
            //AspNetUserRoles -> UserRole
            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("UserRole");
            //AspNetUserClaims -> UserClaim
            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("UserClaim");
            //AspNetUserLogins -> UserLogin
            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("UserLogin");


            modelBuilder.Entity<Project>()
                       .HasRequired(m => m.ProjectManager)
                       .WithMany(t => t.ProjectManager)
                       .HasForeignKey(m => m.ProjectManagerId)
                       .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                        .HasRequired(m => m.Client)
                        .WithMany(t => t.Client)
                        .HasForeignKey(m => m.ClientId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                     .HasRequired(m => m.ProjectLeader)
                     .WithMany(t => t.ProjectLeader)
                     .HasForeignKey(m => m.ProjectLeaderId)
                     .WillCascadeOnDelete(false);

            modelBuilder.Entity<Issue>()
                  .HasRequired(m => m.Project)
                  .WithMany(t => t.Issues)
                  .HasForeignKey(m => m.ProjectId)
                  .WillCascadeOnDelete(false);


        }

       



    }
}