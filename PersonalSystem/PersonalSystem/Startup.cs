using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using PersonalSystem.Models;

[assembly: OwinStartupAttribute(typeof(PersonalSystem.Startup))]
namespace PersonalSystem
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
			createRolesandUsers();
		}

		private void createRolesandUsers()
		{
			ApplicationDbContext context = new ApplicationDbContext();

			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
			var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

			if (!roleManager.RoleExists("Admin"))
			{
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Admin";
				roleManager.Create(role);
			}

			if (!roleManager.RoleExists("Manager"))
			{
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Manager";
				roleManager.Create(role);
			}

			if (!roleManager.RoleExists("Employee"))
			{
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Employee";
				roleManager.Create(role);
			}

			if (!roleManager.RoleExists("Slave"))
			{
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Slave";
				roleManager.Create(role);

				var user = new ApplicationUser();
				user.UserName = "Baboon";
				user.Email = "Baboon@Bananas.com";

				string userPWD = "WeRBaboon_8";

				var chkUser = UserManager.Create(user, userPWD);

				if (chkUser.Succeeded)
				{
					UserManager.AddToRole(user.Id, "Slave");
				}
			}
		} 
	}
}
