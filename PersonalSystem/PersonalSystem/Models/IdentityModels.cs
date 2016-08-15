using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace PersonalSystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
		public virtual Grupp Grupp { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

	public class Company
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual IEnumerable<LedigTjänst> LedigaTjänster { get; set; }
		public virtual IEnumerable<Nyhet> Nyheter { get; set; }
	}

	public class Avdelning
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual Company Company { get; set; }
		public virtual IEnumerable<Grupp> Grupper { get; set; }
	}

	public class Grupp
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual Avdelning Avdelning { get; set; }
		public virtual Schema Schema { get; set; }
		public virtual IEnumerable<ApplicationUser> Users { get; set; }
	}

	public class Schema
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class Nyhet
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual Company Company { get; set; }
		public bool IsInternal { get; set; }
		public string Text { get; set; }
	}

	public class LedigTjänst
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual IEnumerable<AnsökningsHandling> AnsökningsHandlingar { get; set; }
	}

	public class AnsökningsHandling
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual LedigTjänst LedigTjänst { get; set; }
	}

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		public DbSet<Company> Company { get; set; }
		public DbSet<Avdelning> Avdelning { get; set; }
		public DbSet<Grupp> Grupp { get; set; }
		public DbSet<Schema> Scheman { get; set; }
		public DbSet<Nyhet> Nyhet { get; set; }
		public DbSet<LedigTjänst> LedigTjänst { get; set; }
		public DbSet<AnsökningsHandling> AnsökningsHandling { get; set; }

		public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
			modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
			modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
			modelBuilder.Entity<IdentityRole>().ToTable("Role");
			modelBuilder.Entity<ApplicationUser>().ToTable("User")
													.Ignore(c => c.AccessFailedCount)
													.Ignore(c => c.LockoutEnabled)
													.Ignore(c => c.LockoutEndDateUtc)
													.Ignore(c => c.TwoFactorEnabled)
													.Ignore(c => c.PhoneNumber)
													.Ignore(c => c.PhoneNumberConfirmed);
		}
    }
}