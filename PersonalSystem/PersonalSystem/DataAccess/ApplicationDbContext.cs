using Microsoft.AspNet.Identity.EntityFramework;
using PersonalSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PersonalSystem.DataAccess
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Company> Companies { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<Schedule> Schedules { get; set; }
		public DbSet<News> News { get; set; }
		public DbSet<Vacancy> Vacancy { get; set; }
		public DbSet<Application> Applications { get; set; }

		public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
		{

		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
			modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
			modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
			modelBuilder.Entity<IdentityRole>().ToTable("Roles");
			modelBuilder.Entity<ApplicationUser>().ToTable("Users")
													.Ignore(c => c.AccessFailedCount)
													.Ignore(c => c.LockoutEnabled)
													.Ignore(c => c.LockoutEndDateUtc)
													.Ignore(c => c.TwoFactorEnabled)
													.Ignore(c => c.PhoneNumber)
													.Ignore(c => c.PhoneNumberConfirmed);
		}
	}
}