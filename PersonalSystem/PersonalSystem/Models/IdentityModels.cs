using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalSystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
		public virtual Group Group { get; set; }
		[Required]
		[StringLength(50, ErrorMessage = "{0} needs to be at least {2} characters", MinimumLength = 2)]
		public string FirstName { get; set; }
		[Required]
		[StringLength(50, ErrorMessage = "{0} needs to be at least {2} characters", MinimumLength = 2)]
		public string LastName { get; set; }
		[Required, Range(16, 500)]
		public int Age { get; set; }
		public bool? IsMale { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}